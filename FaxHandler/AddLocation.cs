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
    public partial class AddLocation : Form
    {
        public LocationProperties LocationProperties { get; set; }
        public AddLocation(string name)
        {
            InitializeComponent();
            labelName.Text = name;
            LocationProperties = new LocationProperties();
            LocationProperties.Country = "United States";
            propertyGridLocation.SelectedObject = LocationProperties;
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
        private void buttonCopyToOutlook_Click(object sender, EventArgs e)
        {
            Outlook.Application app = new Outlook.Application();
            Outlook.ContactItem ci = app.CreateItem(Outlook.OlItemType.olContactItem);
            LocationProperties lp = propertyGridLocation.SelectedObject as LocationProperties;
            ci.BusinessAddressStreet = string.Format("{0}\r\n{1}\r\n{2}", lp.Address1, lp.Address2, lp.Address3);
            ci.BusinessAddressCity = lp.City;
            ci.BusinessAddressState = lp.Locality1;
            ci.BusinessAddressPostalCode = lp.PostalCode;
            ci.BusinessAddressCountry = lp.Country;
            ci.BusinessTelephoneNumber = lp.Telephone;
            ci.BusinessFaxNumber = lp.Fax;
            ci.Email1Address = lp.Email;
            ci.Body = lp.ContactPerson;
            ci.FullName = labelName.Text;
            ci.Save();
            app.ActiveExplorer().Activate();
        }

        private void AddLocation_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!IsOutlook(e))
                return;
            e.Effect = DragDropEffects.Copy;
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

        private void AddLocation_DragDrop(object sender, DragEventArgs e)
        {
            Outlook.ContactItem ci = GetContactItem(e);
            if (ci == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            LocationProperties lp = propertyGridLocation.SelectedObject as LocationProperties;
            string bizadd = (ci.BusinessAddressStreet ?? string.Empty).Replace("\r", "").Trim();
            lp.Address1 = lp.Address2 = lp.Address3 = string.Empty;
            if (bizadd != string.Empty)
            {
                string[] lines = bizadd.Split(new char[] { '\n' });
                lp.Address1 = lines[0].Trim();
                if (lines.Length >= 2)
                    lp.Address2 = lines[1].Trim();
                if (lines.Length >= 3)
                    lp.Address3 = lines[2].Trim();
            }
            lp.City = (ci.BusinessAddressCity ?? string.Empty).Trim();
            lp.Locality1 = (ci.BusinessAddressState ?? string.Empty).Trim();
            lp.Locality2 = string.Empty;
            lp.PostalCode = (ci.BusinessAddressPostalCode ?? string.Empty).Trim();
            lp.Country = (ci.BusinessAddressCountry ?? "United States").Trim();
            lp.Telephone = (ci.BusinessTelephoneNumber ?? string.Empty).Trim();
            lp.Fax = (ci.BusinessFaxNumber ?? string.Empty).Trim();
            lp.Email = (ci.Email1Address ?? string.Empty).Trim();
            propertyGridLocation.Refresh();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
    public class LocationProperties
    {
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
        public LocationProperties()
        {
            Address1 = Address2 = Address3 = City = Locality1 = Locality2 = PostalCode = Country = Telephone = Fax = Email = ContactPerson = "";
        }
    }
}
