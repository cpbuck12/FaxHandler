namespace FaxHandler
{
    partial class AddDoctor
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
            this.labelDoctor = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.propertyGridDoctor = new System.Windows.Forms.PropertyGrid();
            this.labelDivider = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxSearchEngines = new System.Windows.Forms.ComboBox();
            this.buttonCopyToOutlook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDoctor
            // 
            this.labelDoctor.AutoSize = true;
            this.labelDoctor.Location = new System.Drawing.Point(26, 27);
            this.labelDoctor.Name = "labelDoctor";
            this.labelDoctor.Size = new System.Drawing.Size(42, 13);
            this.labelDoctor.TabIndex = 0;
            this.labelDoctor.Text = "Doctor:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(105, 26);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(56, 16);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "label1";
            // 
            // propertyGridDoctor
            // 
            this.propertyGridDoctor.Location = new System.Drawing.Point(12, 51);
            this.propertyGridDoctor.Name = "propertyGridDoctor";
            this.propertyGridDoctor.Size = new System.Drawing.Size(717, 369);
            this.propertyGridDoctor.TabIndex = 2;
            // 
            // labelDivider
            // 
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Location = new System.Drawing.Point(12, 423);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(717, 3);
            this.labelDivider.TabIndex = 3;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(191, 460);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(419, 460);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(357, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxSearchEngines
            // 
            this.comboBoxSearchEngines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchEngines.FormattingEnabled = true;
            this.comboBoxSearchEngines.Location = new System.Drawing.Point(438, 24);
            this.comboBoxSearchEngines.Name = "comboBoxSearchEngines";
            this.comboBoxSearchEngines.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSearchEngines.TabIndex = 7;
            // 
            // buttonCopyToOutlook
            // 
            this.buttonCopyToOutlook.Location = new System.Drawing.Point(272, 460);
            this.buttonCopyToOutlook.Name = "buttonCopyToOutlook";
            this.buttonCopyToOutlook.Size = new System.Drawing.Size(141, 23);
            this.buttonCopyToOutlook.TabIndex = 8;
            this.buttonCopyToOutlook.Text = "Copy To Outlook...";
            this.buttonCopyToOutlook.UseVisualStyleBackColor = true;
            this.buttonCopyToOutlook.Click += new System.EventHandler(this.buttonCopyToOutlook_Click);
            // 
            // AddDoctor
            // 
            this.AcceptButton = this.buttonOk;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(741, 494);
            this.Controls.Add(this.buttonCopyToOutlook);
            this.Controls.Add(this.comboBoxSearchEngines);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.propertyGridDoctor);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelDoctor);
            this.Name = "AddDoctor";
            this.Text = "AddDoctor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.AddDoctor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.AddDoctor_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDoctor;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PropertyGrid propertyGridDoctor;
        private System.Windows.Forms.Label labelDivider;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox comboBoxSearchEngines;
        private System.Windows.Forms.Button buttonCopyToOutlook;
    }
}