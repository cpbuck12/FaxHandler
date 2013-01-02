using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Acrobat;

namespace FaxHandler.PDF
{ // overview
    // we get a file from explorer/mozilla or outlook.  outlook files will be temp files, others will not
    // either way, we need a temp file.  so, copy explorer/mozilla to temp file to keep same operations.
    // take the temp file, make a new PDDoc which will be anonymous.
    // when extracting, it will be from the anonymous pddoc to another temp file, which will then be moved/copied
    // to the ultimate destination.  whew!
    public enum ZoomLevel
    {
        FitHeight = 0,
        FitPage = 1,
        FitVisibleWidth = 2,
        FitWidth = 3
    }
    public enum PDSaveFlags
    {
	    PDSaveIncremental	= 0x0000,	/* write changes only */
	    PDSaveFull			= 0x0001,	/* write entire file */
	    PDSaveCopy			= 0x0002,	/* write copy w/o affecting current state */
	    PDSaveLinearized	= 0x0004,	/* writes the file linearized for
											    ** page-served remote (net) access.
											    */
	    PDSaveBinaryOK		= 0x0010,	/* OK to store binary in file */
	    PDSaveCollectGarbage = 0x0020	/* perform garbage collection on unreferenced objects */
    }

    public class Document :   IDisposable, ICloneable
    { 
        AcroAVDoc AVDocument = null;
        AcroPDDoc PDDocument = null;
        string fileName;
        private AcroPDDoc PDDoc()
        {
            if (PDDocument == null)
                PDDocument = AVDocument.GetPDDoc();
            return PDDocument;
        }
        private Document()
        {
            AVDocument = new AcroAVDoc();
        }
        public int Pages
        {
            get
            {
                if (!Valid)
                    return 0;
                else
                    return PDDoc().GetNumPages();
            }
        }
        public bool Valid
        {
            get
            {
                return AVDocument.IsValid();
            }
        }
        public void Zoom(short percent)
        {
            CAcroAVPageView pageView = AVDocument.GetAVPageView();
            pageView.ZoomTo(4 /* TODO: should be AVZoomNoVary, check */, percent);
        }
        public void Zoom(ZoomLevel zoom)
        {
            CAcroAVPageView pageView = AVDocument.GetAVPageView();
            pageView.ZoomTo((short)zoom, 100);
        }
        public void Show()
        {
            AVDocument.BringToFront();
            AVDocument.SetViewMode(2); // PDUseThumbs
        }
        public bool Save(string fileName)
        {
            int pages = Pages;
            bool result = PDDoc().Save((short)(PDSaveFlags.PDSaveCopy | PDSaveFlags.PDSaveFull | PDSaveFlags.PDSaveCollectGarbage), fileName);
            return result;
        }
        public Document(string fileName)
        {
            this.fileName = Path.GetTempFileName() + ".PDF";
            try
            {
                AcroAVDoc initialAVDocument = new AcroAVDoc();
                initialAVDocument.Open(fileName, "");
                AcroPDDoc initialPDDocument = initialAVDocument.GetPDDoc();
                initialPDDocument.Save((short)(PDSaveFlags.PDSaveCopy | PDSaveFlags.PDSaveFull | PDSaveFlags.PDSaveCollectGarbage), this.fileName);
                initialAVDocument.Close(1 /*no save*/);
                AVDocument = new AcroAVDoc();
                AVDocument.Open(this.fileName, "");
                PDDoc();
            }
            catch (Exception exception)
            {
                fileName = null;
                throw;
            }
        }
        bool Trim(PageRange pageRange)
        {
            int pages = Pages;
            if (pageRange.Begin < 0 || pageRange.Begin >= Pages || pageRange.End >= Pages || pageRange.Begin > pageRange.End)
            {
                return false; // TODO: throw exception?
            }
            if(pageRange.End - 1 < pages)
                PDDoc().DeletePages(pageRange.End, pages - 1);
            if (pageRange.Begin > 1)
                PDDoc().DeletePages(0, pageRange.Begin - 1);
            return true;
        }
        void Append(Document otherDocument)
        {
            PDDoc().InsertPages(Pages - 1, otherDocument.PDDoc(), 0, otherDocument.Pages, 1 /*copy bookmarks too*/);
        }
        public Document Trim(PageRanges pageRanges)
        {
            Document newDocument =(Document)Clone();
            newDocument.Trim(pageRanges.ToArray()[0]);
            foreach (var pageRange in pageRanges.Skip(1))
            {
                Document tempDocument = (Document)Clone();
                tempDocument.Trim(pageRange);
                newDocument.Append(tempDocument);
                tempDocument.Dispose();
            }
            return newDocument;
            // return (Trim(pageRanges.ToArray()[0])); // TODO: temporary definition for testing.  in the final version,
            //Trim does as whole lot more
        }
        private Document Extract(PageRanges ranges)
        {
            if (!Valid)
            {
                throw new Exception("No freakin way!");
            }
            Document Document = new Document(this.fileName);
            if (ReferenceEquals(ranges, PageRanges.All))
                Document.PDDoc().InsertPages(0, PDDoc(), 0, PDDoc().GetNumPages(), 1/*copy bookmarks too*/);
            else
            {
                if ((from r in ranges
                     where r.End >= this.Pages
                     select r).Count() > 0)
                {
                    throw new ArgumentException("Trying to extract pages beyond the end of the document");
                }
                foreach (var range in ranges)
                {
                    Document.PDDoc().InsertPages(PDDoc().GetNumPages(), PDDoc(), range.Begin - 1, range.Length, 1/*copy bookmarks too*/);
                }
            }
            return Document;
        }
        public Object Clone()
        {
            if (!Valid)
                return null;
            string fileName = Path.GetTempFileName() + ".PDF";
            if (!Save(fileName))
                return null;
            Document newDocument = new Document(fileName);
            return newDocument;
            //return Extract(PageRanges.All);
        }
        public void Dispose()
        {
            if (AVDocument != null)
            {
                AVDocument.Close(1 /* no save */);
                AVDocument = null;
            }
        }
    }
}
