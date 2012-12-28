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
            this.groupBoxConsult = new System.Windows.Forms.GroupBox();
            this.textBoxConsultPatientsLastName = new System.Windows.Forms.TextBox();
            this.textBoxConsultPatientsFirstName = new System.Windows.Forms.TextBox();
            this.textBoxConsultLocation = new System.Windows.Forms.TextBox();
            this.textBoxConsultDoctor = new System.Windows.Forms.TextBox();
            this.dateTimePickerConsult = new System.Windows.Forms.DateTimePicker();
            this.textBoxConsultDate = new System.Windows.Forms.TextBox();
            this.labelConsultPatientLastName = new System.Windows.Forms.Label();
            this.labelConsultPatientFirstName = new System.Windows.Forms.Label();
            this.labelConsultLocation = new System.Windows.Forms.Label();
            this.labelConsultDoctor = new System.Windows.Forms.Label();
            this.labelConsultDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxProcedureName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsLastName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsFirstName = new System.Windows.Forms.TextBox();
            this.textBoxProcedureLocation = new System.Windows.Forms.TextBox();
            this.textBoxProcedureDoctor = new System.Windows.Forms.TextBox();
            this.dateTimePickerProcedure = new System.Windows.Forms.DateTimePicker();
            this.textBoxProcedureDate = new System.Windows.Forms.TextBox();
            this.labelProcedureName = new System.Windows.Forms.Label();
            this.labelProcedurePatientLastName = new System.Windows.Forms.Label();
            this.labelProcedurePatientFirstName = new System.Windows.Forms.Label();
            this.labelProcedureLocation = new System.Windows.Forms.Label();
            this.labelProcedureDoctor = new System.Windows.Forms.Label();
            this.labelProcedureDate = new System.Windows.Forms.Label();
            this.labelConsultPages = new System.Windows.Forms.Label();
            this.labelProcedurePages = new System.Windows.Forms.Label();
            this.buttonSetup = new System.Windows.Forms.Button();
            this.buttonCopyToConsult = new System.Windows.Forms.Button();
            this.buttonCopyToProcedure = new System.Windows.Forms.Button();
            this.textBoxConsultPages = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePages = new System.Windows.Forms.TextBox();
            this.buttonSaveConciergeConsult = new System.Windows.Forms.Button();
            this.buttonSaveConciergeProcedure = new System.Windows.Forms.Button();
            this.buttonSaveConsult = new System.Windows.Forms.Button();
            this.buttonSaveProcedure = new System.Windows.Forms.Button();
            this.checkBoxView = new System.Windows.Forms.CheckBox();
            this.groupBoxConsult.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConsult
            // 
            this.groupBoxConsult.Controls.Add(this.textBoxConsultPatientsLastName);
            this.groupBoxConsult.Controls.Add(this.textBoxConsultPatientsFirstName);
            this.groupBoxConsult.Controls.Add(this.textBoxConsultLocation);
            this.groupBoxConsult.Controls.Add(this.textBoxConsultDoctor);
            this.groupBoxConsult.Controls.Add(this.dateTimePickerConsult);
            this.groupBoxConsult.Controls.Add(this.textBoxConsultDate);
            this.groupBoxConsult.Controls.Add(this.labelConsultPatientLastName);
            this.groupBoxConsult.Controls.Add(this.labelConsultPatientFirstName);
            this.groupBoxConsult.Controls.Add(this.labelConsultLocation);
            this.groupBoxConsult.Controls.Add(this.labelConsultDoctor);
            this.groupBoxConsult.Controls.Add(this.labelConsultDate);
            this.groupBoxConsult.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConsult.Name = "groupBoxConsult";
            this.groupBoxConsult.Size = new System.Drawing.Size(359, 158);
            this.groupBoxConsult.TabIndex = 0;
            this.groupBoxConsult.TabStop = false;
            this.groupBoxConsult.Text = "Consult";
            // 
            // textBoxConsultPatientsLastName
            // 
            this.textBoxConsultPatientsLastName.Location = new System.Drawing.Point(136, 129);
            this.textBoxConsultPatientsLastName.Name = "textBoxConsultPatientsLastName";
            this.textBoxConsultPatientsLastName.Size = new System.Drawing.Size(205, 20);
            this.textBoxConsultPatientsLastName.TabIndex = 5;
            this.textBoxConsultPatientsLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxConsultPatientsFirstName
            // 
            this.textBoxConsultPatientsFirstName.Location = new System.Drawing.Point(136, 102);
            this.textBoxConsultPatientsFirstName.Name = "textBoxConsultPatientsFirstName";
            this.textBoxConsultPatientsFirstName.Size = new System.Drawing.Size(205, 20);
            this.textBoxConsultPatientsFirstName.TabIndex = 4;
            this.textBoxConsultPatientsFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxConsultLocation
            // 
            this.textBoxConsultLocation.Location = new System.Drawing.Point(136, 75);
            this.textBoxConsultLocation.Name = "textBoxConsultLocation";
            this.textBoxConsultLocation.Size = new System.Drawing.Size(205, 20);
            this.textBoxConsultLocation.TabIndex = 3;
            this.textBoxConsultLocation.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxConsultDoctor
            // 
            this.textBoxConsultDoctor.Location = new System.Drawing.Point(136, 48);
            this.textBoxConsultDoctor.Name = "textBoxConsultDoctor";
            this.textBoxConsultDoctor.Size = new System.Drawing.Size(205, 20);
            this.textBoxConsultDoctor.TabIndex = 2;
            this.textBoxConsultDoctor.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // dateTimePickerConsult
            // 
            this.dateTimePickerConsult.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerConsult.Location = new System.Drawing.Point(237, 21);
            this.dateTimePickerConsult.Name = "dateTimePickerConsult";
            this.dateTimePickerConsult.Size = new System.Drawing.Size(104, 20);
            this.dateTimePickerConsult.TabIndex = 1;
            this.dateTimePickerConsult.ValueChanged += new System.EventHandler(this.dateTimePickerConsult_ValueChanged);
            // 
            // textBoxConsultDate
            // 
            this.textBoxConsultDate.Enabled = false;
            this.textBoxConsultDate.Location = new System.Drawing.Point(136, 21);
            this.textBoxConsultDate.Name = "textBoxConsultDate";
            this.textBoxConsultDate.Size = new System.Drawing.Size(95, 20);
            this.textBoxConsultDate.TabIndex = 5;
            // 
            // labelConsultPatientLastName
            // 
            this.labelConsultPatientLastName.AutoSize = true;
            this.labelConsultPatientLastName.Location = new System.Drawing.Point(19, 132);
            this.labelConsultPatientLastName.Name = "labelConsultPatientLastName";
            this.labelConsultPatientLastName.Size = new System.Drawing.Size(101, 13);
            this.labelConsultPatientLastName.TabIndex = 4;
            this.labelConsultPatientLastName.Text = "Patient\'s Last Name";
            // 
            // labelConsultPatientFirstName
            // 
            this.labelConsultPatientFirstName.AutoSize = true;
            this.labelConsultPatientFirstName.Location = new System.Drawing.Point(19, 105);
            this.labelConsultPatientFirstName.Name = "labelConsultPatientFirstName";
            this.labelConsultPatientFirstName.Size = new System.Drawing.Size(100, 13);
            this.labelConsultPatientFirstName.TabIndex = 3;
            this.labelConsultPatientFirstName.Text = "Patient\'s First Name";
            // 
            // labelConsultLocation
            // 
            this.labelConsultLocation.AutoSize = true;
            this.labelConsultLocation.Location = new System.Drawing.Point(19, 78);
            this.labelConsultLocation.Name = "labelConsultLocation";
            this.labelConsultLocation.Size = new System.Drawing.Size(48, 13);
            this.labelConsultLocation.TabIndex = 2;
            this.labelConsultLocation.Text = "Location";
            // 
            // labelConsultDoctor
            // 
            this.labelConsultDoctor.AutoSize = true;
            this.labelConsultDoctor.Location = new System.Drawing.Point(19, 51);
            this.labelConsultDoctor.Name = "labelConsultDoctor";
            this.labelConsultDoctor.Size = new System.Drawing.Size(39, 13);
            this.labelConsultDoctor.TabIndex = 1;
            this.labelConsultDoctor.Text = "Doctor";
            // 
            // labelConsultDate
            // 
            this.labelConsultDate.AutoSize = true;
            this.labelConsultDate.Location = new System.Drawing.Point(19, 24);
            this.labelConsultDate.Name = "labelConsultDate";
            this.labelConsultDate.Size = new System.Drawing.Size(30, 13);
            this.labelConsultDate.TabIndex = 0;
            this.labelConsultDate.Text = "Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxProcedureName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsLastName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsFirstName);
            this.groupBox1.Controls.Add(this.textBoxProcedureLocation);
            this.groupBox1.Controls.Add(this.textBoxProcedureDoctor);
            this.groupBox1.Controls.Add(this.dateTimePickerProcedure);
            this.groupBox1.Controls.Add(this.textBoxProcedureDate);
            this.groupBox1.Controls.Add(this.labelProcedureName);
            this.groupBox1.Controls.Add(this.labelProcedurePatientLastName);
            this.groupBox1.Controls.Add(this.labelProcedurePatientFirstName);
            this.groupBox1.Controls.Add(this.labelProcedureLocation);
            this.groupBox1.Controls.Add(this.labelProcedureDoctor);
            this.groupBox1.Controls.Add(this.labelProcedureDate);
            this.groupBox1.Location = new System.Drawing.Point(377, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 190);
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
            // labelConsultPages
            // 
            this.labelConsultPages.AutoSize = true;
            this.labelConsultPages.Location = new System.Drawing.Point(31, 220);
            this.labelConsultPages.Name = "labelConsultPages";
            this.labelConsultPages.Size = new System.Drawing.Size(43, 13);
            this.labelConsultPages.TabIndex = 2;
            this.labelConsultPages.Text = "Page(s)";
            // 
            // labelProcedurePages
            // 
            this.labelProcedurePages.AutoSize = true;
            this.labelProcedurePages.Location = new System.Drawing.Point(396, 220);
            this.labelProcedurePages.Name = "labelProcedurePages";
            this.labelProcedurePages.Size = new System.Drawing.Size(43, 13);
            this.labelProcedurePages.TabIndex = 2;
            this.labelProcedurePages.Text = "Page(s)";
            // 
            // buttonSetup
            // 
            this.buttonSetup.Location = new System.Drawing.Point(675, 255);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(55, 23);
            this.buttonSetup.TabIndex = 305;
            this.buttonSetup.Text = "Setup...";
            this.buttonSetup.UseVisualStyleBackColor = true;
            this.buttonSetup.Click += new System.EventHandler(this.buttonSetup_Click);
            // 
            // buttonCopyToConsult
            // 
            this.buttonCopyToConsult.Location = new System.Drawing.Point(101, 176);
            this.buttonCopyToConsult.Name = "buttonCopyToConsult";
            this.buttonCopyToConsult.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyToConsult.TabIndex = 201;
            this.buttonCopyToConsult.Text = "<< Copy";
            this.buttonCopyToConsult.UseVisualStyleBackColor = true;
            this.buttonCopyToConsult.Click += new System.EventHandler(this.buttonCopyToConsult_Click);
            // 
            // buttonCopyToProcedure
            // 
            this.buttonCopyToProcedure.Location = new System.Drawing.Point(202, 176);
            this.buttonCopyToProcedure.Name = "buttonCopyToProcedure";
            this.buttonCopyToProcedure.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyToProcedure.TabIndex = 202;
            this.buttonCopyToProcedure.Text = "Copy >>";
            this.buttonCopyToProcedure.UseVisualStyleBackColor = true;
            this.buttonCopyToProcedure.Click += new System.EventHandler(this.buttonCopyToProcedure_Click);
            // 
            // textBoxConsultPages
            // 
            this.textBoxConsultPages.Enabled = false;
            this.textBoxConsultPages.Location = new System.Drawing.Point(148, 217);
            this.textBoxConsultPages.Name = "textBoxConsultPages";
            this.textBoxConsultPages.Size = new System.Drawing.Size(205, 20);
            this.textBoxConsultPages.TabIndex = 6;
            this.textBoxConsultPages.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPages_Validating);
            // 
            // textBoxProcedurePages
            // 
            this.textBoxProcedurePages.Enabled = false;
            this.textBoxProcedurePages.Location = new System.Drawing.Point(525, 217);
            this.textBoxProcedurePages.Name = "textBoxProcedurePages";
            this.textBoxProcedurePages.Size = new System.Drawing.Size(205, 20);
            this.textBoxProcedurePages.TabIndex = 106;
            // 
            // buttonSaveConciergeConsult
            // 
            this.buttonSaveConciergeConsult.Enabled = false;
            this.buttonSaveConciergeConsult.Location = new System.Drawing.Point(12, 255);
            this.buttonSaveConciergeConsult.Name = "buttonSaveConciergeConsult";
            this.buttonSaveConciergeConsult.Size = new System.Drawing.Size(142, 23);
            this.buttonSaveConciergeConsult.TabIndex = 301;
            this.buttonSaveConciergeConsult.Text = "Save Concierge Consult...";
            this.buttonSaveConciergeConsult.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeConsult.Click += new System.EventHandler(this.buttonSaveConciergeConsult_Click);
            // 
            // buttonSaveConciergeProcedure
            // 
            this.buttonSaveConciergeProcedure.Enabled = false;
            this.buttonSaveConciergeProcedure.Location = new System.Drawing.Point(377, 255);
            this.buttonSaveConciergeProcedure.Name = "buttonSaveConciergeProcedure";
            this.buttonSaveConciergeProcedure.Size = new System.Drawing.Size(161, 23);
            this.buttonSaveConciergeProcedure.TabIndex = 303;
            this.buttonSaveConciergeProcedure.Text = "Save Concierge Procedure...";
            this.buttonSaveConciergeProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeProcedure.Click += new System.EventHandler(this.buttonSaveConciergeProcedure_Click);
            // 
            // buttonSaveConsult
            // 
            this.buttonSaveConsult.Location = new System.Drawing.Point(160, 255);
            this.buttonSaveConsult.Name = "buttonSaveConsult";
            this.buttonSaveConsult.Size = new System.Drawing.Size(127, 23);
            this.buttonSaveConsult.TabIndex = 302;
            this.buttonSaveConsult.Text = "Save Consult...";
            this.buttonSaveConsult.UseVisualStyleBackColor = true;
            this.buttonSaveConsult.Click += new System.EventHandler(this.buttonSaveConsult_Click);
            // 
            // buttonSaveProcedure
            // 
            this.buttonSaveProcedure.Location = new System.Drawing.Point(544, 255);
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
            this.checkBoxView.Location = new System.Drawing.Point(313, 259);
            this.checkBoxView.Name = "checkBoxView";
            this.checkBoxView.Size = new System.Drawing.Size(49, 17);
            this.checkBoxView.TabIndex = 306;
            this.checkBoxView.Text = "View";
            this.checkBoxView.UseVisualStyleBackColor = true;
            this.checkBoxView.CheckedChanged += new System.EventHandler(this.checkBoxView_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 300);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.buttonSaveProcedure);
            this.Controls.Add(this.buttonSaveConsult);
            this.Controls.Add(this.buttonSaveConciergeProcedure);
            this.Controls.Add(this.buttonSaveConciergeConsult);
            this.Controls.Add(this.textBoxProcedurePages);
            this.Controls.Add(this.textBoxConsultPages);
            this.Controls.Add(this.buttonCopyToProcedure);
            this.Controls.Add(this.buttonCopyToConsult);
            this.Controls.Add(this.buttonSetup);
            this.Controls.Add(this.labelProcedurePages);
            this.Controls.Add(this.labelConsultPages);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxConsult);
            this.Name = "MainForm";
            this.Text = "Fax Handler";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxConsult.ResumeLayout(false);
            this.groupBoxConsult.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConsult;
        private System.Windows.Forms.Label labelConsultPatientLastName;
        private System.Windows.Forms.Label labelConsultPatientFirstName;
        private System.Windows.Forms.Label labelConsultLocation;
        private System.Windows.Forms.Label labelConsultDoctor;
        private System.Windows.Forms.Label labelConsultDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelProcedurePatientLastName;
        private System.Windows.Forms.Label labelProcedurePatientFirstName;
        private System.Windows.Forms.Label labelProcedureLocation;
        private System.Windows.Forms.Label labelProcedureDoctor;
        private System.Windows.Forms.Label labelProcedureDate;
        private System.Windows.Forms.TextBox textBoxConsultPatientsLastName;
        private System.Windows.Forms.TextBox textBoxConsultPatientsFirstName;
        private System.Windows.Forms.TextBox textBoxConsultLocation;
        private System.Windows.Forms.TextBox textBoxConsultDoctor;
        private System.Windows.Forms.DateTimePicker dateTimePickerConsult;
        private System.Windows.Forms.TextBox textBoxConsultDate;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsLastName;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsFirstName;
        private System.Windows.Forms.TextBox textBoxProcedureLocation;
        private System.Windows.Forms.TextBox textBoxProcedureDoctor;
        private System.Windows.Forms.DateTimePicker dateTimePickerProcedure;
        private System.Windows.Forms.TextBox textBoxProcedureDate;
        private System.Windows.Forms.Label labelConsultPages;
        private System.Windows.Forms.Label labelProcedurePages;
        private System.Windows.Forms.TextBox textBoxProcedureName;
        private System.Windows.Forms.Label labelProcedureName;
        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.Button buttonCopyToConsult;
        private System.Windows.Forms.Button buttonCopyToProcedure;
        private System.Windows.Forms.TextBox textBoxConsultPages;
        private System.Windows.Forms.TextBox textBoxProcedurePages;
        private System.Windows.Forms.Button buttonSaveConciergeConsult;
        private System.Windows.Forms.Button buttonSaveConciergeProcedure;
        private System.Windows.Forms.Button buttonSaveConsult;
        private System.Windows.Forms.Button buttonSaveProcedure;
        private System.Windows.Forms.CheckBox checkBoxView;

    }
}

