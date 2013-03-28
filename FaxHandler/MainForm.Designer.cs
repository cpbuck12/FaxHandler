﻿namespace FaxHandler
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
            this.buttonSaveConciergeProcedure = new System.Windows.Forms.Button();
            this.buttonSaveProcedure = new System.Windows.Forms.Button();
            this.checkBoxView = new System.Windows.Forms.CheckBox();
            this.labelProcedureDate = new System.Windows.Forms.Label();
            this.labelProcedureLocation = new System.Windows.Forms.Label();
            this.labelProcedurePatientFirstName = new System.Windows.Forms.Label();
            this.labelProcedurePatientLastName = new System.Windows.Forms.Label();
            this.textBoxProcedureDate = new System.Windows.Forms.TextBox();
            this.dateTimePickerProcedure = new System.Windows.Forms.DateTimePicker();
            this.labelProcedurePages = new System.Windows.Forms.Label();
            this.textBoxProcedurePages = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsFirstName = new System.Windows.Forms.TextBox();
            this.textBoxProcedurePatientsLastName = new System.Windows.Forms.TextBox();
            this.comboBoxSuffix = new System.Windows.Forms.ComboBox();
            this.comboBoxProcedureName = new System.Windows.Forms.ComboBox();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.comboBoxDoctor = new System.Windows.Forms.ComboBox();
            this.groupBoxProcedure = new System.Windows.Forms.GroupBox();
            this.labelProcedureName = new System.Windows.Forms.Label();
            this.groupBoxPatient = new System.Windows.Forms.GroupBox();
            this.comboBoxPatients = new System.Windows.Forms.ComboBox();
            this.groupBoxPractitioner = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPractitionerName = new System.Windows.Forms.Label();
            this.buttonGetSignature = new System.Windows.Forms.Button();
            this.buttonCreateRelease = new System.Windows.Forms.Button();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelSpecialty = new System.Windows.Forms.Label();
            this.comboBoxSpecialty = new System.Windows.Forms.ComboBox();
            this.dragger1 = new FaxHandler.Dragger();
            this.groupBoxProcedure.SuspendLayout();
            this.groupBoxPatient.SuspendLayout();
            this.groupBoxPractitioner.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveConciergeProcedure
            // 
            this.buttonSaveConciergeProcedure.Enabled = false;
            this.buttonSaveConciergeProcedure.Location = new System.Drawing.Point(15, 351);
            this.buttonSaveConciergeProcedure.Name = "buttonSaveConciergeProcedure";
            this.buttonSaveConciergeProcedure.Size = new System.Drawing.Size(161, 23);
            this.buttonSaveConciergeProcedure.TabIndex = 303;
            this.buttonSaveConciergeProcedure.Text = "Save Concierge Procedure...";
            this.buttonSaveConciergeProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeProcedure.Click += new System.EventHandler(this.buttonSaveConciergeProcedure_Click);
            // 
            // buttonSaveProcedure
            // 
            this.buttonSaveProcedure.Location = new System.Drawing.Point(182, 351);
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
            this.checkBoxView.Location = new System.Drawing.Point(315, 357);
            this.checkBoxView.Name = "checkBoxView";
            this.checkBoxView.Size = new System.Drawing.Size(49, 17);
            this.checkBoxView.TabIndex = 306;
            this.checkBoxView.Text = "View";
            this.checkBoxView.UseVisualStyleBackColor = true;
            this.checkBoxView.CheckedChanged += new System.EventHandler(this.checkBoxView_CheckedChanged);
            // 
            // labelProcedureDate
            // 
            this.labelProcedureDate.AutoSize = true;
            this.labelProcedureDate.Location = new System.Drawing.Point(6, 16);
            this.labelProcedureDate.Name = "labelProcedureDate";
            this.labelProcedureDate.Size = new System.Drawing.Size(30, 13);
            this.labelProcedureDate.TabIndex = 0;
            this.labelProcedureDate.Text = "Date";
            // 
            // labelProcedureLocation
            // 
            this.labelProcedureLocation.AutoSize = true;
            this.labelProcedureLocation.Location = new System.Drawing.Point(6, 42);
            this.labelProcedureLocation.Name = "labelProcedureLocation";
            this.labelProcedureLocation.Size = new System.Drawing.Size(48, 13);
            this.labelProcedureLocation.TabIndex = 3;
            this.labelProcedureLocation.Text = "Location";
            // 
            // labelProcedurePatientFirstName
            // 
            this.labelProcedurePatientFirstName.AutoSize = true;
            this.labelProcedurePatientFirstName.Location = new System.Drawing.Point(6, 19);
            this.labelProcedurePatientFirstName.Name = "labelProcedurePatientFirstName";
            this.labelProcedurePatientFirstName.Size = new System.Drawing.Size(26, 13);
            this.labelProcedurePatientFirstName.TabIndex = 4;
            this.labelProcedurePatientFirstName.Text = "First";
            this.labelProcedurePatientFirstName.Click += new System.EventHandler(this.labelProcedurePatientFirstName_Click);
            // 
            // labelProcedurePatientLastName
            // 
            this.labelProcedurePatientLastName.AutoSize = true;
            this.labelProcedurePatientLastName.Location = new System.Drawing.Point(6, 41);
            this.labelProcedurePatientLastName.Name = "labelProcedurePatientLastName";
            this.labelProcedurePatientLastName.Size = new System.Drawing.Size(27, 13);
            this.labelProcedurePatientLastName.TabIndex = 5;
            this.labelProcedurePatientLastName.Text = "Last";
            // 
            // textBoxProcedureDate
            // 
            this.textBoxProcedureDate.Enabled = false;
            this.textBoxProcedureDate.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedureDate.Location = new System.Drawing.Point(71, 13);
            this.textBoxProcedureDate.Name = "textBoxProcedureDate";
            this.textBoxProcedureDate.Size = new System.Drawing.Size(174, 22);
            this.textBoxProcedureDate.TabIndex = 5;
            // 
            // dateTimePickerProcedure
            // 
            this.dateTimePickerProcedure.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerProcedure.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerProcedure.Location = new System.Drawing.Point(300, 11);
            this.dateTimePickerProcedure.Name = "dateTimePickerProcedure";
            this.dateTimePickerProcedure.Size = new System.Drawing.Size(140, 22);
            this.dateTimePickerProcedure.TabIndex = 100;
            this.dateTimePickerProcedure.ValueChanged += new System.EventHandler(this.dateTimePickerProcedure_ValueChanged);
            // 
            // labelProcedurePages
            // 
            this.labelProcedurePages.AutoSize = true;
            this.labelProcedurePages.Location = new System.Drawing.Point(6, 96);
            this.labelProcedurePages.Name = "labelProcedurePages";
            this.labelProcedurePages.Size = new System.Drawing.Size(43, 13);
            this.labelProcedurePages.TabIndex = 2;
            this.labelProcedurePages.Text = "Page(s)";
            // 
            // textBoxProcedurePages
            // 
            this.textBoxProcedurePages.Enabled = false;
            this.textBoxProcedurePages.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedurePages.Location = new System.Drawing.Point(71, 93);
            this.textBoxProcedurePages.Name = "textBoxProcedurePages";
            this.textBoxProcedurePages.Size = new System.Drawing.Size(369, 22);
            this.textBoxProcedurePages.TabIndex = 106;
            // 
            // textBoxProcedurePatientsFirstName
            // 
            this.textBoxProcedurePatientsFirstName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedurePatientsFirstName.Location = new System.Drawing.Point(70, 12);
            this.textBoxProcedurePatientsFirstName.MaxLength = 50;
            this.textBoxProcedurePatientsFirstName.Name = "textBoxProcedurePatientsFirstName";
            this.textBoxProcedurePatientsFirstName.Size = new System.Drawing.Size(370, 22);
            this.textBoxProcedurePatientsFirstName.TabIndex = 103;
            this.textBoxProcedurePatientsFirstName.TextChanged += new System.EventHandler(this.textBoxProcedurePatientsFirstName_TextChanged);
            this.textBoxProcedurePatientsFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // textBoxProcedurePatientsLastName
            // 
            this.textBoxProcedurePatientsLastName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedurePatientsLastName.Location = new System.Drawing.Point(71, 38);
            this.textBoxProcedurePatientsLastName.MaxLength = 50;
            this.textBoxProcedurePatientsLastName.Name = "textBoxProcedurePatientsLastName";
            this.textBoxProcedurePatientsLastName.Size = new System.Drawing.Size(369, 22);
            this.textBoxProcedurePatientsLastName.TabIndex = 104;
            this.textBoxProcedurePatientsLastName.TextChanged += new System.EventHandler(this.textBoxProcedurePatientsLastName_TextChanged);
            this.textBoxProcedurePatientsLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // comboBoxSuffix
            // 
            this.comboBoxSuffix.DropDownHeight = 400;
            this.comboBoxSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSuffix.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxSuffix.FormattingEnabled = true;
            this.comboBoxSuffix.IntegralHeight = false;
            this.comboBoxSuffix.ItemHeight = 13;
            this.comboBoxSuffix.Location = new System.Drawing.Point(71, 44);
            this.comboBoxSuffix.MaxLength = 12;
            this.comboBoxSuffix.Name = "comboBoxSuffix";
            this.comboBoxSuffix.Size = new System.Drawing.Size(172, 21);
            this.comboBoxSuffix.TabIndex = 107;
            this.comboBoxSuffix.SelectedIndexChanged += new System.EventHandler(this.comboBoxSuffix_SelectedIndexChanged);
            this.comboBoxSuffix.Enter += new System.EventHandler(this.comboBoxSuffix_Enter);
            // 
            // comboBoxProcedureName
            // 
            this.comboBoxProcedureName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxProcedureName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProcedureName.DropDownHeight = 400;
            this.comboBoxProcedureName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxProcedureName.FormattingEnabled = true;
            this.comboBoxProcedureName.IntegralHeight = false;
            this.comboBoxProcedureName.ItemHeight = 16;
            this.comboBoxProcedureName.Location = new System.Drawing.Point(71, 66);
            this.comboBoxProcedureName.MaxLength = 50;
            this.comboBoxProcedureName.Name = "comboBoxProcedureName";
            this.comboBoxProcedureName.Size = new System.Drawing.Size(369, 24);
            this.comboBoxProcedureName.Sorted = true;
            this.comboBoxProcedureName.TabIndex = 108;
            this.comboBoxProcedureName.Enter += new System.EventHandler(this.comboBoxProcedureName_Enter);
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLocation.DropDownHeight = 400;
            this.comboBoxLocation.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.IntegralHeight = false;
            this.comboBoxLocation.ItemHeight = 16;
            this.comboBoxLocation.Location = new System.Drawing.Point(71, 39);
            this.comboBoxLocation.MaxLength = 50;
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(369, 24);
            this.comboBoxLocation.Sorted = true;
            this.comboBoxLocation.TabIndex = 109;
            this.comboBoxLocation.Enter += new System.EventHandler(this.comboBoxLocation_Enter);
            // 
            // comboBoxDoctor
            // 
            this.comboBoxDoctor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDoctor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDoctor.DropDownHeight = 400;
            this.comboBoxDoctor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDoctor.FormattingEnabled = true;
            this.comboBoxDoctor.IntegralHeight = false;
            this.comboBoxDoctor.ItemHeight = 16;
            this.comboBoxDoctor.Location = new System.Drawing.Point(71, 12);
            this.comboBoxDoctor.MaxDropDownItems = 7;
            this.comboBoxDoctor.MaxLength = 50;
            this.comboBoxDoctor.Name = "comboBoxDoctor";
            this.comboBoxDoctor.Size = new System.Drawing.Size(369, 24);
            this.comboBoxDoctor.Sorted = true;
            this.comboBoxDoctor.TabIndex = 110;
            this.comboBoxDoctor.Enter += new System.EventHandler(this.comboBoxDoctor_Enter);
            // 
            // groupBoxProcedure
            // 
            this.groupBoxProcedure.Controls.Add(this.comboBoxSpecialty);
            this.groupBoxProcedure.Controls.Add(this.labelSpecialty);
            this.groupBoxProcedure.Controls.Add(this.labelProcedureName);
            this.groupBoxProcedure.Controls.Add(this.textBoxProcedureDate);
            this.groupBoxProcedure.Controls.Add(this.dateTimePickerProcedure);
            this.groupBoxProcedure.Controls.Add(this.labelProcedureDate);
            this.groupBoxProcedure.Controls.Add(this.textBoxProcedurePages);
            this.groupBoxProcedure.Controls.Add(this.labelProcedureLocation);
            this.groupBoxProcedure.Controls.Add(this.comboBoxLocation);
            this.groupBoxProcedure.Controls.Add(this.comboBoxProcedureName);
            this.groupBoxProcedure.Controls.Add(this.labelProcedurePages);
            this.groupBoxProcedure.Location = new System.Drawing.Point(15, 12);
            this.groupBoxProcedure.Name = "groupBoxProcedure";
            this.groupBoxProcedure.Size = new System.Drawing.Size(446, 151);
            this.groupBoxProcedure.TabIndex = 309;
            this.groupBoxProcedure.TabStop = false;
            this.groupBoxProcedure.Text = "Procedure";
            // 
            // labelProcedureName
            // 
            this.labelProcedureName.AutoSize = true;
            this.labelProcedureName.Location = new System.Drawing.Point(6, 69);
            this.labelProcedureName.Name = "labelProcedureName";
            this.labelProcedureName.Size = new System.Drawing.Size(35, 13);
            this.labelProcedureName.TabIndex = 313;
            this.labelProcedureName.Text = "Name";
            // 
            // groupBoxPatient
            // 
            this.groupBoxPatient.Controls.Add(this.comboBoxPatients);
            this.groupBoxPatient.Controls.Add(this.labelProcedurePatientFirstName);
            this.groupBoxPatient.Controls.Add(this.labelProcedurePatientLastName);
            this.groupBoxPatient.Controls.Add(this.textBoxProcedurePatientsFirstName);
            this.groupBoxPatient.Controls.Add(this.textBoxProcedurePatientsLastName);
            this.groupBoxPatient.Location = new System.Drawing.Point(15, 247);
            this.groupBoxPatient.Name = "groupBoxPatient";
            this.groupBoxPatient.Size = new System.Drawing.Size(446, 98);
            this.groupBoxPatient.TabIndex = 312;
            this.groupBoxPatient.TabStop = false;
            this.groupBoxPatient.Text = "Patient";
            // 
            // comboBoxPatients
            // 
            this.comboBoxPatients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxPatients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPatients.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPatients.DropDownHeight = 400;
            this.comboBoxPatients.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPatients.FormattingEnabled = true;
            this.comboBoxPatients.IntegralHeight = false;
            this.comboBoxPatients.Location = new System.Drawing.Point(70, 64);
            this.comboBoxPatients.MaxLength = 100;
            this.comboBoxPatients.Name = "comboBoxPatients";
            this.comboBoxPatients.Size = new System.Drawing.Size(370, 23);
            this.comboBoxPatients.Sorted = true;
            this.comboBoxPatients.TabIndex = 314;
            this.comboBoxPatients.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxPatients_DrawItem);
            this.comboBoxPatients.SelectedIndexChanged += new System.EventHandler(this.comboBoxPatients_SelectedIndexChanged);
            this.comboBoxPatients.Enter += new System.EventHandler(this.comboBoxPatients_Enter);
            // 
            // groupBoxPractitioner
            // 
            this.groupBoxPractitioner.Controls.Add(this.label1);
            this.groupBoxPractitioner.Controls.Add(this.labelPractitionerName);
            this.groupBoxPractitioner.Controls.Add(this.comboBoxDoctor);
            this.groupBoxPractitioner.Controls.Add(this.comboBoxSuffix);
            this.groupBoxPractitioner.Location = new System.Drawing.Point(15, 169);
            this.groupBoxPractitioner.Name = "groupBoxPractitioner";
            this.groupBoxPractitioner.Size = new System.Drawing.Size(446, 72);
            this.groupBoxPractitioner.TabIndex = 313;
            this.groupBoxPractitioner.TabStop = false;
            this.groupBoxPractitioner.Text = "Practitioner";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 111;
            this.label1.Text = "Append";
            // 
            // labelPractitionerName
            // 
            this.labelPractitionerName.AutoSize = true;
            this.labelPractitionerName.Location = new System.Drawing.Point(6, 15);
            this.labelPractitionerName.Name = "labelPractitionerName";
            this.labelPractitionerName.Size = new System.Drawing.Size(35, 13);
            this.labelPractitionerName.TabIndex = 0;
            this.labelPractitionerName.Text = "Name";
            // 
            // buttonGetSignature
            // 
            this.buttonGetSignature.Location = new System.Drawing.Point(15, 380);
            this.buttonGetSignature.Name = "buttonGetSignature";
            this.buttonGetSignature.Size = new System.Drawing.Size(127, 23);
            this.buttonGetSignature.TabIndex = 314;
            this.buttonGetSignature.Text = "Get Signature...";
            this.buttonGetSignature.UseVisualStyleBackColor = true;
            this.buttonGetSignature.Click += new System.EventHandler(this.buttonGetSignature_Click);
            // 
            // buttonCreateRelease
            // 
            this.buttonCreateRelease.Location = new System.Drawing.Point(161, 380);
            this.buttonCreateRelease.Name = "buttonCreateRelease";
            this.buttonCreateRelease.Size = new System.Drawing.Size(116, 23);
            this.buttonCreateRelease.TabIndex = 315;
            this.buttonCreateRelease.Text = "Create Release...";
            this.buttonCreateRelease.UseVisualStyleBackColor = true;
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(15, 408);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(446, 93);
            this.textBoxStatus.TabIndex = 316;
            this.textBoxStatus.TabStop = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(467, 410);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(54, 91);
            this.buttonClear.TabIndex = 317;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelSpecialty
            // 
            this.labelSpecialty.AutoSize = true;
            this.labelSpecialty.Location = new System.Drawing.Point(6, 124);
            this.labelSpecialty.Name = "labelSpecialty";
            this.labelSpecialty.Size = new System.Drawing.Size(50, 13);
            this.labelSpecialty.TabIndex = 314;
            this.labelSpecialty.Text = "Specialty";
            // 
            // comboBoxSpecialty
            // 
            this.comboBoxSpecialty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSpecialty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSpecialty.DropDownHeight = 400;
            this.comboBoxSpecialty.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSpecialty.FormattingEnabled = true;
            this.comboBoxSpecialty.IntegralHeight = false;
            this.comboBoxSpecialty.ItemHeight = 16;
            this.comboBoxSpecialty.Location = new System.Drawing.Point(70, 119);
            this.comboBoxSpecialty.MaxLength = 100;
            this.comboBoxSpecialty.Name = "comboBoxSpecialty";
            this.comboBoxSpecialty.Size = new System.Drawing.Size(370, 24);
            this.comboBoxSpecialty.Sorted = true;
            this.comboBoxSpecialty.TabIndex = 315;
            this.comboBoxSpecialty.Enter += new System.EventHandler(this.comboBoxSpecialty_Enter);
            // 
            // dragger1
            // 
            this.dragger1.BackColor = System.Drawing.SystemColors.Control;
            this.dragger1.Dragging = false;
            this.dragger1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dragger1.Full = false;
            this.dragger1.Location = new System.Drawing.Point(467, 25);
            this.dragger1.Name = "dragger1";
            this.dragger1.Size = new System.Drawing.Size(54, 309);
            this.dragger1.TabIndex = 308;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 513);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.buttonCreateRelease);
            this.Controls.Add(this.buttonGetSignature);
            this.Controls.Add(this.groupBoxPractitioner);
            this.Controls.Add(this.groupBoxPatient);
            this.Controls.Add(this.groupBoxProcedure);
            this.Controls.Add(this.dragger1);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.buttonSaveProcedure);
            this.Controls.Add(this.buttonSaveConciergeProcedure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PDF Handler Plus";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxProcedure.ResumeLayout(false);
            this.groupBoxProcedure.PerformLayout();
            this.groupBoxPatient.ResumeLayout(false);
            this.groupBoxPatient.PerformLayout();
            this.groupBoxPractitioner.ResumeLayout(false);
            this.groupBoxPractitioner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveConciergeProcedure;
        private System.Windows.Forms.Button buttonSaveProcedure;
        private System.Windows.Forms.CheckBox checkBoxView;
        private Dragger dragger1;
        private System.Windows.Forms.Label labelProcedureDate;
        private System.Windows.Forms.Label labelProcedureLocation;
        private System.Windows.Forms.Label labelProcedurePatientFirstName;
        private System.Windows.Forms.Label labelProcedurePatientLastName;
        private System.Windows.Forms.TextBox textBoxProcedureDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerProcedure;
        private System.Windows.Forms.Label labelProcedurePages;
        private System.Windows.Forms.TextBox textBoxProcedurePages;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsFirstName;
        private System.Windows.Forms.TextBox textBoxProcedurePatientsLastName;
        private System.Windows.Forms.ComboBox comboBoxSuffix;
        private System.Windows.Forms.ComboBox comboBoxProcedureName;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.ComboBox comboBoxDoctor;
        private System.Windows.Forms.GroupBox groupBoxProcedure;
        private System.Windows.Forms.Label labelProcedureName;
        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.ComboBox comboBoxPatients;
        private System.Windows.Forms.GroupBox groupBoxPractitioner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPractitionerName;
        private System.Windows.Forms.Button buttonGetSignature;
        private System.Windows.Forms.Button buttonCreateRelease;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBoxSpecialty;
        private System.Windows.Forms.Label labelSpecialty;

    }
}

