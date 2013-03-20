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
            this.textBoxProcedurePatientsLastName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsFirstName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePages = new System.Windows.Forms.TextBox();
            this.labelProcedurePages = new System.Windows.Forms.Label();
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
            this.comboBoxSuffix = new System.Windows.Forms.ComboBox();
            this.comboBoxProcedureName = new System.Windows.Forms.ComboBox();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.comboBoxDoctor = new System.Windows.Forms.ComboBox();
            this.dragger1 = new FaxHandler.Dragger();
            this.buttonGetName = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGetName);
            this.groupBox1.Controls.Add(this.comboBoxDoctor);
            this.groupBox1.Controls.Add(this.comboBoxLocation);
            this.groupBox1.Controls.Add(this.comboBoxProcedureName);
            this.groupBox1.Controls.Add(this.comboBoxSuffix);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsLastName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePatientsFirstName);
            this.groupBox1.Controls.Add(this.textBoxProcedurePages);
            this.groupBox1.Controls.Add(this.labelProcedurePages);
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
            this.groupBox1.Size = new System.Drawing.Size(451, 220);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Procedure";
            // 
            // textBoxProcedurePatientsLastName
            // 
            this.textBoxProcedurePatientsLastName.Location = new System.Drawing.Point(148, 129);
            this.textBoxProcedurePatientsLastName.Name = "textBoxProcedurePatientsLastName";
            this.textBoxProcedurePatientsLastName.Size = new System.Drawing.Size(227, 20);
            this.textBoxProcedurePatientsLastName.TabIndex = 104;
            this.textBoxProcedurePatientsLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePatientsFirstName
            // 
            this.textBoxProcedurePatientsFirstName.Location = new System.Drawing.Point(148, 101);
            this.textBoxProcedurePatientsFirstName.Name = "textBoxProcedurePatientsFirstName";
            this.textBoxProcedurePatientsFirstName.Size = new System.Drawing.Size(227, 20);
            this.textBoxProcedurePatientsFirstName.TabIndex = 103;
            this.textBoxProcedurePatientsFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePages
            // 
            this.textBoxProcedurePages.Enabled = false;
            this.textBoxProcedurePages.Location = new System.Drawing.Point(148, 183);
            this.textBoxProcedurePages.Name = "textBoxProcedurePages";
            this.textBoxProcedurePages.Size = new System.Drawing.Size(297, 20);
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
            // dateTimePickerProcedure
            // 
            this.dateTimePickerProcedure.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerProcedure.Location = new System.Drawing.Point(326, 21);
            this.dateTimePickerProcedure.Name = "dateTimePickerProcedure";
            this.dateTimePickerProcedure.Size = new System.Drawing.Size(118, 20);
            this.dateTimePickerProcedure.TabIndex = 100;
            this.dateTimePickerProcedure.ValueChanged += new System.EventHandler(this.dateTimePickerProcedure_ValueChanged);
            // 
            // textBoxProcedureDate
            // 
            this.textBoxProcedureDate.Enabled = false;
            this.textBoxProcedureDate.Location = new System.Drawing.Point(148, 21);
            this.textBoxProcedureDate.Name = "textBoxProcedureDate";
            this.textBoxProcedureDate.Size = new System.Drawing.Size(128, 20);
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
            this.buttonSetup.Location = new System.Drawing.Point(469, 245);
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
            this.buttonSaveConciergeProcedure.Location = new System.Drawing.Point(35, 243);
            this.buttonSaveConciergeProcedure.Name = "buttonSaveConciergeProcedure";
            this.buttonSaveConciergeProcedure.Size = new System.Drawing.Size(161, 23);
            this.buttonSaveConciergeProcedure.TabIndex = 303;
            this.buttonSaveConciergeProcedure.Text = "Save Concierge Procedure...";
            this.buttonSaveConciergeProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeProcedure.Click += new System.EventHandler(this.buttonSaveConciergeProcedure_Click);
            // 
            // buttonSaveProcedure
            // 
            this.buttonSaveProcedure.Location = new System.Drawing.Point(205, 243);
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
            this.checkBoxView.Location = new System.Drawing.Point(338, 247);
            this.checkBoxView.Name = "checkBoxView";
            this.checkBoxView.Size = new System.Drawing.Size(49, 17);
            this.checkBoxView.TabIndex = 306;
            this.checkBoxView.Text = "View";
            this.checkBoxView.UseVisualStyleBackColor = true;
            this.checkBoxView.CheckedChanged += new System.EventHandler(this.checkBoxView_CheckedChanged);
            // 
            // comboBoxSuffix
            // 
            this.comboBoxSuffix.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSuffix.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxSuffix.FormattingEnabled = true;
            this.comboBoxSuffix.ItemHeight = 13;
            this.comboBoxSuffix.Location = new System.Drawing.Point(326, 50);
            this.comboBoxSuffix.MaxLength = 12;
            this.comboBoxSuffix.Name = "comboBoxSuffix";
            this.comboBoxSuffix.Size = new System.Drawing.Size(118, 21);
            this.comboBoxSuffix.TabIndex = 107;
            this.comboBoxSuffix.Enter += new System.EventHandler(this.comboBoxSuffix_Enter);
            // 
            // comboBoxProcedureName
            // 
            this.comboBoxProcedureName.FormattingEnabled = true;
            this.comboBoxProcedureName.Location = new System.Drawing.Point(148, 157);
            this.comboBoxProcedureName.Name = "comboBoxProcedureName";
            this.comboBoxProcedureName.Size = new System.Drawing.Size(296, 21);
            this.comboBoxProcedureName.TabIndex = 108;
            this.comboBoxProcedureName.Enter += new System.EventHandler(this.comboBoxProcedureName_Enter);
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Location = new System.Drawing.Point(148, 75);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(296, 21);
            this.comboBoxLocation.TabIndex = 109;
            this.comboBoxLocation.Enter += new System.EventHandler(this.comboBoxLocation_Enter);
            // 
            // comboBoxDoctor
            // 
            this.comboBoxDoctor.FormattingEnabled = true;
            this.comboBoxDoctor.Location = new System.Drawing.Point(148, 51);
            this.comboBoxDoctor.Name = "comboBoxDoctor";
            this.comboBoxDoctor.Size = new System.Drawing.Size(172, 21);
            this.comboBoxDoctor.TabIndex = 110;
            this.comboBoxDoctor.Enter += new System.EventHandler(this.comboBoxDoctor_Enter);
            // 
            // dragger1
            // 
            this.dragger1.BackColor = System.Drawing.SystemColors.Control;
            this.dragger1.Dragging = false;
            this.dragger1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dragger1.Full = false;
            this.dragger1.Location = new System.Drawing.Point(469, 33);
            this.dragger1.Name = "dragger1";
            this.dragger1.Size = new System.Drawing.Size(54, 182);
            this.dragger1.TabIndex = 308;
            // 
            // buttonGetName
            // 
            this.buttonGetName.Location = new System.Drawing.Point(381, 102);
            this.buttonGetName.Name = "buttonGetName";
            this.buttonGetName.Size = new System.Drawing.Size(63, 49);
            this.buttonGetName.TabIndex = 111;
            this.buttonGetName.Text = "Get Name...";
            this.buttonGetName.UseVisualStyleBackColor = true;
            this.buttonGetName.Click += new System.EventHandler(this.buttonGetName_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 278);
            this.Controls.Add(this.dragger1);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.buttonSaveProcedure);
            this.Controls.Add(this.buttonSaveConciergeProcedure);
            this.Controls.Add(this.buttonSetup);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
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
        private System.Windows.Forms.DateTimePicker dateTimePickerProcedure;
        private System.Windows.Forms.TextBox textBoxProcedureDate;
        private System.Windows.Forms.Label labelProcedurePages;
        private System.Windows.Forms.Label labelProcedureName;
        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.TextBox textBoxProcedurePages;
        private System.Windows.Forms.Button buttonSaveConciergeProcedure;
        private System.Windows.Forms.Button buttonSaveProcedure;
        private System.Windows.Forms.CheckBox checkBoxView;
        private Dragger dragger1;
        private System.Windows.Forms.ComboBox comboBoxSuffix;
        private System.Windows.Forms.ComboBox comboBoxProcedureName;
        private System.Windows.Forms.ComboBox comboBoxDoctor;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.Button buttonGetName;

    }
}

