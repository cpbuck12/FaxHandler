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
                            ShowError("That is not a valid directory, please try again.");
                            break;
                    }
                    else // cancel
                    {
                        ShowWarning("This program cannot continue until you set the location of the Concierge directory.\n" +
                            "The program will now quit.");
                        Application.Exit();
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
            timer.Stop();
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
            Regex r = new Regex(@"\-+");
            if (r.IsMatch(val))
            {
                ShowError("No embedded hyphens are allowed");
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
        TextBox GetTextBoxDoctor()
        {
            return textBoxProcedureDoctor;
        }
        TextBox GetTextBoxPages()
        {
            return textBoxProcedurePages;
        }
        TextBox GetTextBoxDate()
        {
            return textBoxProcedureDate;
        }
        TextBox GetTextBoxLocation()
        {
            return textBoxProcedureLocation;
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
            string fileName = GetTextBoxDate().Text.Replace("-","")
                + "--PROCEDURE--" + textBoxProcedureName.Text
                    + "--LOCATION--" + GetTextBoxLocation().Text 
                    + "--DOCTOR--" + GetTextBoxDoctor().Text
                    + "--PATIENT--" + PatientsDirectoryName().TrimAll() 
                    + "--USER--" + userName
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
            return string.Format("{0:D2}-{1:D2}-{2}", dateTime.Month, dateTime.Day, dateTime.Year);
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
            textBoxProcedureName.AutoCompleteCustomSource = collection;
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
            textBoxProcedureName.AutoCompleteCustomSource = collection;
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
                if(files != null && files.Length == 1)
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
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        void UpdateSaveButtons()
        {

            if (GetTextBoxPages().Text.Trim().Length > 0
                && GetTextBoxDate().Text.Trim().Length > 0
                && GetTextBoxDoctor().Text.Trim().Length > 0
                && GetTextBoxLocation().Text.Trim().Length > 0
                && GetTextBoxPatientsFirstName().Text.Trim().Length > 0
                && GetTextBoxPatientsLastName().Text.Trim().Length > 0
                && ValidPageRange(document.Pages))
            {
                dragger1.Full = GetButtonSave(concierge: true).Enabled = GetButtonSave(concierge: false).Enabled = true;
            }
            else
            {
                dragger1.Full = GetButtonSave(concierge: true).Enabled = GetButtonSave(concierge: false).Enabled = false;
            }
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
                AddPredefinedProcedure(textBoxProcedureName.Text);
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
    }
}
