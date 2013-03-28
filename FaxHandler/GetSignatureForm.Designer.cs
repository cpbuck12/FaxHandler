namespace FaxHandler
{
    partial class GetSignatureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.signatureControl = new Topaz.SigPlusNET();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPatientName = new System.Windows.Forms.Label();
            this.labelPatientNameValue = new System.Windows.Forms.Label();
            this.numericUpDownInk = new System.Windows.Forms.NumericUpDown();
            this.labelInk = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInk)).BeginInit();
            this.SuspendLayout();
            // 
            // signatureControl
            // 
            this.signatureControl.BackColor = System.Drawing.Color.White;
            this.signatureControl.ForeColor = System.Drawing.Color.Black;
            this.signatureControl.Location = new System.Drawing.Point(13, 12);
            this.signatureControl.Name = "signatureControl";
            this.signatureControl.Size = new System.Drawing.Size(881, 287);
            this.signatureControl.TabIndex = 0;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(657, 308);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 1;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonRetry
            // 
            this.buttonRetry.Location = new System.Drawing.Point(738, 308);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(75, 23);
            this.buttonRetry.TabIndex = 2;
            this.buttonRetry.Text = "Retry";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(819, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelPatientName
            // 
            this.labelPatientName.AutoSize = true;
            this.labelPatientName.Location = new System.Drawing.Point(12, 313);
            this.labelPatientName.Name = "labelPatientName";
            this.labelPatientName.Size = new System.Drawing.Size(74, 13);
            this.labelPatientName.TabIndex = 4;
            this.labelPatientName.Text = "Patient Name:";
            // 
            // labelPatientNameValue
            // 
            this.labelPatientNameValue.AutoSize = true;
            this.labelPatientNameValue.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatientNameValue.Location = new System.Drawing.Point(89, 313);
            this.labelPatientNameValue.Name = "labelPatientNameValue";
            this.labelPatientNameValue.Size = new System.Drawing.Size(56, 16);
            this.labelPatientNameValue.TabIndex = 5;
            this.labelPatientNameValue.Text = "label1";
            // 
            // numericUpDownInk
            // 
            this.numericUpDownInk.Location = new System.Drawing.Point(598, 311);
            this.numericUpDownInk.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownInk.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInk.Name = "numericUpDownInk";
            this.numericUpDownInk.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownInk.TabIndex = 6;
            this.numericUpDownInk.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownInk.ValueChanged += new System.EventHandler(this.numericUpDownInk_ValueChanged);
            // 
            // labelInk
            // 
            this.labelInk.AutoSize = true;
            this.labelInk.Location = new System.Drawing.Point(570, 313);
            this.labelInk.Name = "labelInk";
            this.labelInk.Size = new System.Drawing.Size(22, 13);
            this.labelInk.TabIndex = 7;
            this.labelInk.Text = "Ink";
            // 
            // GetSignatureForm
            // 
            this.AcceptButton = this.buttonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(906, 341);
            this.Controls.Add(this.labelInk);
            this.Controls.Add(this.numericUpDownInk);
            this.Controls.Add(this.labelPatientNameValue);
            this.Controls.Add(this.labelPatientName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.signatureControl);
            this.Name = "GetSignatureForm";
            this.Text = "Get Signature";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetSignatureForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Topaz.SigPlusNET signatureControl;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPatientName;
        private System.Windows.Forms.Label labelPatientNameValue;
        private System.Windows.Forms.NumericUpDown numericUpDownInk;
        private System.Windows.Forms.Label labelInk;
    }
}