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
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;

namespace FaxHandler
{
    public partial class AddDoctor : Form
    {
        Timer timer;

        private DoctorProperties doctorProperties;

        public DoctorProperties DoctorProperties
        {
            get { return doctorProperties; }
            set
            {
                propertyGridDoctor.SelectedObject = value;
                doctorProperties = value;
            }
        }
        
        public AddDoctor(string name,bool edit = false)
        {
            InitializeComponent();
            Text = edit ? "Edit Practitioner" : "Add Practitioner";
            labelName.Text = name;
            DoctorProperties = new DoctorProperties();
            DoctorProperties.Country = "United States";
            propertyGridDoctor.SelectedObject = DoctorProperties;

            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetSearchEngines();
            comboBoxSearchEngines.DisplayMember = "Name";
            comboBoxSearchEngines.ValueMember = "QueryString";
            List<MiniSearchEngine> engines = new List<MiniSearchEngine>();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> entries = result["data"] as List<Hashtable>;
                foreach (Hashtable entry in entries)
                {
                    MiniSearchEngine mse = new MiniSearchEngine(entry);
                    engines.Add(mse);
                }
            }
            comboBoxSearchEngines.DataSource = engines;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {            
            if ((comboBoxSearchEngines.Text ?? "").Trim() == string.Empty)
            {
                buttonSearch.Enabled = false;
                return;
            }
            if ((DoctorProperties.FirstName ?? "").Trim() != string.Empty &&
                (DoctorProperties.LastName ?? "").Trim() != string.Empty)
            {
                buttonSearch.Enabled = true;
            }
            else
            {
                buttonSearch.Enabled = false;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        class MiniSearchEngine
        {
            public Hashtable values;
            public string Name
            {
                get { return values["name"] as string; }
            }
            public string QueryString
            {
                get { return values["query_string"] as string; }
            }
            public MiniSearchEngine(Hashtable values)
            {
                this.values = values;
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxSearchEngines.SelectedIndex < 0)
                return;
            string template = comboBoxSearchEngines.SelectedValue as string;
            string url = string.Format(template,
                string.Format("{0} {1}", DoctorProperties.FirstName, DoctorProperties.LastName));
            url = System.Uri.EscapeUriString(url);
            Process.Start(url);
        }
        bool IsOutlook(DragEventArgs e)
        {
            bool validObjectDescriptor = e.Data.GetDataPresent("Object Descriptor", false);
            if (!validObjectDescriptor)
                return false;
            MemoryStream ms = e.Data.GetData("Object Descriptor") as MemoryStream;
            if (ms == null)
                return false;
            byte[] buffer = new byte[ms.Length];
            ms.Read(buffer, 0, (int)ms.Length);
            uint position = BitConverter.ToUInt32(buffer, 0x2C);
            int length;
            bool found = false;
            for (length = 0; position + length < buffer.Length; length += 2)
            {
                ushort us = BitConverter.ToUInt16(buffer, (int)position + length);
                if (us == 0)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                return false;
            byte[] outlookStringBuffer = new byte[length];
            Array.Copy(buffer, position, outlookStringBuffer, 0, length);
            string outlookString = UnicodeEncoding.Unicode.GetString(outlookStringBuffer);
            if (outlookString == null || outlookString.ToLower().Trim() != "outlook")
                return false;
            return true;
        }
        Outlook.ContactItem GetContactItem(DragEventArgs e)
        {
            MemoryStream msId = e.Data.GetData(DataFormats.CommaSeparatedValue) as MemoryStream;
            if (msId == null)
                return null;
            byte[] idBuffer = new byte[msId.Length];
            msId.Read(idBuffer, 0, (int)msId.Length);
            string id = UnicodeEncoding.Unicode.GetString(idBuffer);
            if (id == null || id == string.Empty)
                return null;
            if (id.ToCharArray().Last() == 0)
            {
                id = id.Substring(0, id.Length - 1);
            }
            Outlook.Application app = new Outlook.Application();
            Outlook.NameSpace ns = app.GetNamespace("MAPI");
            Outlook.ContactItem ci = ns.GetItemFromID(id) as Outlook.ContactItem;
            if (ci == null)
                return null;
            return ci;
        }
        private void AddDoctor_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!IsOutlook(e))
                return;
            e.Effect = DragDropEffects.Copy;
        }

        private void AddDoctor_DragDrop(object sender, DragEventArgs e)
        {
            Outlook.ContactItem ci = GetContactItem(e);
            if (ci == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            DoctorProperties dp = propertyGridDoctor.SelectedObject as DoctorProperties;
            dp.FirstName = (ci.FirstName ?? string.Empty).Trim();
            dp.LastName = (ci.LastName ?? string.Empty).Trim();
            string bizadd = (ci.BusinessAddressStreet ?? string.Empty).Replace("\r", "").Trim();
            dp.Address1 = dp.Address2 = dp.Address3 = string.Empty;
            if (bizadd != string.Empty)
            {
                string[] lines = bizadd.Split(new char[] { '\n' });
                dp.Address1 = lines[0].Trim();
                if (lines.Length >= 2)
                    dp.Address2 = lines[1].Trim();
                if (lines.Length >= 3)
                    dp.Address3 = lines[2].Trim();
            }
            dp.City = (ci.BusinessAddressCity ?? string.Empty).Trim();
            dp.Locality1 = (ci.BusinessAddressState ?? string.Empty).Trim();
            dp.Locality2 = string.Empty;
            dp.PostalCode = (ci.BusinessAddressPostalCode ?? string.Empty).Trim();
            dp.Country = (ci.BusinessAddressCountry ?? "United States").Trim();
            dp.Telephone = (ci.BusinessTelephoneNumber ?? string.Empty).Trim();
            dp.Fax = (ci.BusinessFaxNumber ?? string.Empty).Trim();
            dp.Email = (ci.Email1Address ?? string.Empty).Trim();
            propertyGridDoctor.Refresh();
        }

        private void buttonCopyToOutlook_Click(object sender, EventArgs e)
        {
            Outlook.Application app = new Outlook.Application();
            Outlook.ContactItem ci = app.CreateItem(Outlook.OlItemType.olContactItem);
            DoctorProperties dp = propertyGridDoctor.SelectedObject as DoctorProperties;
            ci.FirstName = dp.FirstName;
            ci.LastName = dp.LastName;
            ci.BusinessAddressStreet = string.Format("{0}\r\n{1}\r\n{2}", dp.Address1, dp.Address2, dp.Address3);
            ci.BusinessAddressCity = dp.City;
            ci.BusinessAddressState = dp.Locality1;
            ci.BusinessAddressPostalCode = dp.PostalCode;
            ci.BusinessAddressCountry = dp.Country;
            ci.BusinessTelephoneNumber = dp.Telephone;
            ci.BusinessFaxNumber = dp.Fax;
            ci.Email1Address = dp.Email;
            if (dp.ContactPerson.Trim() != string.Empty)
            {
                ci.Body = dp.ContactPerson.Trim();
            }
            ci.Save();
            Outlook.Explorer oe = app.ActiveExplorer();
            if (oe != null)
            {
                oe.Activate();
            }
            else
                app.Quit();
        }
    }
    public class DoctorProperties
    {
        [Category("Name")]
        public string FirstName { get; set; }
        [Category("Name")]
        public string LastName { get; set; }
        [Category("Postal")]
        public string Address1 { get; set; }
        [Category("Postal")]
        public string Address2 { get; set; }
        [Category("Postal")]
        public string Address3 { get; set; }
        [Category("Postal")]
        public string City { get; set; }
        [Category("Postal")]
        [Description("U.S. State")]
        public string Locality1 { get; set; }
        [Category("Postal")]
        public string Locality2 { get; set; }
        [Description("U.S. Zip Code")]
        [Category("Postal")]
        public string PostalCode { get; set; }
        [Category("Postal")]
        public string Country { get; set; }
        [Category("Other")]
        public string Telephone { get; set; }
        [Category("Other")]
        public string Fax { get; set; }
        [Category("Other")]
        public string Email { get; set; }
        [Category("Other")]
        public string ContactPerson { get; set; }
        public DoctorProperties()
        {
            FirstName = LastName = Address1 = Address2 = Address3 = City = Locality1 = Locality2 = PostalCode = Country = Telephone = Fax = Email = ContactPerson = "";
        }
    }
}
