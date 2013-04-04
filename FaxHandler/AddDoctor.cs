using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaxHandler
{
    public partial class AddDoctor : Form
    {
        public DoctorProperties DoctorProperties { get; set; }
        public AddDoctor(string name)
        {
            InitializeComponent();
            labelName.Text = name;
            DoctorProperties = new DoctorProperties();
            DoctorProperties.Country = "United States";
            propertyGridDoctor.SelectedObject = DoctorProperties;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
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
    }
}
