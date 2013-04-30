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
    public partial class AddPatient : Form
    {
        public PatientProperties PatientProperties { get; set; }
        public AddPatient(string name)
        {
            InitializeComponent();
            labelName.Text = name;
            PatientProperties = new PatientProperties();
            propertyGridPatient.SelectedObject = PatientProperties;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
    internal class MyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { "M", "F" });
        }
    }
    public class PatientProperties
    {
        public string First { get; set; }
        public string Last { get; set; } 
        public DateTime DateOfBirth { get; set; }
        [TypeConverter(typeof(MyConverter))]
        [DefaultValue("F")]
        public string Gender { get; set; }
        public string EmergencyContact { get; set; }
    }
}
