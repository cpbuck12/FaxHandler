using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaxHandler
{
    public partial class Dragger : UserControl
    {
        bool full;
        Rectangle dragBoxFromMouseDown;
        string filename;

        public bool Full
        {
            get
            {
                return Filename != null && Filename != string.Empty;
            }
        }
        public bool Dragging
        {
            get;
            set;
        }
        public string Filename
        {
            set
            {
                filename = value;
                Invalidate();
            }
            get
            {
                return filename;
            }
        }
        public Dragger()
        {
            InitializeComponent();
            filename = @"z:\develop\long";
        }

        private void Dragger_Paint(object sender, PaintEventArgs e)
        {
            Dragger dragger = sender as Dragger;
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Full ? ForeColor : BackColor), ClientRectangle);
            ControlPaint.DrawBorder3D(g, ClientRectangle,Full ? Border3DStyle.Raised : Border3DStyle.Sunken);
        }

        private void Dragger_MouseDown(object sender, MouseEventArgs e)
        {
            if(!Full)
                return;
            Size dragSize = SystemInformation.DragSize;
            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                           e.Y - (dragSize.Height / 2)),
                                                 dragSize);
        }
        private void Dragger_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            if (dragBoxFromMouseDown == null || dragBoxFromMouseDown.Contains(e.X, e.Y))
                return;
            if (!Full)
                return;
            string[] filenames = { filename };
            MainForm parent = Parent as MainForm;
            parent.AllowDrop = false;
            DataObject dataObject = new DataObject(DataFormats.FileDrop, filenames);
            DragDropEffects result = DoDragDrop(dataObject, DragDropEffects.Copy);
            parent.AllowDrop = true;
            if (result != DragDropEffects.Copy)
                return;
        }
    }
}
