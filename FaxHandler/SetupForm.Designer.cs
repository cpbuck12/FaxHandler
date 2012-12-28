namespace FaxHandler
{
    partial class SetupForm
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
            this.labelConciegeLocation = new System.Windows.Forms.Label();
            this.labelConciegeLocationValue = new System.Windows.Forms.Label();
            this.buttonChangeConciergeLocation = new System.Windows.Forms.Button();
            this.labelProcedures = new System.Windows.Forms.Label();
            this.textBoxProcedures = new System.Windows.Forms.TextBox();
            this.labelDivider = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelConciegeLocation
            // 
            this.labelConciegeLocation.AutoSize = true;
            this.labelConciegeLocation.Location = new System.Drawing.Point(119, 25);
            this.labelConciegeLocation.Name = "labelConciegeLocation";
            this.labelConciegeLocation.Size = new System.Drawing.Size(87, 13);
            this.labelConciegeLocation.TabIndex = 0;
            this.labelConciegeLocation.Text = "Concierge Folder";
            // 
            // labelConciegeLocationValue
            // 
            this.labelConciegeLocationValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelConciegeLocationValue.Location = new System.Drawing.Point(212, 25);
            this.labelConciegeLocationValue.Name = "labelConciegeLocationValue";
            this.labelConciegeLocationValue.Size = new System.Drawing.Size(304, 18);
            this.labelConciegeLocationValue.TabIndex = 1;
            this.labelConciegeLocationValue.Text = ":";
            // 
            // buttonChangeConciergeLocation
            // 
            this.buttonChangeConciergeLocation.Location = new System.Drawing.Point(21, 20);
            this.buttonChangeConciergeLocation.Name = "buttonChangeConciergeLocation";
            this.buttonChangeConciergeLocation.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeConciergeLocation.TabIndex = 2;
            this.buttonChangeConciergeLocation.Text = "Change...";
            this.buttonChangeConciergeLocation.UseVisualStyleBackColor = true;
            this.buttonChangeConciergeLocation.Click += new System.EventHandler(this.buttonChangeConciergeLocation_Click);
            // 
            // labelProcedures
            // 
            this.labelProcedures.AutoSize = true;
            this.labelProcedures.Location = new System.Drawing.Point(126, 68);
            this.labelProcedures.Name = "labelProcedures";
            this.labelProcedures.Size = new System.Drawing.Size(61, 13);
            this.labelProcedures.TabIndex = 3;
            this.labelProcedures.Text = "Procedures";
            // 
            // textBoxProcedures
            // 
            this.textBoxProcedures.Location = new System.Drawing.Point(215, 68);
            this.textBoxProcedures.Multiline = true;
            this.textBoxProcedures.Name = "textBoxProcedures";
            this.textBoxProcedures.Size = new System.Drawing.Size(280, 200);
            this.textBoxProcedures.TabIndex = 4;
            this.textBoxProcedures.TextChanged += new System.EventHandler(this.textBoxProcedures_TextChanged);
            this.textBoxProcedures.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxProcedures_Validating);
            // 
            // labelDivider
            // 
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Location = new System.Drawing.Point(0, 284);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(530, 2);
            this.labelDivider.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(112, 304);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(333, 304);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 341);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.textBoxProcedures);
            this.Controls.Add(this.labelProcedures);
            this.Controls.Add(this.buttonChangeConciergeLocation);
            this.Controls.Add(this.labelConciegeLocationValue);
            this.Controls.Add(this.labelConciegeLocation);
            this.Name = "SetupForm";
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConciegeLocation;
        private System.Windows.Forms.Label labelConciegeLocationValue;
        private System.Windows.Forms.Button buttonChangeConciergeLocation;
        private System.Windows.Forms.Label labelProcedures;
        private System.Windows.Forms.TextBox textBoxProcedures;
        private System.Windows.Forms.Label labelDivider;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}