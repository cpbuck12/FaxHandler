using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Acrobat;

namespace FaxHandler.PDF
{ // overview
    // we get a file from explorer/mozilla or outlook.  outlook files will be temp files, others will not
    // either way, we need a temp file.  so, copy explorer/mozilla to temp file to keep same operations.
    // take the temp file, make a new PDDoc which will be anonymous.
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
    public enum AVZoomType
    {
        AVZoomNoVary,					/* no variable zoom */
        AVZoomFitPage,					/* fit page to window */
        AVZoomFitWidth,					/* fit page width to window */
        AVZoomFitHeight,				/* fit page height to window */
        AVZoomFitVisibleWidth,			/* fit visible width to window */
        AVZoomPreferred					/* use page's preferred zoom */
    }
    public enum ZoomLevel
    {
        FitHeight = AVZoomType.AVZoomFitHeight,
        FitPage = AVZoomType.AVZoomFitHeight,
        FitVisibleWidth = AVZoomType.AVZoomFitVisibleWidth,
        FitWidth = AVZoomType.AVZoomFitWidth
    }

    public class Document :   IDisposable, ICloneable
    { 
        AcroAVDoc AVDocument = null;
        AcroPDDoc PDDocument = null;
        string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
        }
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
            pageView.ZoomTo((short)(AVZoomType.AVZoomNoVary), percent);
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
        public string Sha1()
        {
            string tempFileName = Path.GetTempFileName() + ".PDF";
            Save(tempFileName);
            SHA1 sha1 = SHA1.Create();
            byte[] results;
            FileStream fileStream = new FileStream(tempFileName, FileMode.Open);
            results = sha1.ComputeHash(fileStream);
            fileStream.Dispose();
            fileStream = null;
            File.Delete(tempFileName);
            string[] values = (from result in results.AsQueryable()
                               select string.Format("{0:X2}", result)).ToArray();
            string returnValue = string.Concat(values);
            return returnValue;
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
                AVDocument.Open(this.fileName, "Copy of " + fileName);
                PDDoc();
            }
            catch (Exception)
            {
                fileName = null;
                throw;
            }
        }
        bool Trim(PageRange pageRange)
        {
            if (pageRange.Begin < 1 || pageRange.Begin > Pages || pageRange.End > Pages || pageRange.Begin > pageRange.End)
                return false; 
            if(pageRange.End < Pages)
                PDDoc().DeletePages(pageRange.End, Pages - 1);
            if (pageRange.Begin > 1)
                PDDoc().DeletePages(0, pageRange.Begin - 2);
            return true;
        }
        void Append(Document otherDocument)
        {
            PDDoc().InsertPages(Pages - 1, otherDocument.PDDoc(), 0, otherDocument.Pages, 1 /*copy bookmarks too*/);
        }
        public Document TrimPages(PageRanges pageRanges)
        {
            Document newDocument =(Document)Clone();
            if (!newDocument.Trim(pageRanges.ToArray()[0]))
                return null;
            foreach (var pageRange in pageRanges.Skip(1))
            {
                Document tempDocument = (Document)Clone();
                if (!tempDocument.Trim(pageRange))
                {
                    newDocument.Dispose();
                    tempDocument.Dispose();
                    return null;
                }
                newDocument.Append(tempDocument);
                tempDocument.Dispose();
            }
            return newDocument;
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
        }
        public void Dispose()
        {
            if (AVDocument != null)
            {
                AVDocument.Close(1 /* no save */);
                AVDocument = null;
            }
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
