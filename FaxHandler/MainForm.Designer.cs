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
            this.buttonSaveConciergeProcedure = new System.Windows.Forms.Button();
            this.buttonSaveProcedure = new System.Windows.Forms.Button();
            this.checkBoxView = new System.Windows.Forms.CheckBox();
            this.labelProcedureDate = new System.Windows.Forms.Label();
            this.labelProcedureLocation = new System.Windows.Forms.Label();
            this.textBoxProcedureDate = new System.Windows.Forms.TextBox();
            this.dateTimePickerProcedure = new System.Windows.Forms.DateTimePicker();
            this.labelProcedurePages = new System.Windows.Forms.Label();
            this.textBoxProcedurePages = new System.Windows.Forms.TextBox();
            this.comboBoxSuffix = new System.Windows.Forms.ComboBox();
            this.comboBoxProcedureName = new System.Windows.Forms.ComboBox();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.comboBoxPractitioner = new System.Windows.Forms.ComboBox();
            this.buttonEditLocation = new System.Windows.Forms.Button();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.buttonNewLocation = new System.Windows.Forms.Button();
            this.comboBoxSpecialty = new System.Windows.Forms.ComboBox();
            this.labelSpecialty = new System.Windows.Forms.Label();
            this.labelProcedureName = new System.Windows.Forms.Label();
            this.buttonEditPatient = new System.Windows.Forms.Button();
            this.labelPatientName = new System.Windows.Forms.Label();
            this.buttonCreateThumbDrive = new System.Windows.Forms.Button();
            this.buttonItems = new System.Windows.Forms.Button();
            this.buttonGetRelease = new System.Windows.Forms.Button();
            this.buttonAddPatient = new System.Windows.Forms.Button();
            this.comboBoxPatients = new System.Windows.Forms.ComboBox();
            this.buttonGetSignature = new System.Windows.Forms.Button();
            this.buttonEditPractitioner = new System.Windows.Forms.Button();
            this.buttonAddPractitioner = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPractitionerName = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.dragger1 = new FaxHandler.Dragger();
            this.SuspendLayout();
            // 
            // buttonSaveConciergeProcedure
            // 
            this.buttonSaveConciergeProcedure.Enabled = false;
            this.buttonSaveConciergeProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveConciergeProcedure.Location = new System.Drawing.Point(233, 240);
            this.buttonSaveConciergeProcedure.Name = "buttonSaveConciergeProcedure";
            this.buttonSaveConciergeProcedure.Size = new System.Drawing.Size(105, 23);
            this.buttonSaveConciergeProcedure.TabIndex = 303;
            this.buttonSaveConciergeProcedure.Text = "Save Concierge";
            this.buttonSaveConciergeProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveConciergeProcedure.Click += new System.EventHandler(this.buttonSaveConciergeProcedure_Click);
            // 
            // buttonSaveProcedure
            // 
            this.buttonSaveProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveProcedure.Location = new System.Drawing.Point(344, 240);
            this.buttonSaveProcedure.Name = "buttonSaveProcedure";
            this.buttonSaveProcedure.Size = new System.Drawing.Size(78, 23);
            this.buttonSaveProcedure.TabIndex = 304;
            this.buttonSaveProcedure.Text = "Save...";
            this.buttonSaveProcedure.UseVisualStyleBackColor = true;
            this.buttonSaveProcedure.Click += new System.EventHandler(this.buttonSaveProcedure_Click);
            // 
            // checkBoxView
            // 
            this.checkBoxView.AutoSize = true;
            this.checkBoxView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxView.Location = new System.Drawing.Point(428, 245);
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
            this.labelProcedureDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcedureDate.Location = new System.Drawing.Point(12, 38);
            this.labelProcedureDate.Name = "labelProcedureDate";
            this.labelProcedureDate.Size = new System.Drawing.Size(82, 13);
            this.labelProcedureDate.TabIndex = 0;
            this.labelProcedureDate.Text = "Procedure Date";
            // 
            // labelProcedureLocation
            // 
            this.labelProcedureLocation.AutoSize = true;
            this.labelProcedureLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcedureLocation.Location = new System.Drawing.Point(12, 214);
            this.labelProcedureLocation.Name = "labelProcedureLocation";
            this.labelProcedureLocation.Size = new System.Drawing.Size(48, 13);
            this.labelProcedureLocation.TabIndex = 3;
            this.labelProcedureLocation.Text = "Location";
            // 
            // textBoxProcedureDate
            // 
            this.textBoxProcedureDate.Enabled = false;
            this.textBoxProcedureDate.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedureDate.Location = new System.Drawing.Point(108, 34);
            this.textBoxProcedureDate.Name = "textBoxProcedureDate";
            this.textBoxProcedureDate.Size = new System.Drawing.Size(119, 22);
            this.textBoxProcedureDate.TabIndex = 5;
            // 
            // dateTimePickerProcedure
            // 
            this.dateTimePickerProcedure.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerProcedure.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerProcedure.Location = new System.Drawing.Point(233, 34);
            this.dateTimePickerProcedure.Name = "dateTimePickerProcedure";
            this.dateTimePickerProcedure.Size = new System.Drawing.Size(118, 22);
            this.dateTimePickerProcedure.TabIndex = 100;
            this.dateTimePickerProcedure.ValueChanged += new System.EventHandler(this.dateTimePickerProcedure_ValueChanged);
            // 
            // labelProcedurePages
            // 
            this.labelProcedurePages.AutoSize = true;
            this.labelProcedurePages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcedurePages.Location = new System.Drawing.Point(12, 244);
            this.labelProcedurePages.Name = "labelProcedurePages";
            this.labelProcedurePages.Size = new System.Drawing.Size(43, 13);
            this.labelProcedurePages.TabIndex = 2;
            this.labelProcedurePages.Text = "Page(s)";
            // 
            // textBoxProcedurePages
            // 
            this.textBoxProcedurePages.Enabled = false;
            this.textBoxProcedurePages.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcedurePages.Location = new System.Drawing.Point(108, 240);
            this.textBoxProcedurePages.Name = "textBoxProcedurePages";
            this.textBoxProcedurePages.Size = new System.Drawing.Size(119, 22);
            this.textBoxProcedurePages.TabIndex = 106;
            // 
            // comboBoxSuffix
            // 
            this.comboBoxSuffix.DropDownHeight = 400;
            this.comboBoxSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSuffix.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxSuffix.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSuffix.FormattingEnabled = true;
            this.comboBoxSuffix.IntegralHeight = false;
            this.comboBoxSuffix.ItemHeight = 16;
            this.comboBoxSuffix.Location = new System.Drawing.Point(108, 152);
            this.comboBoxSuffix.MaxLength = 12;
            this.comboBoxSuffix.Name = "comboBoxSuffix";
            this.comboBoxSuffix.Size = new System.Drawing.Size(180, 24);
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
            this.comboBoxProcedureName.Location = new System.Drawing.Point(106, 62);
            this.comboBoxProcedureName.MaxLength = 50;
            this.comboBoxProcedureName.Name = "comboBoxProcedureName";
            this.comboBoxProcedureName.Size = new System.Drawing.Size(341, 24);
            this.comboBoxProcedureName.Sorted = true;
            this.comboBoxProcedureName.TabIndex = 108;
            this.comboBoxProcedureName.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcedureName_SelectedIndexChanged);
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
            this.comboBoxLocation.Location = new System.Drawing.Point(106, 210);
            this.comboBoxLocation.MaxLength = 50;
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(341, 24);
            this.comboBoxLocation.Sorted = true;
            this.comboBoxLocation.TabIndex = 109;
            this.comboBoxLocation.Enter += new System.EventHandler(this.comboBoxLocation_Enter);
            // 
            // comboBoxPractitioner
            // 
            this.comboBoxPractitioner.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxPractitioner.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPractitioner.DropDownHeight = 400;
            this.comboBoxPractitioner.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPractitioner.FormattingEnabled = true;
            this.comboBoxPractitioner.IntegralHeight = false;
            this.comboBoxPractitioner.ItemHeight = 16;
            this.comboBoxPractitioner.Location = new System.Drawing.Point(108, 122);
            this.comboBoxPractitioner.MaxDropDownItems = 7;
            this.comboBoxPractitioner.MaxLength = 50;
            this.comboBoxPractitioner.Name = "comboBoxPractitioner";
            this.comboBoxPractitioner.Size = new System.Drawing.Size(339, 24);
            this.comboBoxPractitioner.Sorted = true;
            this.comboBoxPractitioner.TabIndex = 110;
            this.comboBoxPractitioner.SelectedIndexChanged += new System.EventHandler(this.comboBoxDoctor_SelectedIndexChanged);
            this.comboBoxPractitioner.TextChanged += new System.EventHandler(this.comboBoxDoctor_TextChanged);
            this.comboBoxPractitioner.Enter += new System.EventHandler(this.comboBoxDoctor_Enter);
            // 
            // buttonEditLocation
            // 
            this.buttonEditLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditLocation.Location = new System.Drawing.Point(453, 209);
            this.buttonEditLocation.Name = "buttonEditLocation";
            this.buttonEditLocation.Size = new System.Drawing.Size(52, 23);
            this.buttonEditLocation.TabIndex = 319;
            this.buttonEditLocation.Text = "Edit...";
            this.buttonEditLocation.UseVisualStyleBackColor = true;
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxTarget.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxTarget.DropDownHeight = 400;
            this.comboBoxTarget.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.IntegralHeight = false;
            this.comboBoxTarget.ItemHeight = 16;
            this.comboBoxTarget.Location = new System.Drawing.Point(108, 92);
            this.comboBoxTarget.MaxLength = 50;
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(339, 24);
            this.comboBoxTarget.Sorted = true;
            this.comboBoxTarget.TabIndex = 318;
            this.comboBoxTarget.Enter += new System.EventHandler(this.comboBoxTarget_Enter);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTarget.Location = new System.Drawing.Point(12, 96);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(90, 13);
            this.labelTarget.TabIndex = 317;
            this.labelTarget.Text = "Procedure Target";
            // 
            // buttonNewLocation
            // 
            this.buttonNewLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewLocation.Location = new System.Drawing.Point(511, 209);
            this.buttonNewLocation.Name = "buttonNewLocation";
            this.buttonNewLocation.Size = new System.Drawing.Size(52, 23);
            this.buttonNewLocation.TabIndex = 316;
            this.buttonNewLocation.Text = "Add...";
            this.buttonNewLocation.UseVisualStyleBackColor = true;
            this.buttonNewLocation.Click += new System.EventHandler(this.buttonNewLocation_Click);
            // 
            // comboBoxSpecialty
            // 
            this.comboBoxSpecialty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSpecialty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxSpecialty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSpecialty.DropDownHeight = 400;
            this.comboBoxSpecialty.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSpecialty.FormattingEnabled = true;
            this.comboBoxSpecialty.IntegralHeight = false;
            this.comboBoxSpecialty.ItemHeight = 16;
            this.comboBoxSpecialty.Location = new System.Drawing.Point(106, 182);
            this.comboBoxSpecialty.MaxLength = 100;
            this.comboBoxSpecialty.Name = "comboBoxSpecialty";
            this.comboBoxSpecialty.Size = new System.Drawing.Size(341, 22);
            this.comboBoxSpecialty.TabIndex = 315;
            this.comboBoxSpecialty.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpecialty_DrawItem);
            this.comboBoxSpecialty.Enter += new System.EventHandler(this.comboBoxSpecialty_Enter);
            // 
            // labelSpecialty
            // 
            this.labelSpecialty.AutoSize = true;
            this.labelSpecialty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpecialty.Location = new System.Drawing.Point(12, 186);
            this.labelSpecialty.Name = "labelSpecialty";
            this.labelSpecialty.Size = new System.Drawing.Size(81, 13);
            this.labelSpecialty.TabIndex = 314;
            this.labelSpecialty.Text = "Specialty Name";
            // 
            // labelProcedureName
            // 
            this.labelProcedureName.AutoSize = true;
            this.labelProcedureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcedureName.Location = new System.Drawing.Point(12, 66);
            this.labelProcedureName.Name = "labelProcedureName";
            this.labelProcedureName.Size = new System.Drawing.Size(87, 13);
            this.labelProcedureName.TabIndex = 313;
            this.labelProcedureName.Text = "Procedure Name";
            // 
            // buttonEditPatient
            // 
            this.buttonEditPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditPatient.Location = new System.Drawing.Point(453, 5);
            this.buttonEditPatient.Name = "buttonEditPatient";
            this.buttonEditPatient.Size = new System.Drawing.Size(52, 23);
            this.buttonEditPatient.TabIndex = 321;
            this.buttonEditPatient.Text = "Edit...";
            this.buttonEditPatient.UseVisualStyleBackColor = true;
            // 
            // labelPatientName
            // 
            this.labelPatientName.AutoSize = true;
            this.labelPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatientName.Location = new System.Drawing.Point(12, 10);
            this.labelPatientName.Name = "labelPatientName";
            this.labelPatientName.Size = new System.Drawing.Size(71, 13);
            this.labelPatientName.TabIndex = 318;
            this.labelPatientName.Text = "Patient Name";
            // 
            // buttonCreateThumbDrive
            // 
            this.buttonCreateThumbDrive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateThumbDrive.Location = new System.Drawing.Point(462, 268);
            this.buttonCreateThumbDrive.Name = "buttonCreateThumbDrive";
            this.buttonCreateThumbDrive.Size = new System.Drawing.Size(97, 23);
            this.buttonCreateThumbDrive.TabIndex = 318;
            this.buttonCreateThumbDrive.Text = "Thumb Drive...";
            this.buttonCreateThumbDrive.UseVisualStyleBackColor = true;
            // 
            // buttonItems
            // 
            this.buttonItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonItems.Location = new System.Drawing.Point(233, 268);
            this.buttonItems.Name = "buttonItems";
            this.buttonItems.Size = new System.Drawing.Size(105, 23);
            this.buttonItems.TabIndex = 317;
            this.buttonItems.Text = "Patient Issues...";
            this.buttonItems.UseVisualStyleBackColor = true;
            this.buttonItems.Click += new System.EventHandler(this.buttonItems_Click);
            // 
            // buttonGetRelease
            // 
            this.buttonGetRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetRelease.Location = new System.Drawing.Point(106, 268);
            this.buttonGetRelease.Name = "buttonGetRelease";
            this.buttonGetRelease.Size = new System.Drawing.Size(121, 23);
            this.buttonGetRelease.TabIndex = 316;
            this.buttonGetRelease.Text = "Medical Release...";
            this.buttonGetRelease.UseVisualStyleBackColor = true;
            this.buttonGetRelease.Click += new System.EventHandler(this.buttonGetRelease_Click);
            // 
            // buttonAddPatient
            // 
            this.buttonAddPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPatient.Location = new System.Drawing.Point(511, 5);
            this.buttonAddPatient.Name = "buttonAddPatient";
            this.buttonAddPatient.Size = new System.Drawing.Size(52, 23);
            this.buttonAddPatient.TabIndex = 315;
            this.buttonAddPatient.Text = "Add...";
            this.buttonAddPatient.UseVisualStyleBackColor = true;
            this.buttonAddPatient.Click += new System.EventHandler(this.buttonAddPatient_Click);
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
            this.comboBoxPatients.Location = new System.Drawing.Point(108, 5);
            this.comboBoxPatients.MaxLength = 100;
            this.comboBoxPatients.Name = "comboBoxPatients";
            this.comboBoxPatients.Size = new System.Drawing.Size(339, 23);
            this.comboBoxPatients.Sorted = true;
            this.comboBoxPatients.TabIndex = 314;
            this.comboBoxPatients.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxPatients_DrawItem);
            this.comboBoxPatients.SelectedIndexChanged += new System.EventHandler(this.comboBoxPatients_SelectedIndexChanged);
            this.comboBoxPatients.TextChanged += new System.EventHandler(this.comboBoxPatients_TextChanged);
            this.comboBoxPatients.Enter += new System.EventHandler(this.comboBoxPatients_Enter);
            this.comboBoxPatients.MouseHover += new System.EventHandler(this.comboBoxPatients_MouseHover);
            // 
            // buttonGetSignature
            // 
            this.buttonGetSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetSignature.Location = new System.Drawing.Point(344, 268);
            this.buttonGetSignature.Name = "buttonGetSignature";
            this.buttonGetSignature.Size = new System.Drawing.Size(112, 23);
            this.buttonGetSignature.TabIndex = 314;
            this.buttonGetSignature.Text = "Patient Signature...";
            this.buttonGetSignature.UseVisualStyleBackColor = true;
            this.buttonGetSignature.Click += new System.EventHandler(this.buttonGetSignature_Click);
            // 
            // buttonEditPractitioner
            // 
            this.buttonEditPractitioner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditPractitioner.Location = new System.Drawing.Point(453, 121);
            this.buttonEditPractitioner.Name = "buttonEditPractitioner";
            this.buttonEditPractitioner.Size = new System.Drawing.Size(52, 23);
            this.buttonEditPractitioner.TabIndex = 320;
            this.buttonEditPractitioner.Text = "Edit...";
            this.buttonEditPractitioner.UseVisualStyleBackColor = true;
            this.buttonEditPractitioner.Click += new System.EventHandler(this.buttonEditPractitioner_Click);
            // 
            // buttonAddPractitioner
            // 
            this.buttonAddPractitioner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPractitioner.Location = new System.Drawing.Point(511, 121);
            this.buttonAddPractitioner.Name = "buttonAddPractitioner";
            this.buttonAddPractitioner.Size = new System.Drawing.Size(52, 23);
            this.buttonAddPractitioner.TabIndex = 319;
            this.buttonAddPractitioner.Text = "Add...";
            this.buttonAddPractitioner.UseVisualStyleBackColor = true;
            this.buttonAddPractitioner.Click += new System.EventHandler(this.buttonAddNewDoctor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 111;
            this.label1.Text = "Append";
            // 
            // labelPractitionerName
            // 
            this.labelPractitionerName.AutoSize = true;
            this.labelPractitionerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPractitionerName.Location = new System.Drawing.Point(12, 126);
            this.labelPractitionerName.Name = "labelPractitionerName";
            this.labelPractitionerName.Size = new System.Drawing.Size(91, 13);
            this.labelPractitionerName.TabIndex = 0;
            this.labelPractitionerName.Text = "Practitioner Name";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(12, 297);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(504, 56);
            this.textBoxStatus.TabIndex = 316;
            this.textBoxStatus.TabStop = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(522, 297);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(41, 56);
            this.buttonClear.TabIndex = 317;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // dragger1
            // 
            this.dragger1.BackColor = System.Drawing.SystemColors.Control;
            this.dragger1.Dragging = false;
            this.dragger1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dragger1.Full = false;
            this.dragger1.Location = new System.Drawing.Point(453, 34);
            this.dragger1.Name = "dragger1";
            this.dragger1.Size = new System.Drawing.Size(106, 81);
            this.dragger1.TabIndex = 308;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 364);
            this.Controls.Add(this.textBoxProcedurePages);
            this.Controls.Add(this.buttonNewLocation);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.buttonEditLocation);
            this.Controls.Add(this.buttonSaveProcedure);
            this.Controls.Add(this.buttonItems);
            this.Controls.Add(this.labelProcedurePages);
            this.Controls.Add(this.buttonCreateThumbDrive);
            this.Controls.Add(this.buttonSaveConciergeProcedure);
            this.Controls.Add(this.comboBoxSpecialty);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.labelProcedureLocation);
            this.Controls.Add(this.buttonGetRelease);
            this.Controls.Add(this.labelSpecialty);
            this.Controls.Add(this.buttonEditPatient);
            this.Controls.Add(this.buttonAddPractitioner);
            this.Controls.Add(this.buttonEditPractitioner);
            this.Controls.Add(this.buttonGetSignature);
            this.Controls.Add(this.buttonAddPatient);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.comboBoxTarget);
            this.Controls.Add(this.comboBoxSuffix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPatientName);
            this.Controls.Add(this.comboBoxPractitioner);
            this.Controls.Add(this.labelPractitionerName);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.labelProcedureName);
            this.Controls.Add(this.comboBoxPatients);
            this.Controls.Add(this.dateTimePickerProcedure);
            this.Controls.Add(this.dragger1);
            this.Controls.Add(this.comboBoxProcedureName);
            this.Controls.Add(this.textBoxProcedureDate);
            this.Controls.Add(this.labelProcedureDate);
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
        private System.Windows.Forms.TextBox textBoxProcedureDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerProcedure;
        private System.Windows.Forms.Label labelProcedurePages;
        private System.Windows.Forms.TextBox textBoxProcedurePages;
        private System.Windows.Forms.ComboBox comboBoxSuffix;
        private System.Windows.Forms.ComboBox comboBoxProcedureName;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.ComboBox comboBoxPractitioner;
        private System.Windows.Forms.Label labelProcedureName;
        private System.Windows.Forms.ComboBox comboBoxPatients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPractitionerName;
        private System.Windows.Forms.Button buttonGetSignature;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBoxSpecialty;
        private System.Windows.Forms.Label labelSpecialty;
        private System.Windows.Forms.Button buttonAddPatient;
        private System.Windows.Forms.Button buttonAddPractitioner;
        private System.Windows.Forms.Button buttonNewLocation;
        private System.Windows.Forms.Button buttonGetRelease;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Button buttonItems;
        private System.Windows.Forms.Button buttonCreateThumbDrive;
        private System.Windows.Forms.Label labelPatientName;
        private System.Windows.Forms.Button buttonEditLocation;
        private System.Windows.Forms.Button buttonEditPatient;
        private System.Windows.Forms.Button buttonEditPractitioner;

    }
}

