using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace FaxHandler
{
    public partial class SetupForm : Form
    {
        bool changed;
        bool Changed 
        {
            get
            {
                return changed;
            }
            set
            {
                changed = value;
                buttonSave.Enabled = changed;
            }
        }

        public SetupForm()
        {
            InitializeComponent();
            Changed = false;
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            labelConciegeLocationValue.Text = Properties.Settings.Default.ConciergeLocation;

            char[] delimeters = { '\n' };
            textBoxProcedures.Lines = Properties.Settings.Default.Procedures.Split(delimeters);
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            DateTime buildDateTime = new DateTime(2000, 1, 1).Add(
                new TimeSpan(TimeSpan.TicksPerDay * version.Build + TimeSpan.TicksPerSecond * 2 * version.Revision));
            labelBuildInfo.Text = string.Format("Build Date: {0}", buildDateTime);
        }

        private void textBoxProcedures_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void buttonChangeConciergeLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = labelConciegeLocationValue.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!dialog.SelectedPath.Equals(labelConciegeLocationValue.Text))
                {
                    Changed = true;
                    labelConciegeLocationValue.Text = dialog.SelectedPath;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ConciergeLocation = labelConciegeLocationValue.Text;

            Properties.Settings.Default.Procedures = String.Concat(
             (from procedureName in textBoxProcedures.Lines
              where procedureName.Trim().Length > 0
              orderby procedureName.Trim().ToUpper() ascending
              select procedureName.Trim().ToUpper() + "\n"));

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxProcedures_Validating(object sender, CancelEventArgs e)
        {
            Regex r = new Regex(@"\-{2}");
            if ((from line in textBoxProcedures.Lines
                 where r.IsMatch(line.Trim())
                 select line).Count() > 0)
            {
                MessageBox.Show("No embedded double dashes");
                e.Cancel = true;
                return;
            }
        }

    }
}
