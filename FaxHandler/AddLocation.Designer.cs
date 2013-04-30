namespace FaxHandler
{
    partial class AddLocation
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.propertyGridLocation = new System.Windows.Forms.PropertyGrid();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCopyToOutlook = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(69, 24);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(56, 16);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "label2";
            // 
            // propertyGridLocation
            // 
            this.propertyGridLocation.Location = new System.Drawing.Point(12, 43);
            this.propertyGridLocation.Name = "propertyGridLocation";
            this.propertyGridLocation.Size = new System.Drawing.Size(721, 374);
            this.propertyGridLocation.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(229, 423);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCopyToOutlook
            // 
            this.buttonCopyToOutlook.Location = new System.Drawing.Point(310, 423);
            this.buttonCopyToOutlook.Name = "buttonCopyToOutlook";
            this.buttonCopyToOutlook.Size = new System.Drawing.Size(105, 23);
            this.buttonCopyToOutlook.TabIndex = 3;
            this.buttonCopyToOutlook.Text = "Copy to Outlook...";
            this.buttonCopyToOutlook.UseVisualStyleBackColor = true;
            this.buttonCopyToOutlook.Click += new System.EventHandler(this.buttonCopyToOutlook_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(421, 423);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // AddLocation
            // 
            this.AcceptButton = this.buttonOk;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(745, 458);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCopyToOutlook);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.propertyGridLocation);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Name = "AddLocation";
            this.Text = "AddLocation";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.AddLocation_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.AddLocation_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PropertyGrid propertyGridLocation;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCopyToOutlook;
        private System.Windows.Forms.Button buttonCancel;
    }
}