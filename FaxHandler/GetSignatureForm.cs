using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Topaz;
using System.IO;
using System.Security.Cryptography;

















namespace FaxHandler
{
    public partial class GetSignatureForm : Form
    {
        const int defaultInk = 5;
        // signature specs:
        // 4.3" X 1.4"
        // 410 DPI resolution
        // My calculations mean that the total resoluation is  1763 x 574
        // To fit on a screen, half the total resolution to 881 x 287
        public GetSignatureForm(MainForm owner)
        {
            InitializeComponent();
            ClearReader();
            labelPatientNameValue.Text = owner.LastName + "," + owner.FirstName;
            signatureControl.SetImageFileFormat(0);
            signatureControl.SetImageXSize(881);
            signatureControl.SetImageYSize(287);
//            signatureControl.SetImageXSize(signatureControl.GetTabletLogicalXSize());
//            signatureControl.SetImageYSize(signatureControl.GetTabletLogicalYSize());
            numericUpDownInk.Value = InkSize = defaultInk;
            EnableReading(true);
        }
        private int InkSize
        {
            set
            {
                signatureControl.SetDisplayPenWidth(value);
                signatureControl.SetImagePenWidth(value);
            }
        }
        private void ClearReader()
        {
            signatureControl.ClearTablet();
        }
     
        private void EnableReading(bool val)
        {
            signatureControl.SetTabletState(val ? 1 : 0);
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            ClearReader();
            EnableReading(true);
        }

        private void GetSignatureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnableReading(false);
            ClearReader();
            signatureControl.Dispose();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
                        //Rijndael f;
            EnableReading(false);
            int xsize = signatureControl.GetImageXSize();
            int ysize = signatureControl.GetImageYSize();
            xsize = signatureControl.GetTabletLogicalXSize();
            ysize = signatureControl.GetTabletLogicalYSize();
            Image image = signatureControl.GetSigImage();
            

            //image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Close();
        }

        private void numericUpDownInk_ValueChanged(object sender, EventArgs e)
        {
            InkSize = (int) numericUpDownInk.Value;
        }
    }
}
