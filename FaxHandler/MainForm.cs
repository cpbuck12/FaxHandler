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
        public string LastName
        {
            get { return GetTextBoxPatientsLastName().Text; }
        }
        public string FirstName
        {
            get { return GetTextBoxPatientsFirstName().Text; }
        }
        #endregion
        #region Control Accessors
        DateTimePicker GetDateTimePicker()
        {
            return dateTimePickerProcedure;
        }
        Button GetButtonSave(bool concierge)
        {
            if (concierge)
                return buttonSaveConciergeProcedure;
            else
                return buttonSaveProcedure;
        }
        ComboBox GetTextBoxDoctor()
        {
            return comboBoxDoctor;
        }
        TextBox GetTextBoxPages()
        {
            return textBoxProcedurePages;
        }
        TextBox GetTextBoxDate()
        {
            return textBoxProcedureDate;
        }
        ComboBox GetComboBoxLocation()
        {
            return comboBoxLocation;
        }
        TextBox GetTextBoxPatientsFirstName()
        {
            return textBoxProcedurePatientsFirstName;
        }
        TextBox GetTextBoxPatientsLastName()
        {
            return textBoxProcedurePatientsLastName;
        }
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
            string date = GetTextBoxDate().Text;
            string fileName = date
                + "--" + comboBoxProcedureName.Text
                    + "--" + GetComboBoxLocation().Text 
                    + "--" + GetTextBoxDoctor().Text
                    + "--" + PatientsDirectoryName().TrimAll() 
                    + "--" + userName
                    + suffix + ".PDF";
            return fileName;
        }
        string PatientsDirectoryName()
        {
            string result = string.Format("{0}, {1}", GetTextBoxPatientsLastName().Text, GetTextBoxPatientsFirstName().Text).ToUpper(); ;
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
                GetTextBoxPages().Enabled = true;
            }
            else
            {
                TextBox textBox = GetTextBoxPages();
                textBox.Enabled = false;
                textBox.Clear();
            }
        }
        bool ValidPageRange(int numberOfPages)
        {
            PageRanges pageRanges;
            string s = GetTextBoxPages().Text.TrimAll();
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
                PageRanges pageRanges = new PageRanges(GetTextBoxPages().Text.TrimAll());
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
            bool patientNameFilled = GetTextBoxPatientsFirstName().Text.Trim().Length > 0 && GetTextBoxPatientsLastName().Text.Trim().Length > 0;
            bool doctorNameFilled = GetTextBoxDoctor().Text.Trim().Length > 0;
            
            if (GetTextBoxPages().Text.Trim().Length > 0
                && GetTextBoxDate().Text.Trim().Length > 0
                && doctorNameFilled
                && GetComboBoxLocation().Text.Trim().Length > 0
                && patientNameFilled
                && ValidPageRange(document.Pages))
            {
                dragger1.Full = GetButtonSave(concierge: true).Enabled = GetButtonSave(concierge: false).Enabled = true;
            }
            else
            {
                dragger1.Full = GetButtonSave(concierge: true).Enabled = GetButtonSave(concierge: false).Enabled = false;
            }
            buttonCreateRelease.Enabled = patientNameFilled && doctorNameFilled;
            buttonGetSignature.Enabled = comboBoxPatients.SelectedIndex >= 0;

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
            DirectoryInfo[] dirs;

            try
            {
                directoryInfoMain = new DirectoryInfo(concierge ? Properties.Settings.Default.ConciergeLocation : Properties.Settings.Default.LastSavedDirectory);
            }
            catch (SecurityException /*securityException*/)
            {
                ShowError("You do not have permission to access " 
                    + (concierge ? "the Concierge directory" : Properties.Settings.Default.LastSavedDirectory));
                return;
            }
            catch (ArgumentException /*argumentException*/)
            {
                ShowError("There is an invalid character in " 
                    + (concierge ? "either the name of the Concierge directory, or the name of a directory above it" : Properties.Settings.Default.LastSavedDirectory));
                return;
            }
            catch (PathTooLongException /*pathTooLongException*/)
            {
                ShowError((concierge ? "The Concierge folder is too far deep.  Try moving it to a higher directory"
                    : Properties.Settings.Default.LastSavedDirectory + " is too far deep"));
                return;
            }
            if (directoryInfoMain.Exists)
            {
                startingDirectory = directoryInfoMain.FullName;
                if (concierge)
                {
                    try
                    {
                        dirs = directoryInfoMain.GetDirectories(PatientsDirectoryName(),SearchOption.TopDirectoryOnly);
                    }
                    catch (ArgumentException argumentException)
                    {
                        ShowError("The patient's name contains invalid characters.\n" + argumentException.Message);
                        return;
                    }
                    catch (DirectoryNotFoundException directoryNotFoundException)
                    {
                        ShowError("It was not possible to read the Concierge directory\n" + directoryNotFoundException.Message);
                        return;
                    }
                    catch (UnauthorizedAccessException unauthorizedAccessException)
                    {
                        ShowError("Permission denied reading the Concierge directory\n" + unauthorizedAccessException.Message);
                        return;
                    }
                    if (dirs.Length == 1)
                    {
                        startingDirectory = dirs[0].FullName;
                    }
                    else if(dirs.Length > 1)
                    {
                        ShowError("There seems to be more than directory with the patient's name.  Fix this and try again later");
                        return;
                    }
                    else
                    {
                        ShowError("The patient's directory does not exist.  Fix this and try again later.");
                        return;
                    }
                }
            }
            else
            {
                if(concierge)
                    ShowError("Cannot find the Concierge directory.");
                else
                    ShowError("Cannot find " + Properties.Settings.Default.LastSavedDirectory);
                return;
            }
            DialogResult result;
            string destinationDirectory;
            string location = string.Empty;
            if (concierge)
            {
                ConciergeBrowser browser = new ConciergeBrowser(startingDirectory);
                result = browser.ShowDialog(this);
                destinationDirectory = browser.SelectedDirectory.FullName;
                DirectoryInfo[] path;
                foreach (var p in browser.SelectedPath.Skip(1))
                {
                    string input = p.Name.Trim();
                    string core = Regex.Replace(input,"#$",s => ( string.Empty ));
                    location += core + "_";
                } 
                path = browser.SelectedPath;
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.SelectedPath = startingDirectory;
                result = Show(dialog);
                destinationDirectory = dialog.SelectedPath;
            }
            if (result == DialogResult.OK)
            {
                /*
                    (GetTextBoxDate(saveType).Text)
                    + (saveType == SaveType.Procedure ? " " + textBoxProcedureName.Text + " " :  " CONSULT ")
                    + GetTextBoxLocation(saveType).Text + " " + GetTextBoxDoctor(saveType).Text + " " +
                    PatientsDirectoryName(saveType).TrimAll() + (concierge ? location : "_nonconcierge" ) + ".PDF";
                 */
                PageRanges pageRanges;
                if (GetTextBoxPages().Text.Trim() == string.Empty)
                    pageRanges = PageRanges.All;
                else
                {
                    try
                    {
                        pageRanges = new PageRanges(GetTextBoxPages().Text);
                    }
                    catch (Exception)
                    {
                        ShowError("Wasn't able to parse the page range");
                        return;
                    }
                }
                string destinationPath;
                try
                {

                    PDF.Document trimmedDocument = document.TrimPages(pageRanges);
                    if (trimmedDocument == null)
                    {
                        ShowError("Couldn't trim and save the new PDF.");
                        return;
                    }
                    string fileName;
                    fileName = GenerateFilename((concierge ? "--SVC--" + location : "--SV--")  + TimeStamp());
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
                catch (Exception exception)
                {
                    ShowError("Acrobat reported an error when extracting the pages.\n" + exception.Message);
                    return;
                }
                if (checkBoxView.Checked)
                {
                    int tries = 5;
                    int sleepTime = 1 * 1000; // one second
                    for (int i = 0; i < tries;i++ )
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
                    if(concierge)
                        Process.Start("explorer.exe", "/select,\"" + startingDirectory + "\"");
                    else
                        Process.Start("explorer.exe", "/select,\"" + destinationPath + "\"");
                    System.Threading.Thread.Sleep(1000);
                }
                AddPredefinedProcedure(comboBoxProcedureName.Text);
                if (concierge)
                {
                    UpdateDirectoryLabels(destinationPath);
                }
            }
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
        class Minidoctor
        {
            public Hashtable values;
            public Minidoctor(Hashtable values)
            {
                this.values = values;
            }
            public override string ToString()
            {
                return values["shortname"] as string;
            }
        }

        private void comboBoxDoctor_Enter(object sender, EventArgs e)
        {
            comboBoxDoctor.Items.Clear();
            if (comboBoxDoctor.Items.Count > 0)
                return; // reentrancy issue
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetDoctors();
            if (result["status"] as string == "ok")
            {
                var items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxDoctor.Items.Add(new Minidoctor(item));
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
                    comboBoxLocation.Items.Add(item["value"] as string);
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
                    comboBoxProcedureName.Items.Add(item["value"] as string);
                }
            }
            else
            {
                SetStatus("While trying to get the procedure list, the database reported:" + result["reason"]);
            }
        }
        class Minipatient
        {
            public Hashtable values;
            public Minipatient(Hashtable values)
            {
                this.values = values;
            }
            public override string ToString()
            {
                return string.Format("{0},{1}", values["last"],values["first"]);
            }
        }

        class Minispecialty
        {
            Hashtable values;
            public Minispecialty(Hashtable values)
            {
                this.values = values;
            }
            public override string ToString()
            {
                string spec = values["specialty"] as string;
                string subspec = (values["subspecialty"] ?? "") as string;
                if (subspec != string.Empty)
                    return string.Format("{0}/{1}", spec, subspec);
                return spec;
            }
        }
        private void comboBoxSpecialty_Enter(object sender, EventArgs e)
        {
            comboBoxSpecialty.Items.Clear();
            if (comboBoxSpecialty.Items.Count > 0)
                return; // reentrancy issue
            SetStatus();
            Db.Db db = Db.Db.Instance();
            Hashtable result = db.GetSpecialties();
            if (result["status"] as string == "ok")
            {
                List<Hashtable> items = result["data"] as List<Hashtable>;
                foreach (var item in items)
                {
                    comboBoxSpecialty.Items.Add(new Minispecialty(item));
                }
            }
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
                foreach (var item in items)
                {
                    comboBoxPatients.Items.Add(new Minipatient(item));
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
            comboBoxDoctor.Text = comboBoxDoctor.Text.Trim();
            string val = comboBoxSuffix.SelectedItem as string;
            if (val.Trim() == string.Empty)
                return;
            if (comboBoxDoctor.MaxLength > 0 && (comboBoxDoctor.Text.Length + val.Length + 1) > comboBoxDoctor.MaxLength)
            {
                SetStatus("Adding the suffix would make the doctor name too long");
                return;
            }
            comboBoxDoctor.Text += "," + val;
        }

        private void buttonGetSignature_Click(object sender, EventArgs e)
        {
            Db.Db db = Db.Db.Instance();
            Hashtable parameters = new Hashtable();
            Hashtable result;
            var p = comboBoxPatients.SelectedItem as Minipatient;
            parameters["patient_id"] = p.values["id"];
            result = db.GetStamp(parameters);
            if (result["status"] == "error")
            {
                SetStatus("Error checking preexisting signature.  More info:" + result["reason"]);
                return;
            }
            if (result["image"] != null)
            {
                DialogResult dr = MessageBox.Show(text: "The patient already has a signature.  Replace it?", caption: "Warning", owner: this, buttons: MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                    return;
            }
            GetSignatureForm form = new GetSignatureForm(this);
            form.ShowDialog(this);
            if (form.Image != null)
            {
                parameters = new Hashtable();
                parameters["image"] = form.Image;
                parameters["patient_id"] = (int)p.values["id"];
                result = db.AddStamp(parameters);
                if (result["status"] == "error")
                {
                    SetStatus("While trying to set the signature:"+result["reason"]);
                }
            }
            form.Dispose();
        }

        private void comboBoxPatients_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (e.Index == -1)
                return;
            Minipatient mp = cb.Items[e.Index] as Minipatient;
            Hashtable p = mp.values;
            e.DrawBackground();
            e.DrawFocusRectangle();
            string name = string.Format("{0},{1}",p["last"],p["first"]);
            e.Graphics.DrawString(name, cb.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }

        private void comboBoxPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBoxPatients.SelectedIndex;
            if (i < 0)
            {
                return;
            }
            Minipatient mp = comboBoxPatients.Items[i] as Minipatient;
            Hashtable p = mp.values;
            GetTextBoxPatientsFirstName().Text = p["first"] as string;
            GetTextBoxPatientsLastName().Text = p["last"] as string;
        }

        private void UpdateSelectedPatient()
        {
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
            comboBoxPatients.SelectedIndex = -1;
        }
        private void textBoxProcedurePatientsLastName_TextChanged(object sender, EventArgs e)
        {
            //if (textBoxProcedurePatientsLastName.Focused)
//                comboBoxPatients.SelectedIndex = -1;
        }

        private void textBoxProcedurePatientsFirstName_TextChanged(object sender, EventArgs e)
        {
  //          if (textBoxProcedurePatientsFirstName.Focused)
    //            comboBoxPatients.SelectedIndex = -1;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            SetStatus();
        }

    }
}
