namespace FaxHandler
{
    partial class ConciergeBrowser
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
            this.treeViewDirectories = new System.Windows.Forms.TreeView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDivider = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeViewDirectories
            // 
            this.treeViewDirectories.Location = new System.Drawing.Point(12, 12);
            this.treeViewDirectories.Name = "treeViewDirectories";
            this.treeViewDirectories.Size = new System.Drawing.Size(721, 333);
            this.treeViewDirectories.TabIndex = 0;
            this.treeViewDirectories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDirectories_AfterSelect);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(263, 355);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(370, 355);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDivider
            // 
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Location = new System.Drawing.Point(12, 348);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(721, 4);
            this.labelDivider.TabIndex = 2;
            // 
            // ConciergeBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 390);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.treeViewDirectories);
            this.Name = "ConciergeBrowser";
            this.Text = "ConciergeBrowser";
            this.Load += new System.EventHandler(this.ConciergeBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDirectories;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDivider;
    }
}