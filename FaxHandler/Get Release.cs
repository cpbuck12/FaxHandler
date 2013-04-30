using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web;
using System.Collections;
using System.IO;

using Word = Microsoft.Office.Interop.Word;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace FaxHandler
{
    public partial class Get_Release : Form
    {
        private string status = "";
        private ItemHolder XXXXXXXXXX;
        private Image signature;
        public string Status
        {
            get { return status; }
        }
        bool CheckRange()
        {
            DateTime from, until;
            from = dateTimePickerFrom.Value;
            until = dateTimePickerUntil.Value;
            if (from.Date > until.Date)
                return false;
            if (until.Date > DateTime.Now.Date)
                return false;
            return true;
        }
        public Get_Release(ItemHolder patient)
        {
            this.XXXXXXXXXX = patient;
            InitializeComponent();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetLocations();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> items = result["data"] as List<Hashtable>;
                Func<Hashtable, string> f = (Hashtable h) => { return h["shortname"] as string; };
                foreach (var item in items)
                {
                    comboBoxLocation.Items.Add(new ItemHolder(item,f));
                }
            }
            else
            {
                status = result["reason"] as string;
                Close();
            }
            Hashtable values = new Hashtable();
            values["patient_id"] = patient.values["id"];
            result = db.GetStamp(values);
            if (result["status"] as string != "ok")
            {
                status = result["reason"] as string;
                Close();
            }
            signature = result["image"] as Image;
            result = db.GetDoctors();
            Func<Hashtable, string> f2 = (Hashtable h) => { return h["shortname"] as string; };
            if (result["status"] as string == "ok")
            {
                var items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxPractitioner.Items.Add(new ItemHolder(item, f2));
                }
            }
            else
            {
                status = result["reason"] as string;
                Close();
            }
        }

        private void comboBoxPractitioner_Enter(object sender, EventArgs e)
        {
            comboBoxLocation.SelectedIndex = -1;
        }

        private void comboBoxLocation_Enter(object sender, EventArgs e)
        {
            comboBoxPractitioner.SelectedIndex = -1;
        }

        private Word.Document GetForm(Word.Application app,string name)
        {
            try
            {
                Db.Db db = Db.Db.Instance();
                Hashtable values = new Hashtable();
                values["name"] = name;
                Hashtable result = db.GetSpecialFile(values);
                if (result["status"] != "ok")
                    throw new Exception(result["reason"] as string);
                byte[] data = result["data"] as byte[];
                string dirName = Path.GetTempFileName() + ".dir";
                DirectoryInfo di = Directory.CreateDirectory(dirName);
                if (di == null)
                    throw new Exception("Could not create a temporary directory while getting a form");
                string fullName = dirName + "\\" + name + ".docx";
                FileStream fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write);
                fs.Write(data, 0, data.Length);
                fs.Close();
                fs.Dispose();
                fs = null;
                return app.Documents.Open(FileName: fullName,
                    ReadOnly: false,
                    Visible: true);
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return null;
            }
            /*
            const string formTemplate = @"z:\develop\{0}.docx";
            string destination = Path.GetTempFileName() + name + ".docx";
            string from = string.Format(formTemplate,name);
            File.Copy(from,destination);

            Word.Application app = new Word.Application();
            return app.Documents.Open(FileName: destination,
                ReadOnly: false,
                Visible: true);
            */
        }
        private Word.Shape GetBox(Word.Document document, float left, float top, float width, float height)
        {
            Word.Shape box = document.Shapes.AddTextbox
            (
                Orientation: Office.MsoTextOrientation.msoTextOrientationDownward,
                Left: left,
                Top: top,
                Width: width,
                Height: height
            );
//            box.Line.Visible = (Office.MsoTriState)0;
            box.TextFrame.TextRange.Text = "filler\nfiller";
            box.Fill.Visible = Office.MsoTriState.msoFalse;
            box.TextFrame.Orientation = Office.MsoTextOrientation.msoTextOrientationHorizontal;
            box.Line.Visible = Office.MsoTriState.msoFalse;
            return box;
        }
        // Cover text boxes
        // 1: fax
        // 2: to
        // 3: phone
        // 4: pages
        // 5: date
        private void FillInCover(Word.Document document, string to, string fax, string phone)
        {
            document.Shapes[2].TextFrame.TextRange.Text = to;
            document.Shapes[1].TextFrame.TextRange.Text = fax;
            document.Shapes[3].TextFrame.TextRange.Text = phone;
            document.Shapes[4].TextFrame.TextRange.Text = "2";
            document.Shapes[5].TextFrame.TextRange.Text = DateTime.Now.Date.ToString("MMMM d,yyyy");
            //Word.Shape toBox = GetBox(document, 72f, 360f, 360f, 72f);
            //toBox.TextFrame.TextRange.Text = to;
            //Word.Shape faxBox = GetBox(document, 72f, 432f, 360f, 72f);
            //faxBox.TextFrame.TextRange.Text = fax;
            //Word.Shape phoneBox = GetBox(document, 72f, 504f, 360f, 72f);
            //phoneBox.TextFrame.TextRange.Text = phone;
        }
        // Release text boxes
        // 1: name
        // 2: dob
        // 3: dr/hosp
        // 4: address
        // 5: phone
        // 6: fax
        // 7: beginning
        // 8: ending
        // 9: date
        private void FillInRelease(Word.Document document,string patientName,string dob,string drName,
            string address,string phone,string fax,string begin,string end)
        {
            int i = 0;
            foreach (dynamic shape in document.Shapes)
            {
                try
                {
                    shape.TextFrame.TextRange.Text = shape.Name;
                }
                catch (Exception)
                {
                }
                var shape2 = shape;
            }
            var s = document.Shapes;
            s[2].TextFrame.TextRange.Text = patientName;
            s[3].TextFrame.TextRange.Text = dob;
            s[4].TextFrame.TextRange.Text = drName;
            s[5].TextFrame.TextRange.Text = address;
            s[6].TextFrame.TextRange.Text = phone;
            s[7].TextFrame.TextRange.Text = fax;
            s[8].TextFrame.TextRange.Text = begin;
            s[9].TextFrame.TextRange.Text = end;
            //s[11].TextFrame.TextRange.Text = DateTime.Now.ToLongDateString();
        }
        string ToPdfName(string DocName)
        {
            return DocName.Substring(0,DocName.Length-4) + "pdf";
        }
        private void buttonPractitioner_Click(object sender, EventArgs e)
        {
            if (comboBoxPractitioner.SelectedIndex < 0)
                return;
            ItemHolder practitioner = comboBoxPractitioner.SelectedItem as ItemHolder;
            // debugging purposes
            Word.Application app = new Word.Application();
            app.Visible = true;
            Word.Document docCover = GetForm(app,"cover");
            if (docCover == null)
                return;
            Word.Document docRelease = GetForm(app,"Release");
            if (docRelease == null)
                return;
            string fax = practitioner.values["fax"] as string ?? "";
            string first = practitioner.values["firstname"] as string ?? "";
            string last = practitioner.values["lastname"] as string ?? "";
            string drName = string.Format("{0} {1}",first,last);
            string phone = practitioner.values["telehpone"] as string ?? "";
            string patientName = string.Format("{0} {1}",XXXXXXXXXX.values["first"],XXXXXXXXXX.values["last"]);
            string dob = ((DateTime)XXXXXXXXXX.values["dob"]).ToString("MMMM d,yyyy");
            string address1 = practitioner.values["address1"] as string ?? "";
            string address2 = practitioner.values["address2"] as string ?? "";
            string address3 = practitioner.values["address3"] as string ?? "";
            string address = address1;
            if(address2 != "")
                address = address + " " + address2;
            if(address3 != "")
                address = address + " " + address3;
            string locality1 = practitioner.values["locality1"] as string ?? "";
            string locality2 = practitioner.values["locality2"] as string ?? "";
            if(locality1 != "")
                address = address + " " + locality1;
            if(locality2 != "")
                address = address + " " + locality2;
            string postalcode = practitioner.values["postal_cod"] as string ?? "";
            if(postalcode != "")
                address = address + " " + postalcode;
            string from = dateTimePickerFrom.Value.ToString("M/d/yyyy");
            string until = dateTimePickerUntil.Value.ToString("M/d/yyyy");
            FillInRelease(docRelease,patientName,dob,drName,address,phone,fax,from,until);
            FillInCover(docCover, drName, fax, phone);
//            FillInRelease(docRelease,name,);
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.MailItem mi = outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

            mi.To = string.Format("{0}@faxage.com", fax);
            mi.Subject = mi.To;
            mi.To = "cpbuck12@hotmail.com";
            mi.Body = string.Format("Fax for {0} {1}", first, last);

            string fullName;
            string pdfName;
            fullName = docRelease.FullName;
            pdfName = ToPdfName(fullName);
            docRelease.ExportAsFixedFormat(pdfName,Word.WdExportFormat.wdExportFormatPDF);         
            docRelease.Close(SaveChanges:false);
            mi.Attachments.Add(pdfName);
            fullName = docCover.FullName;
            pdfName = ToPdfName(fullName);
            docCover.ExportAsFixedFormat(pdfName,Word.WdExportFormat.wdExportFormatPDF);
            docCover.Close(SaveChanges:false);
            mi.Attachments.Add(pdfName);
            mi.Send();
            Close();
        }
    }
}
