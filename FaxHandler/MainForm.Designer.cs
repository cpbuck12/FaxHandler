namespace FaxHandler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxProcedureName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsLastName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsFirstName = new System.Windows.Forms.TextBox();
            this.textBoxProcedureLocation = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePages = new System.Windows.Forms.TextBox();
            this.labelProcedurePages = new System.Windows.Forms.Label();
            this.textBoxProcedureDoctor = new System.Windows.Forms.TextBox();
            this.dateTimePickerProcedure = new System.Windows.Forms.DateTimePicker();
            this.textBoxProcedureDate = new System.Windows.Forms.TextBox();
            this.labelProcedureName = new System.Windows.Forms.Label();
            this.labelProcedurePatientLastName = new System.Windows.Forms.Label();
            this.labelProcedurePatientFirstName = new System.Windows.Forms.Label();
            this.labelProcedureLocation = new System.Windows.Forms.Label();
            this.labelProcedureDoctor = new System.Windows.Forms.Label();
            this.labelProcedureDate = new System.Windows.Forms.Label();
            this.buttonSetup = new System.Windows.Forms.Button();
            this.buttonSaveConciergeProcedure = new System.Windows.Forms.Button();
            this.buttonSaveProcedure = new System.Windows.Forms.Button();
            this.checkBoxView = new System.Windows.Forms.CheckBox();
            this.dragger1 = new FaxHandler.Dragger();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxProcedureName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsLastName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsFirstName);
            this.groupBox1.Controls.Add(this.textBoxProcedureLocation);
            this.groupBox1.Controls.Add(this.textBoxProcedurePages);
            this.groupBox1.Controls.Add(this.labelProcedurePages);
            this.groupBox1.Controls.Add(this.textBoxProcedureDoctor);
            this.groupBox1.Controls.Add(this.dateTimePickerProcedure);
            this.groupBox1.Controls.Add(this.textBoxProcedureDate);
            this.groupBox1.Controls.Add(this.labelProcedureName);
            this.groupBox1.Controls.Add(this.labelProcedurePatientLastName);
            this.groupBox1.Controls.Add(this.labelProcedurePatientFirstName);
            this.groupBox1.Controls.Add(this.labelProcedureLocation);
            this.groupBox1.Controls.Add(this.labelProcedureDoctor);
            this.groupBox1.Controls.Add(this.labelProcedureDate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 220);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Procedure";
            // 
            // textBoxProcedureName
            // 
            this.textBoxProcedureName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxProcedureName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxProcedureName.Location = new System.Drawing.Point(148, 157);
            this.textBoxProcedureName.Name = "textBoxProcedureName";
            this.textBoxProcedureName.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedureName.TabIndex = 105;
            this.textBoxProcedureName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePatientsLastName
            // 
            this.textBoxProcedurePatientsLastName.Location = new System.Drawing.Point(148, 129);
            this.textBoxProcedurePatientsLastName.Name = "textBoxProcedurePatientsLastName";
            this.textBoxProcedurePatientsLastName.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedurePatientsLastName.TabIndex = 104;
            this.textBoxProcedurePatientsLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePatientsFirstName
            // 
            this.textBoxProcedurePatientsFirstName.Location = new System.Drawing.Point(148, 101);
            this.textBoxProcedurePatientsFirstName.Name = "textBoxProcedurePatientsFirstName";
            this.textBoxProcedurePatientsFirstName.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedurePatientsFirstName.TabIndex = 103;
            this.textBoxProcedurePatientsFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedureLocation
            // 
            this.textBoxProcedureLocation.Location = new System.Drawing.Point(148, 75);
            this.textBoxProcedureLocation.Name = "textBoxProcedureLocation";
            this.textBoxProcedureLocation.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedureLocation.TabIndex = 102;
            this.textBoxProcedureLocation.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePages
            // 
            this.textBoxProcedurePages.Enabled = false;
            this.textBoxProcedurePages.Location = new System.Drawing.Point(148, 183);
            this.textBoxProcedurePages.Name = "textBoxProcedurePages";
            this.textBoxProcedurePages.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedurePages.TabIndex = 106;
            // 
            // labelProcedurePages
            // 
            this.labelProcedurePages.AutoSize = true;
            this.labelProcedurePages.Location = new System.Drawing.Point(20, 186);
            this.labelProcedurePages.Name = "labelProcedurePages";
            this.labelProcedurePages.Size = new System.Drawing.Size(43, 13);
            this.labelProcedurePages.TabIndex = 2;
            this.labelProcedurePages.Text = "Page(s)";
            // 
            // textBoxProcedureDoctor
            // 
            this.textBoxProcedureDoctor.Location = new System.Drawing.Point(148, 48);
            this.textBoxProcedureDoctor.Name = "textBoxProcedureDoctor";
            this.textBoxProcedureDoctor.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedureDoctor.TabIndex = 101;
            this.textBoxProcedureDoctor.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // dateTimePickerProcedure
            // 
            this.dateTimePickerProcedure.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerProcedure.Location = new System.Drawing.Point(249, 21);
            this.dateTimePickerProcedure.Name = "dateTimePickerProcedure";
            this.dateTimePickerProcedure.Size = new System.Drawing.Size(104, 20);
            this.dateTimePickerProcedure.TabIndex = 100;
            this.dateTimePickerProcedure.ValueChanged += new System.EventHandler(this.dateTimePickerProcedure_ValueChanged);
            // 
            // textBoxProcedureDate
            // 
            this.textBoxProcedureDate.Enabled = false;
            this.textBoxProcedureDate.Location = new System.Drawing.Point(148, 21);
            this.textBoxProcedureDate.Name = "textBoxProcedureDate";
            this.textBoxProcedureDate.Size = new System.Drawing.Size(95, 20);
            this.textBoxProcedureDate.TabIndex = 5;
            // 
            // labelProcedureName
            // 
            this.labelProcedureName.AutoSize = true;
            this.labelProcedureName.Location = new System.Drawing.Point(20, 160);
            this.labelProcedureName.Name = "labelProcedureName";
            this.labelProcedureName.Size = new System.Drawing.Size(87, 13);
            this.labelProcedureName.TabIndex = 5;
            this.labelProcedureName.Text = "Procedure Name";
            // 
            // labelProcedurePatientLastName
            // 
            this.labelProcedurePatientLastName.AutoSize = true;
            this.labelProcedurePatientLastName.Location = new System.Drawing.Point(20, 132);
            this.labelProcedurePatientLastName.Name = "labelProcedurePatientLastName";
            this.labelProcedurePatientLastName.Size = new System.Drawing.Size(101, 13);
            this.labelProcedurePatientLastName.TabIndex = 5;
            this.labelProcedurePatientLastName.Text = "Patient\'s Last Name";
            // 
            // labelProcedurePatientFirstName
            // 
            this.labelProcedurePatientFirstName.AutoSize = true;
            this.labelProcedurePatientFirstName.Location = new System.Drawing.Point(20, 105);
            this.labelProcedurePatientFirstName.Name = "labelProcedurePatientFirstName";
            this.labelProcedurePatientFirstName.Size = new System.Drawing.Size(100, 13);
            this.labelProcedurePatientFirstName.TabIndex = 4;
            this.labelProcedurePatientFirstName.Text = "Patient\'s First Name";
            // 
            // labelProcedureLocation
            // 
            this.labelProcedureLocation.AutoSize = true;
            this.labelProcedureLocation.Location = new System.Drawing.Point(20, 78);
            this.labelProcedureLocation.Name = "labelProcedureLocation";
            this.labelProcedureLocation.Size = new System.Drawing.Size(48, 13);
            this.labelProcedureLocation.TabIndex = 3;
            this.labelProcedureLocation.Text = "Location";
            // 
            // labelProcedureDoctor
            // 
            this.labelProcedureDoctor.AutoSize = true;
            this.labelProcedureDoctor.Location = new System.Drawing.Point(19, 51);
            this.labelProcedureDoctor.Name = "labelProcedureDoctor";
            this.labelProcedureDoctor.Size = new System.Drawing.Size(39, 13);
            this.labelProcedureDoctor.TabIndex = 2;
            this.labelProcedureDoctor.Text = "Doctor";
            // 
            // labelProcedureDate
            // 
            this.labelProcedureDate.AutoSize = true;
            this.labelProcedureDate.Location = new System.Drawing.Point(20, 24);
            this.labelProcedureDate.Name = "labelProcedureDate";
            this.labelProcedureDate.Size = new System.Drawing.Size(30, 13);
            this.labelProcedureDate.TabIndex = 0;
            this.labelProcedureDate.Text = "Date";
            // 
            // buttonSetup
            // 
            this.buttonSetup.Location = new System.Drawing.Point(388, 243);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(54, 23);
            this.buttonSetup.TabIndex = 305;
            this.buttonSetup.Text = "Setup...";
            this.buttonSetup.UseVisualStyleBackColor = true;
            this.buttonSetup.Click += new System.EventHandler(this.buttonSetup_Click);
            // 
            // buttonSaveConciergeProcedure
            // 
            this.buttonSaveConciergeProcedure.Enabled = false;
            this.buttonSaveConciergeProcedure.Location = new System.Drawing.Point(12, 243);
            this.buttonSaveConciergeProcedure.Name = "buttonSaveConciergeProcedure";
            this.buttonSaveConciergeProcedure.Size = new System.Drawing.Size(161, 23);
            this.buttonSaveConciergeProcedure.TabIndex = 303;
            this.buttonSaveConciergeProcedure.Text = "Save Concierge Procedure...";
            this.buttonSaveConciergeProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeProcedure.Click += new System.EventHandler(this.buttonSaveConciergeProcedure_Click);
            // 
            // buttonSaveProcedure
            // 
            this.buttonSaveProcedure.Location = new System.Drawing.Point(179, 243);
            this.buttonSaveProcedure.Name = "buttonSaveProcedure";
            this.buttonSaveProcedure.Size = new System.Drawing.Size(127, 23);
            this.buttonSaveProcedure.TabIndex = 304;
            this.buttonSaveProcedure.Text = "Save Procedure...";
            this.buttonSaveProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveProcedure.Click += new System.EventHandler(this.buttonSaveProcedure_Click);
            // 
            // checkBoxView
            // 
            this.checkBoxView.AutoSize = true;
            this.checkBoxView.Location = new System.Drawing.Point(316, 247);
            this.checkBoxView.Name = "checkBoxView";
            this.checkBoxView.Size = new System.Drawing.Size(49, 17);
            this.checkBoxView.TabIndex = 306;
            this.checkBoxView.Text = "View";
            this.checkBoxView.UseVisualStyleBackColor = true;
            this.checkBoxView.CheckedChanged += new System.EventHandler(this.checkBoxView_CheckedChanged);
            // 
            // dragger1
            // 
            this.dragger1.BackColor = System.Drawing.SystemColors.Control;
            this.dragger1.Dragging = false;
            this.dragger1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dragger1.Full = false;
            this.dragger1.Location = new System.Drawing.Point(388, 33);
            this.dragger1.Name = "dragger1";
            this.dragger1.Size = new System.Drawing.Size(54, 182);
            this.dragger1.TabIndex = 308;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 278);
            this.Controls.Add(this.dragger1);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.buttonSaveProcedure);
            this.Controls.Add(this.buttonSaveConciergeProcedure);
            this.Controls.Add(this.buttonSetup);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "PDF Handler";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelProcedurePatientLastName;
        private System.Windows.Forms.Label labelProcedurePatientFirstName;
        private System.Windows.Forms.Label labelProcedureLocation;
        private System.Windows.Forms.Label labelProcedureDoctor;
        private System.Windows.Forms.Label labelProcedureDate;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsLastName;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsFirstName;
        private System.Windows.Forms.TextBox textBoxProcedureLocation;
        private System.Windows.Forms.TextBox textBoxProcedureDoctor;
        private System.Windows.Forms.DateTimePicker dateTimePickerProcedure;
        private System.Windows.Forms.TextBox textBoxProcedureDate;
        private System.Windows.Forms.Label labelProcedurePages;
        private System.Windows.Forms.TextBox textBoxProcedureName;
        private System.Windows.Forms.Label labelProcedureName;
        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.TextBox textBoxProcedurePages;
        private System.Windows.Forms.Button buttonSaveConciergeProcedure;
        private System.Windows.Forms.Button buttonSaveProcedure;
        private System.Windows.Forms.CheckBox checkBoxView;
        private Dragger dragger1;

    }
}

