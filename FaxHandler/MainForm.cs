using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
using iwantedue.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;
using Acrobat;
using System.Security.Principal;
using System.Collections;

namespace FaxHandler
{
    public partial class MainForm : Form
    {
        #region Declarations
        PDF.Document document;
        Timer timer;
        double minimumOpacity = 0.0;
        bool suspendValidation = false;
        FileInfo draggableFile = null;
        ToolTip toolTipPatient;
        #endregion
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            string lastSavedDirectory = Properties.Settings.Default.LastSavedDirectory;
            if (lastSavedDirectory == null || lastSavedDirectory.Equals(string.Empty))
            {
                Properties.Settings.Default.LastSavedDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);  // My Documents
                Properties.Settings.Default.Save();
            }
            string conciergeLocation = Properties.Settings.Default.ConciergeLocation;
            if (conciergeLocation == null || conciergeLocation == string.Empty || !Directory.Exists(conciergeLocation))
            {
                ShowWarning("The location of the Concierge directory is either not set, is invalid, or isunreadable.\n" +
                    "You are about to be given the chance to choose the location.");
                for(;;)
                {
                    var dialog = new FolderBrowserDialog();
                    var result = Show(dialog);
                    if (result == DialogResult.OK)
                    {
                        if (!Directory.Exists(dialog.SelectedPath))
                        {
                            ShowError("That is not a valid directory, please try again.");
                            continue;
                        }
                        conciergeLocation = dialog.SelectedPath;
                        break;
                    }
                    else // cancel
                    {
                        ShowWarning("This program cannot continue until you set the location of the Concierge directory.\n" +
                            "The program will now quit.");
                        System.Threading.Thread.CurrentThread.Abort();
                        return;
                    }
                }
                Properties.Settings.Default.ConciergeLocation = conciergeLocation;
                Properties.Settings.Default.Save();
            }
            checkBoxView.Checked = Properties.Settings.Default.View;
            AllowDrop = true;
            DragEnter += new DragEventHandler(MainForm_DragEnter);
            DragDrop += new DragEventHandler(MainForm_DragDrop);
            toolTipPatient = new ToolTip();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 500;
        }
        #endregion
        #region Event handlers
        private void checkBoxView_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.View = checkBoxView.Checked;
            Properties.Settings.Default.Save();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProcedureAutocomplete();
            timer.Start();
        }
        private void buttonSaveConciergeProcedure_Click(object sender, EventArgs e)
        {
            Save(concierge: true);
        }
        private void buttonSaveProcedure_Click(object sender, EventArgs e)
        {
            Save(concierge: false);
        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            Opacity = Math.Max(minimumOpacity, .4);
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            Opacity = 1.0;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDocument();
            try
            {
                timer.Stop();
            }
            catch (Exception )
            {
                // don't care
            }
        }
        private void textBoxPages_Validating(object sender, CancelEventArgs e)
        {
            textBoxPages_Validate(sender, e);
            UpdateSaveButtons();
        }
        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (suspendValidation)
                return;
            string val = textBox.Text.Trim();
            Regex r = new Regex(@"\-{2}");
            if (r.IsMatch(val))
            {
                ShowError("No embedded double hyphens are allowed");
                e.Cancel = true;
            }
            else if (Regex.IsMatch(val,@"<+|>+|:+|/+|\\+|\|+|\?+|\*+|""+"))
            {
                ShowError(@"None of these characters are allowed: <>:\"" /\|?*");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            //textBox.Text = val.ToUpper();
            UpdateSaveButtons();
        }
        private void buttonSetup_Click(object sender, EventArgs e)
        {
            DialogResult result = ShowSetup();
            if (result == DialogResult.OK)
                LoadProcedureAutocomplete();
        }
        private void dateTimePickerProcedure_ValueChanged(object sender, EventArgs e)
        {
            textBoxProcedureDate.Text = DateToString(dateTimePickerProcedure.Value);
            UpdateSaveButtons();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if(Opacity < 1)
                EnsureTopmost();
            CheckDocument();
            UpdateSaveButtons();
        }
        #endregion
        #region Properties
        public DateTime InvalidDate
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
            }
        }
        public int doctor_id
        {
            get
            {
                int si = comboBoxPractitioner.SelectedIndex;
                if (si < 0)
                    return -1;
                ItemHolder d = comboBoxPractitioner.Items[si] as ItemHolder;
                return (int)d.values["id"];
            }
        }
        public DateTime patient_doc
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return InvalidDate;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                return (DateTime)p.values["dob"];
            }
        }
        public int patient_id
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return -1;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                return (int)p.values["id"];
            }
        }
        public string patient_emergency_contact
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return null;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                string last = p.values["last"] as string;
                return last;
            }
        }
        public string patient_gender
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return null;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                int patient_id = (int)p.values["id"];
                string gender = p.values["gender"] as string;
                return gender;
            }
        }
        public string patient_first
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return null;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                int patient_id = (int)p.values["id"];
                string first = p.values["first"] as string;
                return first;
            }
        }
        public string patient_last
        {
            get
            {
                if (comboBoxPatients.SelectedIndex < 0)
                    return null;
                ItemHolder p = comboBoxPatients.Items[comboBoxPatients.SelectedIndex] as ItemHolder;
                string last = p.values["last"] as string;
                return last;
            }
        }
        #endregion
        #region Control Accessors
        /*
        TextBox GetTextBoxPatientsLastName_()
        {
            return textBoxProcedurePatientsLastName;
        }
         */
        #endregion
        #region Native Methods
        private class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        }
        #endregion

        #region Utilities
        string GenerateFilename(string suffix)
        {
            string[] tempParts = WindowsIdentity.GetCurrent().Name.Split('\\');
            string userName = tempParts[1];
            string date = textBoxProcedureDate.Text;
            string fileName = date
                + "--" + comboBoxProcedureName.Text
                    + "--" + comboBoxLocation.Text
                    + "--" + comboBoxPractitioner.Text
                    + "--" + PatientsDirectoryName().TrimAll() 
                    + "--" + userName
                    + suffix + ".PDF";
            return fileName;
        }
        
        string PatientsDirectoryName()
        {
            string result = string.Format("{0}, {1}", patient_last, patient_first).ToUpper(); ;
            return result;
        }
         
        void EnsureTopmost()
        {
            const UInt32 WS_EX_TOPMOST = 0x0008;
            const int GWL_EXSTYLE = (-20);
            if ((NativeMethods.GetWindowLong(this.Handle, GWL_EXSTYLE) & WS_EX_TOPMOST) == 0)
            {
                TopMost = false;
                TopMost = true;
            }
        }
        void UpdateDirectoryLabels(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                DirectoryInfo di = fileInfo.Directory;
                string patientDirName = PatientsDirectoryName();
                DirectoryInfo diConcierge = new DirectoryInfo(Properties.Settings.Default.ConciergeLocation);
                while (di != null)
                {
                    DirectoryInfo diParent = di.Parent;
                    if (diParent != null && diParent.Name.ToUpper().Trim() == patientDirName)
                    {
                        DirectoryInfo diGrandparent = diParent.Parent;
                        if (diGrandparent != null && diGrandparent.FullName == diConcierge.FullName)
                        {
                            di = fileInfo.Directory;
                            while (di.FullName.ToUpper().Trim() != diParent.FullName.ToUpper().Trim())
                            {
                                DirectoryInfo diParent2 = di.Parent;
                                Regex r = new Regex("^.*#$");
                                string name = di.Name.Trim();
                                if (!r.IsMatch(name))
                                {
                                    Directory.Move(di.FullName, di.FullName + " #");
                                }
                                di = diParent2;
                            }
                            return;
                        }
                        di = di.Parent;
                    }
                    if (di == null)
                        return;
                    di = di.Parent;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        void CloseDocument()
        {
            if (document != null)
            {
                document.Dispose();
                document = null;
            }
        }
        string DateToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MMM-dd");
            //return string.Format("{0:D2}-{1:D2}-{2}", dateTime.Month, dateTime.Day, dateTime.Year);
        }
        string[] GetPredefinedProcedures()
        {
            char[] delimeters = { '\n' };
            string procedures = Properties.Settings.Default.Procedures;
            string[] definedProcedureNames = procedures.Split(delimeters);
            return definedProcedureNames;
        }
        void SetPredfinedProcedures(string[] procedures)
        {
            string definedProcedureNames = String.Concat(from procedure in procedures
                                                         orderby procedure ascending
                                                         select procedure.Trim().ToUpper() + "\n").Trim();
            Properties.Settings.Default.Procedures = definedProcedureNames;
            Properties.Settings.Default.Save();
            var collection = new AutoCompleteStringCollection();
            collection.AddRange(procedures);
            comboBoxProcedureName.AutoCompleteCustomSource = collection;
        }
        void AddPredefinedProcedure(string newProcedure)
        {
            string[] procedures = GetPredefinedProcedures();
            if ((from procedure in procedures
                 where procedure.ToUpper().Trim().Equals(newProcedure.ToUpper().Trim())
                 select procedure).Count() == 0)
            {
                string[] newProcedures = new string[procedures.Length+1];
                procedures.CopyTo(newProcedures, 0);
                procedures = newProcedures;
                procedures[procedures.Length-1] = newProcedure.Trim().ToUpper();
                SetPredfinedProcedures(procedures);
            }
        }
        void LoadProcedureAutocomplete()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string[] definedProcedureNames = GetPredefinedProcedures();
            collection.AddRange(definedProcedureNames);
            comboBoxProcedureName.AutoCompleteCustomSource = collection;
        }
        bool IsPdfFileName(string fileName)
        {
            Regex regex = new Regex(@".*\.PDF$");
            return regex.IsMatch(fileName.ToUpper());
        }
        string GetPortablePath(string path)
        {
            if (path[0] != '\\')
                path = @"\" + path;
            path = path.Replace(":", String.Empty).Replace(@"\", "/");
            return path;
        }
        #endregion
        #region Drag and Drop
        void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                try
                {
                    CloseDocument();
                    Cursor = Cursors.WaitCursor;
                    document = new PDF.Document(files[0]);
                }
                catch (Exception exception)
                {
                    ShowError(exception.Message);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                string newFileName;
                //wrap standard IDataObject in OutlookDataObject
                OutlookDataObject dataObject = new OutlookDataObject(e.Data);

                //get the names and data streams of the files dropped
                string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");

                if (filenames.Length != 1 || !IsPdfFileName(filenames[0]))
                    return;
                MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");
                newFileName = Path.GetTempFileName() + filenames[0];

                try
                {
                    FileInfo fileInfo = new FileInfo(newFileName);
                    FileStream tempFileStream = File.Create(newFileName);
                    fileInfo.Attributes = FileAttributes.Temporary;
                    filestreams[0].WriteTo(tempFileStream);
                    tempFileStream.Close();
                    Cursor = Cursors.WaitCursor;
                    CloseDocument();
                    document = new PDF.Document(newFileName);
                }
                catch (System.Exception ex)
                {
                    ShowError(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            document.Show();
            document.Zoom(PDF.ZoomLevel.FitPage);
        }
        void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if(files != null && files.Length == 1 && IsPdfFileName(files[0]))
                    e.Effect = DragDropEffects.Copy;
            }
            else
            {
                //wrap standard IDataObject in OutlookDataObject
                OutlookDataObject dataObject = new OutlookDataObject(e.Data);

                //get the names and data streams of the files dropped
                string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
                if (filenames.Length == 1 && IsPdfFileName(filenames[0]))
                    e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion
        #region Controls manipulation
        void CheckDocument()
        {
            if (document != null)
                if (!document.Valid)
                {
                    document.Dispose();
                    document = null;
                }
            if (document != null)
            {
                textBoxProcedurePages.Enabled = true;
            }
            else
            {
                TextBox textBox = textBoxProcedurePages;
                textBox.Enabled = false;
                textBox.Clear();
            }
        }
        bool ValidPageRange(int numberOfPages)
        {
            PageRanges pageRanges;
            string s = textBoxProcedurePages.Text.TrimAll();
            if (s == string.Empty)
                return false;
            try
            {
                pageRanges = new PageRanges(s);
                if (pageRanges.LastPage > numberOfPages)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public FileInfo GetDraggableFileInfo()
        {
            FileInfo result = null;
            try
            {
                if(draggableFile != null)
                {
                    Directory.Delete(draggableFile.DirectoryName,true);
                    draggableFile = null;
                }
                PageRanges pageRanges = new PageRanges(textBoxProcedurePages.Text.TrimAll());
                string tempDirName = Path.GetTempFileName() + ".dir";
                Directory.CreateDirectory(tempDirName);
                if(Directory.Exists(tempDirName))
                {
                    PDF.Document trimmedDocument = document.TrimPages(pageRanges);
                    string fileName = GenerateFilename("--SVD--" + TimeStamp());
                    string fullFileName = tempDirName + "\\" + fileName;
                    trimmedDocument.Save(fullFileName);
                    result = new FileInfo(fullFileName);
                    trimmedDocument.Dispose();
                }
            }
            catch (Exception )
            {
                result = null;
            }
            return result;
        }
        void UpdateSaveButtons()
        {
            UpdateSelectedPatient();
            UpdateAddDoctorButton();
            UpdateAddLocationButton();
    //        UpdateAddPatientButton();
            //bool patientNameFilled = textBoxProcedurePatientsFirstName.Text.Trim().Length > 0 && textBoxProcedurePatientsLastName.Text.Trim().Length > 0;
            bool patientSelected = patient_id >= 0;
            bool doctorNameFilled = comboBoxPractitioner.Text.Trim().Length > 0;

            if (textBoxProcedurePages.Text.Trim().Length > 0
                && textBoxProcedureDate.Text.Trim().Length > 0
                && doctorNameFilled
                && comboBoxLocation.Text.Trim().Length > 0
                // && patientNameFilled
                && ValidPageRange(document.Pages))
            {
                dragger1.Full = true;
            }
            else
            {
                dragger1.Full = false;
            }
            if (textBoxProcedurePages.Text.Trim().Length > 0
                && textBoxProcedureDate.Text.Trim().Length > 0
                && doctorNameFilled
                && comboBoxLocation.Text.Trim().Length > 0
                && patientSelected
                && ValidPageRange(document.Pages))
            {
                buttonSaveConciergeProcedure.Enabled = true;
            }
            else
            {
                buttonSaveConciergeProcedure.Enabled = false;
            }
            
            
            // buttonSaveReleaseRequest.Enabled = patientNameFilled && doctorNameFilled;
            buttonGetSignature.Enabled = patient_id >= 0;

        }

        #endregion
        string TimeStamp()
        {
            return string.Format("{0:yyyyMMddHHmm}", DateTime.Now);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                var result = AskYesNo(message:"Are you sure", caption:"Quitting");
                if (result == DialogResult.Yes)
                    suspendValidation = true;
                else
                    return;
            }
            base.WndProc(ref m);
        }
        bool ValidatePageRange(string s)
        {
            s = s.TrimAll();
            if (s == string.Empty)
                return false;
            if (!document.Valid)
                return false;
            PageRanges pageRanges = null;
            try
            {
                pageRanges = new PageRanges(s);
            }
            catch (Exception)
            {
                return false;
            }
            if (pageRanges.LastPage > document.Pages)
                return false;
            return true;
        }
        private void textBoxPages_Validate(object sender, CancelEventArgs e)
        {
            CheckDocument();
            if(!Enabled || suspendValidation)
            {
                e.Cancel = false;
                return;
            }

            TextBox textBox = sender as TextBox;
            string text = textBox.Text.Trim();
            if (!ValidatePageRange(text))
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            return;
            /*
            if (text.Trim().Length == 0)
            {
                e.Cancel = false;
                return;
            }
            if (!document.Valid)
            {
                ShowError("The document has been closed");
                e.Cancel = true;
            }
            int pageCount = document.Pages;
            PageRanges pageRanges;
            try
            {
                pageRanges = new PageRanges(text);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                e.Cancel = true;
                return;
            }
            if (pageRanges.LastPage > pageCount)
            {
                ShowError(String.Format("There are only {0} page(s) in the doucment",pageCount));
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            */
        }
        private void Save(bool concierge)
        {
            string startingDirectory = null;
            DirectoryInfo directoryInfoMain;
            string destinationPath = string.Empty;
            string destinationDirectory = string.Empty;
            DialogResult result;
            if (!concierge)
            {
                try
                {
                    directoryInfoMain = new DirectoryInfo(Properties.Settings.Default.LastSavedDirectory);
                }
                catch (SecurityException /*securityException*/)
                {
                    ShowError("You do not have permission to access " + Properties.Settings.Default.LastSavedDirectory);
                    return;
                }
                catch (ArgumentException /*argumentException*/)
                {
                    ShowError("There is an invalid character in " + Properties.Settings.Default.LastSavedDirectory);
                    return;
                }
                catch (PathTooLongException /*pathTooLongException*/)
                {
                    ShowError(Properties.Settings.Default.LastSavedDirectory + " is too far deep");
                    return;
                }
                if (directoryInfoMain.Exists)
                {
                    startingDirectory = directoryInfoMain.FullName;
                }
                else
                {
                    ShowError("Cannot find " + Properties.Settings.Default.LastSavedDirectory);
                    return;
                }
                string location = string.Empty;
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.SelectedPath = startingDirectory;
                result = Show(dialog);
                destinationDirectory = dialog.SelectedPath;
                if (result != DialogResult.OK)
                    return;
            }
            PageRanges pageRanges;
            if (textBoxProcedurePages.Text.Trim() == string.Empty)
                pageRanges = PageRanges.All;
            else
            {
                try
                {
                    pageRanges = new PageRanges(textBoxProcedurePages.Text);
                }
                catch (Exception)
                {
                    ShowError("Wasn't able to parse the page range");
                    return;
                }
            }
            try
            {

                PDF.Document trimmedDocument = document.TrimPages(pageRanges);
                if (trimmedDocument == null)
                {
                    ShowError("Couldn't trim and save the new PDF.");
                    return;
                }
                if (!concierge)
                {

                    string fileName;
                    fileName = GenerateFilename("--SV--" + TimeStamp());
                    destinationPath = destinationDirectory + (destinationDirectory.Last() != '\\' ? @"\" : String.Empty) + fileName;

                    if (File.Exists(destinationPath))
                    {
                        string prompt = String.Format(
                            "The file \"{0}\" already exists in folder \"{1}\". " +
                            "Do you want to replace it?  If you do, the original file will be lost forever.",
                            fileName, destinationPath);
                        result = AskYesNo(message: prompt, caption: "Warning");
                        if (result == DialogResult.No)
                            return;
                    }
                    trimmedDocument.Save(destinationPath);
                    trimmedDocument.Dispose();
                }
                else
                {
                    Db.Db db = Db.Db.Instance();
                    List<Hashtable> items = new List<Hashtable>();
                    Hashtable item = new Hashtable();
                    string[] parts = comboBoxSpecialty.Text.Split(new char[] { '/' });
                    item["specialty"] = parts[0];
                    item["subspecialty"] = (parts.Length == 1 || parts[1] == null) ? string.Empty : parts[1];

                    ItemHolder ih2 = comboBoxSpecialty.Items[comboBoxSpecialty.SelectedIndex] as ItemHolder;
                    int procedure_specialty_id = (int)ih2.values["procedure_specialty_id"];
                    item["firstName"] = patient_first;
                    item["lastName"] = patient_last;
                    item["path"] = trimmedDocument.FileName;
                    item["procedureDate"] = dateTimePickerProcedure.Value;
                    item["procedure_name"] = comboBoxProcedureName.Text;
                    item["location_name"] = comboBoxLocation.Text;
                    item["doctor_name"] = comboBoxPractitioner.Text;
                    item["patient_id"] = patient_id;
                    item["procedure_specialty_id"] = procedure_specialty_id;
                    item["target_name"] = comboBoxTarget.Items[comboBoxTarget.SelectedIndex] as string;
                    item["doctor_id"] = doctor_id;
                    items.Add(item);
                    Hashtable dbResult = db.AddActivities(items);
                    if (checkBoxView.Checked)
                    {
                        if (dbResult["status"] as string == "ok")
                        {
                            List<Hashtable> data = dbResult["data"] as List<Hashtable>;
                            int file_id = (int)data[0]["file_id"];
                            Process.Start("http://localhost:50505/downloadfile/" + file_id + ".pdf");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ShowError("Acrobat reported an error when extracting the pages.\n" + exception.Message);
                return;
            }
            if (checkBoxView.Checked)
            {
                if (!concierge)
                {
                    int tries = 5;
                    int sleepTime = 1 * 1000; // one second
                    for (int i = 0; i < tries; i++)
                    {
                        System.Threading.Thread.Sleep(sleepTime); // gives Acrobat a chance to finish
                        FileInfo fileInfo = new FileInfo(destinationPath);
                        if (!fileInfo.Exists)
                            continue;
                        try
                        {
                            FileStream fs = File.Open(destinationPath, FileMode.Open, FileAccess.ReadWrite);
                            fs.Close();
                            break;
                        }
                        catch (IOException e)
                        {
                            const int ERROR_SHARING_VIOLATION = 0x20;
                            const int ERROR_LOCK_VIOLATION = 0x21;
                            int errorCode = Marshal.GetHRForException(e) & 0xFFFF;
                            if (errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION)
                            {
                                continue;
                            }
                        }
                    }
                    Process.Start("explorer.exe", "/select,\"" + destinationPath + "\"");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            //AddPredefinedProcedure(comboBoxProcedureName.Text);
        }
        #region Boxes
        DialogResult ShowSetup()
        {
            SetupForm setupForm = new SetupForm();
            minimumOpacity = 1;
            DialogResult result = setupForm.ShowDialog(this);
            return result;
        }
        DialogResult AskYesNo(string message, string caption)
        {
            minimumOpacity = 1;
            var result = MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo);
            minimumOpacity = 0;
            return result;
        }
        void ShowError(string message)
        {
            minimumOpacity = 1;
            MessageBox.Show(this, message, "Error");
            minimumOpacity = 0;
        }
        void ShowWarning(string message)
        {
            minimumOpacity = 1;
            MessageBox.Show(this, message, "Warning");
            minimumOpacity = 0;
        }
        private DialogResult Show(FolderBrowserDialog dialog)
        {
            minimumOpacity = 1;
            var result = dialog.ShowDialog(this);
            minimumOpacity = 0;
            return result;
        }
        #endregion

        private void SetStatus(string status = null)
        {
            //statusStrip.Text = status;
            textBoxStatus.Text = status ?? string.Empty;
            //toolStripStatusLabel1.Text = "text";
            //this.toolStripStatusLabel1.Text = status;
        }
        private void comboBoxSuffix_Enter(object sender, EventArgs e)
        {
            comboBoxSuffix.Items.Clear();
            if (comboBoxSuffix.Items.Count > 0)
                return; // reentrancy issue
            comboBoxSuffix.Items.Add("");
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetSuffixes();
            if (result["status"] as string == "ok")
            {
                var items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxSuffix.Items.Add(item["value"] as string);
                }
            }
            else
            {
                SetStatus("While trying to get the suffix list, the database reported:" + result["reason"]);
            }
        }

        private void comboBoxDoctor_Enter(object sender, EventArgs e)
        {
            comboBoxPractitioner.Items.Clear();
            if (comboBoxPractitioner.Items.Count > 0)
                return; // reentrancy issue
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetDoctors();
            Func<Hashtable,string> f = (Hashtable h) => { return h["shortname"] as string; };
            if (result["status"] as string == "ok")
            {
                var items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxPractitioner.Items.Add(new ItemHolder(item,f));
                }
            }
            else
            {
                SetStatus("While trying to get the doctor list, the database reported:" + result["reason"]);
            }
        }

        private void comboBoxLocation_Enter(object sender, EventArgs e)
        {
            comboBoxLocation.Items.Clear();
            if (comboBoxLocation.Items.Count > 0)
                return; // reentrancy issue
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetLocations();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxLocation.Items.Add(item["name"] as string);
                }
            }
            else
            {
                SetStatus("While trying to get the location list, the database reported:" + result["reason"]);
            }
        }

        private void comboBoxProcedureName_Enter(object sender, EventArgs e)
        {
            comboBoxProcedureName.Items.Clear();
            if (comboBoxProcedureName.Items.Count > 0)
                return; // reentrancy issue;
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetProcedures();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    ItemHolder ih = new ItemHolder(item, (Hashtable h) => { return h["name"] as string; });
                    comboBoxProcedureName.Items.Add(ih);
                }
            }
            else
            {
                SetStatus("While trying to get the procedure list, the database reported:" + result["reason"]);
            }
        }

        private void comboBoxSpecialty_Enter(object sender, EventArgs e)
        {
            if (comboBoxProcedureName.SelectedIndex < 0)
            {
                comboBoxSpecialty.Items.Clear();
                return;
            }
            string procedure_name = ((comboBoxProcedureName.Items[comboBoxProcedureName.SelectedIndex] as ItemHolder).values["name"] as string);
            Func<Hashtable, string> f = (Hashtable h) =>
            {
                string specialty_name = h["specialty_name"] as string;
                return specialty_name;
            };
            comboBoxSpecialty.Items.Clear();
            if (comboBoxSpecialty.Items.Count > 0)
                return; // reentrancy issue
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result;
            Hashtable values = new Hashtable();
            values["procedure_name"] = procedure_name;
            if (patient_id < 0)
            {
                result = db.GetSpecialties(values);
                if (result["status"] as string == "ok")
                {
                    AutoCompleteStringCollection acc = new AutoCompleteStringCollection();
                    List<Hashtable> items = result["data"] as List<Hashtable>;
                    foreach (Hashtable item in items)
                    {
                        ItemHolder ih = new ItemHolder(item, f);
                        comboBoxSpecialty.Items.Add(ih);
                        acc.Add(ih.ToString());
                    }
                    comboBoxSpecialty.AutoCompleteCustomSource = acc;
                }
            }
            else
            {
                values["patient_id"] = patient_id;
                result = db.GetPatientSpecialties(values);
                if (result["status"] as string == "ok")
                {
                    List<Hashtable> items = result["data"] as List<Hashtable>;
                    AutoCompleteStringCollection acc = new AutoCompleteStringCollection();
                    foreach (Hashtable item in items)
                    {
                        if ((int)item["count"] > 0)
                        {
                            comboBoxSpecialty.Items.Add(new ItemHolder(item, f));
                            acc.Add(item["specialty_name"] as string);
                        }
                    }
                    foreach (Hashtable item in items)
                    {
                        if ((int)item["count"] == 0)
                        {
                            comboBoxSpecialty.Items.Add(new ItemHolder(item, f));
                            acc.Add(item["specialty_name"] as string);
                        }
                    }
                    comboBoxSpecialty.AutoCompleteCustomSource = acc;
                }
            }
            if (result["status"] as string == "error")
                SetStatus(result["reason"] as string);
        }

        private void comboBoxPatients_Enter(object sender, EventArgs e)
        {
            comboBoxPatients.Items.Clear();
            if (comboBoxPatients.Items.Count > 0)
                return; // reentrancy issue;
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetPatients();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> items = result["data"] as List<Hashtable>;
                Func<Hashtable, string> f = (Hashtable h) => { return string.Format("{0},{1}", h["last"], h["first"]); };
                foreach (var item in items)
                {
                    comboBoxPatients.Items.Add(new ItemHolder(item,f));
                }
            }
            else
            {
                SetStatus("While trying to get the patient list, the database reported:" + result["reason"]);
            }
        }

        private void buttonGetName_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void labelProcedurePatientFirstName_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPractitioner.Text = comboBoxPractitioner.Text.Trim();
            string val = comboBoxSuffix.SelectedItem as string;
            if (val.Trim() == string.Empty)
                return;
            if (comboBoxPractitioner.MaxLength > 0 && (comboBoxPractitioner.Text.Length + val.Length + 1) > comboBoxPractitioner.MaxLength)
            {
                SetStatus("Adding the suffix would make the doctor name too long");
                return;
            }
            SetStatus(comboBoxPractitioner.Text);
            comboBoxPractitioner.Text += "," + val;
        }

        private void buttonGetSignature_Click(object sender, EventArgs e)
        {
            Db.Db db = Db.Db.Instance();
            Hashtable parameters = new Hashtable();
            Hashtable result;
            result = db.GetPatient(patient_id);
            if (result["status"] as string == "error")
            {
                SetStatus("Error getting XXXXXXXXXX.  More info:" + result["reason"]);
                return;
            }
            Hashtable data = result["data"] as Hashtable;
            if (data["signature_document_id"] != null)
                /*
            {
                int signature_document_id = (int)result["signature_document_id"];
                Process.Start("http://localhost:50505/downloadfile/" + signature_document_id + ".bmp");
                return;
            }
            if (result["image"] != null)
            {
                 */
            {
                DialogResult dr = MessageBox.Show(text: "The XXXXXXXXXX already has a signature.  Replace it?", caption: "Warning", owner: this, buttons: MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                    return;
            }
            GetSignatureForm form = new GetSignatureForm(this);
            form.ShowDialog(this);
            if (form.Image != null)
            {
                parameters = new Hashtable();
                parameters["image"] = form.Image;
                parameters["patient_id"] = patient_id;
                result = db.AddStamp(parameters);
                if (result["status"] as string == "error")
                {
                    SetStatus("While trying to set the signature:" + result["reason"]);
                }
                else
                {
                    int file_id = (int)result["file_id"];
                    Process.Start("http://localhost:50505/downloadfile/" + file_id + ".bmp");
                }
            }
            form.Dispose();
        }

        private void comboBoxPatients_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (e.Index == -1)
                return;
            ItemHolder mp = cb.Items[e.Index] as ItemHolder;
            Hashtable p = mp.values;
            e.DrawBackground();
            e.DrawFocusRectangle();
            string name = string.Format("{0},{1}",p["last"],p["first"]);
            e.Graphics.DrawString(name, cb.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
        private void comboBoxPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBoxPatients.SelectedIndex;

            //UpdateDoctorPatientIds();
            /*
            int i = comboBoxPatients.SelectedIndex;
            if (i < 0)
            {
                toolTipPatient.Dispose();
                toolTipPatient = new ToolTip();
                return;
            }
            ItemHolder mp = comboBoxPatients.Items[i] as ItemHolder;
            string last = mp.values["last"] as string;
            string first = mp.values["first"] as string;
            DateTime dob = (DateTime)mp.values["dob"];
            string emergencyContact = mp.values["emergency_contact"] as string;
            string gender = mp.values["gender"] as string;
            string tt = string.Format("Name: {0} {1}\nDOB: {2}\nEmergency contact:", first, last, dob,emergencyContact);
            toolTipPatient.SetToolTip(comboBoxPatients, tt);
            Hashtable p = mp.values;
          */
            //GetTextBoxPatientsFirstName().Text = p["first"] as string;
            //GetTextBoxPatientsLastName().Text = p["last"] as string;
        }

        private void UpdateSelectedPatient()
        {
            return;/*
            if (comboBoxPatients.Focused)
                return;
            if (textBoxProcedurePatientsFirstName.Text.Trim() != string.Empty && textBoxProcedurePatientsLastName.Text.Trim() != string.Empty)
            {
                string searchValue = string.Format("{0},{1}",textBoxProcedurePatientsLastName.Text.Trim(),textBoxProcedurePatientsFirstName.Text.Trim());
                foreach (var o in comboBoxPatients.Items)
                {
                    if (o.ToString() == searchValue)
                    {
                        comboBoxPatients.SelectedIndex = comboBoxPatients.Items.IndexOf(o);
                        return;
                    }
                }
            }
            comboBoxPatients.SelectedIndex = -1;*/
        }
        private void textBoxProcedurePatientsLastName_TextChanged(object sender, EventArgs e)
        {
            comboBoxPatients.SelectedIndex = -1;
            //if (textBoxProcedurePatientsLastName.Focused)
//                comboBoxPatients.SelectedIndex = -1;
        }

        private void textBoxProcedurePatientsFirstName_TextChanged(object sender, EventArgs e)
        {
            comboBoxPatients.SelectedIndex = -1;
            //          if (textBoxProcedurePatientsFirstName.Focused)
    //            comboBoxPatients.SelectedIndex = -1;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void comboBoxSpecialty_DrawItem(object sender, DrawItemEventArgs e)
        {
            ItemHolder ih = comboBoxSpecialty.Items[e.Index] as ItemHolder;
//            int id = (int)ih.values["id"];
            int count = (int)(ih.values["count"] ?? 0);
            e.DrawBackground();
            bool fSelected = (DrawItemState.Selected & e.State) > 0;
            if (count > 0)
            {
                ControlPaint.DrawButton(e.Graphics, e.Bounds, fSelected ? ButtonState.Pushed : ButtonState.Normal);
                e.Graphics.DrawString(ih.ToString(), e.Font, new SolidBrush(SystemColors.ControlText), e.Bounds.Location);
            }
            else
                e.Graphics.DrawString(ih.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Location);
        }
        void UpdateAddPatientButton()
        {
            string first = (patient_first ?? "").Trim();
            string last = (patient_last ?? "" ).Trim();
            if (first == string.Empty || last == string.Empty || patient_id >= 0 || comboBoxPatients.Text.Trim() == string.Empty)
            {
                buttonAddPatient.Enabled = false;
                return;
            }
            buttonAddPatient.Enabled = true;
        }
        void UpdateAddLocationButton()
        {
            if (comboBoxLocation.SelectedIndex > 0)
            {
                buttonNewLocation.Enabled = false;
                return;
            }
            if (comboBoxLocation.Text.Trim() == string.Empty)
            {
                buttonNewLocation.Enabled = false;
                return;
            }
            buttonNewLocation.Enabled = true;
        }
        void UpdateAddDoctorButton()
        {
            if (comboBoxPractitioner.Text.Trim() == string.Empty)
            {
                buttonAddPractitioner.Enabled = false;
                return;
            }
            foreach (ItemHolder ih in comboBoxPractitioner.Items)
            {
                if (comboBoxPractitioner.Text == ih.ToString())
                {
                    buttonAddPractitioner.Enabled = false;
                    return;
                 }
            }
            buttonAddPractitioner.Enabled = true;
  //          comboBoxPractitioner.SelectedIndex = -1;
        }

        private void comboBoxDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddPractitioner.Enabled = comboBoxPractitioner.SelectedIndex < 0;
        }


        private void buttonAddNewDoctor_Click(object sender, EventArgs e)
        {
            AddDoctor addDoctorForm = new AddDoctor(comboBoxPractitioner.Text.Trim());
            DialogResult r = addDoctorForm.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.Cancel)
                return;
            Db.Db db = Db.Db.Instance();
            Hashtable parms = new Hashtable();
            parms["firstName"] = addDoctorForm.DoctorProperties.FirstName;
            parms["lastName"] = addDoctorForm.DoctorProperties.LastName;
            parms["shortName"] = comboBoxPractitioner.Text.Trim();
            parms["address1"] = addDoctorForm.DoctorProperties.Address1;
            parms["address2"] = addDoctorForm.DoctorProperties.Address2;
            parms["address3"] = addDoctorForm.DoctorProperties.Address3;
            parms["city"] = addDoctorForm.DoctorProperties.City;
            parms["locality1"] = addDoctorForm.DoctorProperties.Locality1;
            parms["locality2"] = addDoctorForm.DoctorProperties.Locality2;
            parms["postalCode"] = addDoctorForm.DoctorProperties.PostalCode;
            parms["country"] = addDoctorForm.DoctorProperties.Country;
            parms["voice"] = addDoctorForm.DoctorProperties.Telephone;
            parms["fax"] = addDoctorForm.DoctorProperties.Fax;
            parms["email"] = addDoctorForm.DoctorProperties.Email;
            parms["contact"] = addDoctorForm.DoctorProperties.ContactPerson;
            Hashtable result;
            result = db.AddDoctor(parms);
            if (result["status"] as string == "ok")
                return;
            SetStatus("An error happened while trying to add a doctor.  More information:" + result["reason"] as string);
        }

        private void buttonEditPractitioner_Click(object sender, EventArgs e)
        {
            SetStatus();
            if (comboBoxPractitioner.SelectedIndex < 0)
                return;
            string shortname = comboBoxPractitioner.Text.Trim();
            Db.Db db = Db.Db.Instance();
            Hashtable parms = new Hashtable();
            DoctorProperties dp = new DoctorProperties();
            ItemHolder ih = comboBoxPractitioner.SelectedItem as ItemHolder;
            Hashtable values = ih.values;
            dp.FirstName = values["firstname"] as string;
            dp.LastName = values["lastname"] as string;
            dp.Address1 = values["address1"] as string;
            dp.Address2 = values["address2"] as string;
            dp.Address3 = values["address3"] as string;
            dp.City = values["city"] as string;
            dp.ContactPerson = values["contact_person"] as string;
            dp.Country = values["country"] as string;
            dp.Email = values["email"] as string;
            dp.Fax = values["fax"] as string;
            dp.Locality1 = values["locality1"] as string;
            dp.Locality2 = values["locality2"] as string;
            dp.PostalCode = values["postal_code"] as string;
            dp.Telephone = values["telephone"] as string;
            AddDoctor addDoctor = new AddDoctor(name: shortname, edit: true);
            addDoctor.DoctorProperties = dp;
            DialogResult r = addDoctor.ShowDialog(this);
            if (r == DialogResult.Cancel)
                return;
            Hashtable values2 = new Hashtable();
            values2["id"] = doctor_id;
            values2["firstname"] = dp.FirstName;
            values2["lastname"] = dp.LastName;
            values2["address1"] = dp.Address1;
            values2["address2"] = dp.Address2;
            values2["address3"] = dp.Address3;
            values2["city"] = dp.City;
            values2["contact_person"] = dp.ContactPerson;
            values2["country"] = dp.Country;
            values2["email"] = dp.Email;
            values2["fax"] = dp.Fax;
            values2["locality1"] = dp.Locality1;
            values2["locality2"] = dp.Locality2;
            values2["postal_code"] = dp.PostalCode;
            values2["telephone"] = dp.Telephone;
            Hashtable result = db.UpdateDoctor(values2);
            if (result["status"] as string != "ok")
                SetStatus(result["reason"] as string);
            ih.values = values2;
        }

        private void buttonAddPatient_Click(object sender, EventArgs e)
        {
            string first = (patient_first ?? "").Trim();
            string last = (patient_last ?? "").Trim();
            string name = comboBoxPatients.Text.Trim();
            AddPatient AddPatientForm = new AddPatient(name);
            DialogResult r = AddPatientForm.ShowDialog(this);
            if (r == DialogResult.Cancel)
                return;
            Hashtable result;
            Hashtable parms = new Hashtable();
            Db.Db db = Db.Db.Instance();
            parms["firstName"] = first;
            parms["lastName"] = last;
            parms["dateOfBirth"] = AddPatientForm.PatientProperties.DateOfBirth.ToString();
            parms["gender"] = AddPatientForm.PatientProperties.Gender;
            parms["emergencyContact"] = AddPatientForm.PatientProperties.EmergencyContact;
            result = db.AddPatient(parms);
            if (result["status"] as string == "ok")
            {
                comboBoxPatients.SelectedIndex = -1;
                return;
            }
            SetStatus("An error occurred while trying to add a XXXXXXXXXX.  More info:" + result["reason"]);
        }

        private void comboBoxPatients_MouseHover(object sender, EventArgs e)
        {
            //ToolTip tt = new ToolTip();

        }

        private void buttonNewLocation_Click(object sender, EventArgs e)
        {
            SetStatus();
            string name;
            name = comboBoxLocation.Text.Trim();
            AddLocation addLocation = new AddLocation(name);
            DialogResult r = addLocation.ShowDialog(this);
            if (r == DialogResult.Cancel)
                return;
            Db.Db db = Db.Db.Instance();
            Hashtable values = new Hashtable();
            values["address1"] = addLocation.LocationProperties.Address1;
            values["address2"] = addLocation.LocationProperties.Address2;
            values["address3"] = addLocation.LocationProperties.Address3;
            values["city"] = addLocation.LocationProperties.City;
            values["contact_person"] = addLocation.LocationProperties.ContactPerson;
            values["country"] = addLocation.LocationProperties.Country;
            values["email"] = addLocation.LocationProperties.Email;
            values["fax"] = addLocation.LocationProperties.Fax;
            values["locality1"] = addLocation.LocationProperties.Locality1;
            values["locality2"] = addLocation.LocationProperties.Locality2;
            values["postal_code"] = addLocation.LocationProperties.PostalCode;
            values["telephone"] = addLocation.LocationProperties.Telephone;
            values["name"] = name;
            Hashtable result = db.AddLocation(values);
            if (result["status"] as string != "ok")
            {
                SetStatus(string.Format("Error adding location.  Reason:{0}", result["reason"]));
            }
        }

        private void buttonGetRelease_Click(object sender, EventArgs e)
        {
            var p = comboBoxPatients.SelectedItem as ItemHolder;
            Get_Release g = new Get_Release(p);
            DialogResult r = g.ShowDialog(this);
        }
        private void RemoveDoctorSpecialtyIds()
        {
            foreach (object o in comboBoxSpecialty.Items)
            {
                ItemHolder ih = o as ItemHolder;
                ih.values.Remove("doctor_specialty_id");
            }
        }
        private void UpdateDoctorSpecialtyIds()
        {
            int si = comboBoxPractitioner.SelectedIndex;
            if (si < 0)
            {
                RemoveDoctorSpecialtyIds();
                return;
            }
        }
        
        private void RemoveDoctorPatientIds()
        {
            foreach (object o in comboBoxPractitioner.Items)
            {
                ItemHolder ih = o as ItemHolder;
                ih.values.Remove("doctor_patient_id");
            }
        }
        private void UpdateDoctorPatientIds()
        {
            int si = comboBoxPatients.SelectedIndex;
            if (si < 0)
            {
                RemoveDoctorPatientIds();
                return;
            }
            ItemHolder ih = comboBoxPatients.Items[si] as ItemHolder;
            Hashtable values = new Hashtable();
            values["patient_id"] = patient_id;
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetDoctorsForPatient(values);
            if (result["status"] as string != "ok")
                return;
            var data = result["data"] as List<Hashtable>;
            foreach (Hashtable item in data)
            {
                int doctor_id = (int)item["doctor_id"];
                int doctor_patient_id = (int)item["doctor_patient_id"];
                foreach (object o in comboBoxPractitioner.Items)
                {
                    ItemHolder ih2 = o as ItemHolder;
                    int doctor_id2 = (int)ih2.values["doctor_id"];
                    if (doctor_id == doctor_id2)
                    {
                        ih2.values["doctor_patient_id"] = doctor_patient_id;
                    }
                }
            }
        }

        private void comboBoxTarget_Enter(object sender, EventArgs e)
        {
            comboBoxTarget.Items.Clear();
            if (comboBoxTarget.Items.Count > 0)
                return; // reentrancy issue
            if(comboBoxProcedureName.SelectedIndex < 0)
                return;
            string procedure_name = (comboBoxProcedureName.Items[comboBoxProcedureName.SelectedIndex] as ItemHolder).ToString() ;
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable values = new Hashtable();
            values["procedure_name"] = procedure_name;
            Hashtable result = db.GetTargets(values);
            if (result["status"] != "ok")
                return;
            List<Hashtable> data = result["data"] as List<Hashtable>;
            foreach (Hashtable item in data)
            {
                comboBoxTarget.Items.Add(item["target_name"]);// make an ItemHolder?
            }
        }

        private void comboBoxProcedureName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSpecialty.Items.Clear();
            comboBoxSpecialty.Text = string.Empty;
            comboBoxTarget.Items.Clear();
            comboBoxTarget.Text = string.Empty;
        }

        private void buttonItems_Click(object sender, EventArgs e)
        {
            int si = comboBoxPatients.SelectedIndex;
            if (si < 0)
                return;

        }

        void UpdateAddEditPatientButtons()
        {
            string s = comboBoxPatients.Text.Trim().ToLower();
            if (s == string.Empty)
            {
                buttonAddPatient.Enabled = false;
                buttonEditPatient.Enabled = false;
                return;
            }
            foreach (ItemHolder ih in comboBoxPatients.Items)
            {
                string s2 = ih.ToString().ToLower().Trim();
                if (s == s2)
                {
                    buttonAddPatient.Enabled = false;
                    buttonEditPatient.Enabled = true;
                    return;
                }
            }
            buttonAddPatient.Enabled = true;
            buttonEditPatient.Enabled = false;
        }
        void UpdateAddEditDoctorButtons()
        {
            string s = comboBoxPractitioner.Text.Trim().ToLower();
            if (s == string.Empty)
            {
                buttonAddPractitioner.Enabled = false;
                buttonEditPractitioner.Enabled = false;
                return;
            }
            foreach (ItemHolder ih in comboBoxPractitioner.Items)
            {
                string s2 = ih.ToString().ToLower().Trim();
                if (s == s2)
                {
                    buttonAddPractitioner.Enabled = false;
                    buttonEditPractitioner.Enabled = true;
                    return;
                }
            }
            buttonAddPractitioner.Enabled = true;
            buttonEditPractitioner.Enabled = false;
        }
        private void comboBoxPatients_TextChanged(object sender, EventArgs e)
        {
            UpdateAddEditPatientButtons();
        }
        private void comboBoxDoctor_TextChanged(object sender, EventArgs e)
        {
            UpdateAddEditDoctorButtons();
        }

    }
}
