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

namespace FaxHandler
{
    public partial class MainForm : Form
    {
        #region Declarations
        enum SaveType
        {
            Consultation,
            Procedure
        }
        string fileName;
        bool tempFile;
        AcroAVDoc document;
        Timer timer;
        double minimumOpacity = 0.0;
        bool suspendValidation = false;
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
            Save(SaveType.Procedure, concierge: true);
        }
        private void buttonSaveProcedure_Click(object sender, EventArgs e)
        {
            Save(SaveType.Procedure, concierge: false);
        }
        private void buttonSaveConsult_Click(object sender, EventArgs e)
        {
            Save(SaveType.Consultation, concierge: false);
        }
        private void buttonSaveConciergeConsult_Click(object sender, EventArgs e)
        {
            Save(SaveType.Consultation, concierge: true);
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
        private void buttonCopyToProcedure_Click(object sender, EventArgs e)
        {
            Copy(SaveType.Consultation);
        }

        private void buttonCopyToConsult_Click(object sender, EventArgs e)
        {
            Copy(SaveType.Procedure);
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (suspendValidation)
                return;
            string val = textBox.Text.Trim();
            Regex r = new Regex(@"\s+");
            if (r.IsMatch(val))
            {
                ShowError("No embedded spaces are allowed");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            textBox.Text = val.ToUpper();
            UpdateSaveButtons();
        }
        private void dateTimePickerConsult_ValueChanged(object sender, EventArgs e)
        {
            textBoxConsultDate.Text = DateToString(dateTimePickerConsult.Value);
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
        DateTimePicker GetDateTimePicker(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return dateTimePickerConsult;
            else
                return dateTimePickerProcedure;
        }
        Button GetButtonSave(SaveType saveType, bool concierge)
        {
            if (saveType == SaveType.Consultation)
                if (concierge)
                    return buttonSaveConciergeConsult;
                else
                    return buttonSaveConsult;
            else
                if (concierge)
                    return buttonSaveConciergeProcedure;
                else
                    return buttonSaveProcedure;
        }
        TextBox GetTextBoxDoctor(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultDoctor;
            else
                return textBoxProcedureDoctor;
        }
        TextBox GetTextBoxPages(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultPages;
            else
                return textBoxProcedurePages;
        }
        TextBox GetTextBoxDate(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultDate;
            else
                return textBoxProcedureDate;
        }
        TextBox GetTextBoxLocation(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultLocation;
            else
                return textBoxProcedureLocation;
        }
        TextBox GetTextBoxPatientsFirstName(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultPatientsFirstName;
            else
                return textBoxProcedurePatientsFirstName;
        }
        TextBox GetTextBoxPatientsLastName(SaveType saveType)
        {
            if (saveType == SaveType.Consultation)
                return textBoxConsultPatientsLastName;
            else
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
        // TODO: handle, somewhere in this file, moving files across system volumes.  its most likely unhandled and untested
        #region Utilities
        string PatientsDirectoryName(SaveType saveType)
        {
            string result = string.Format("{0}, {1}", GetTextBoxPatientsLastName(saveType).Text, GetTextBoxPatientsFirstName(saveType).Text).ToUpper(); ;
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
        void UpdateDirectoryLabels(string fileName,SaveType saveType)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                DirectoryInfo di = fileInfo.Directory;
                string patientDirName = PatientsDirectoryName(saveType);
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
                document.Close(1); // no save
                document = null;
                if (tempFile)
                    File.Delete(fileName);
            }
        }
        int GetAvailablePages()
        {
            if (!document.IsValid())
                return 0;
            CAcroPDDoc pdoc = document.GetPDDoc();
            return pdoc.GetNumPages();
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
            string newFileName;
            bool isTempFile;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                newFileName = files[0];
                isTempFile = false;
            }
            else
            {
                //wrap standard IDataObject in OutlookDataObject
                OutlookDataObject dataObject = new OutlookDataObject(e.Data);

                //get the names and data streams of the files dropped
                string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");

                if (filenames.Length != 1 || !IsPdfFileName(filenames[0]))
                    return;
                MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");
                isTempFile = true;
                newFileName = Path.GetTempFileName() + filenames[0];

                try
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    FileStream tempFileStream = File.Create(fileName);
                    fileInfo.Attributes = FileAttributes.Temporary;
                    filestreams[0].WriteTo(tempFileStream);
                    tempFileStream.Close();
                }
                catch (System.Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            try
            {
                CloseDocument();
                tempFile = isTempFile;
                fileName = newFileName;
                document = new AcroAVDoc();
                document.Open(fileName, "");
                document.BringToFront();
                document.SetViewMode(2); // PDUseThumbs
                CAcroAVPageView pageView = document.GetAVPageView() as CAcroAVPageView;
                pageView.ZoomTo(1 /*AVZoomFitPage*/, 100);
            }
            catch (System.Exception ex)
            {
                ShowError(ex.Message);
                fileName = null;
                tempFile = false;
            }
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
                if (!document.IsValid())
                {
                    document = null;
                }
            if (document != null)
            {
                foreach (SaveType saveType in Enum.GetValues(typeof(SaveType)))
                    GetTextBoxPages(saveType).Enabled = true;
            }
            else
            {
                foreach (SaveType saveType in Enum.GetValues(typeof(SaveType)))
                {
                    TextBox textBox = GetTextBoxPages(saveType);
                    textBox.Enabled = false;
                    textBox.Clear();
                }
            }
        }
        void UpdateSaveButtons()
        {
            foreach (SaveType saveType in Enum.GetValues(typeof(SaveType)))
            {
                if (GetTextBoxPages(saveType).Text.Trim().Length > 0
                    && GetTextBoxDate(saveType).Text.Trim().Length > 0
                    && GetTextBoxDoctor(saveType).Text.Trim().Length > 0
                    && GetTextBoxLocation(saveType).Text.Trim().Length > 0
                    && GetTextBoxPatientsFirstName(saveType).Text.Trim().Length > 0
                    && GetTextBoxPatientsLastName(saveType).Text.Trim().Length > 0)
                {
                    GetButtonSave(saveType, concierge: true).Enabled = true;
                    GetButtonSave(saveType, concierge: false).Enabled = true;
                }
                else
                {
                    GetButtonSave(saveType, concierge: true).Enabled = false;
                    GetButtonSave(saveType, concierge: false).Enabled = false;
                }

            }
        }

        private void Copy(SaveType from)
        {
            SaveType to = (from == SaveType.Procedure ? SaveType.Consultation : SaveType.Procedure);
            if (GetTextBoxDate(from).Text.Length != 0)
                GetDateTimePicker(to).Value = GetDateTimePicker(from).Value;
            if (GetTextBoxDoctor(from).Text.Length != 0)
                GetTextBoxDoctor(to).Text = GetTextBoxDoctor(from).Text;
            if (GetTextBoxLocation(from).Text.Length != 0)
                GetTextBoxLocation(to).Text = GetTextBoxLocation(from).Text;
            if (GetTextBoxPatientsFirstName(from).Text.Length != 0)
                GetTextBoxPatientsFirstName(to).Text = GetTextBoxPatientsFirstName(from).Text;
            if (GetTextBoxPatientsLastName(from).Text.Length != 0)
                GetTextBoxPatientsLastName(to).Text = GetTextBoxPatientsLastName(from).Text;
        }
        #endregion
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
            if (text.Trim().Length == 0)
            {
                e.Cancel = false;
                return;
            }
            if (!document.IsValid())
            {
                ShowError("The document has been closed");
                e.Cancel = true;
            }
            int pageCount = GetAvailablePages();
            PageRange pageRange = new PageRange();
            try
            {
                pageRange.Range = text;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                e.Cancel = true;
                return;
            }
            if (pageRange.End > pageCount)
            {
                ShowError(String.Format("There are only {0} page(s) in the doucment",pageCount));
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
        }
        private void Save(SaveType saveType, bool concierge)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string startingDirectory = null;
            DirectoryInfo directoryInfoMain;
            DirectoryInfo[] dirs;
            bool overwrite = false;

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
                        dirs = directoryInfoMain.GetDirectories(PatientsDirectoryName(saveType),SearchOption.TopDirectoryOnly);
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
            dialog.SelectedPath = startingDirectory;
            var result = Show(dialog);
            if (result == DialogResult.OK)
            {
                string tempPath = Path.GetTempFileName() + ".PDF";
                string tempPathPortable = GetPortablePath(tempPath);
                string fileName =
                    (GetTextBoxDate(saveType).Text)
                    + (saveType == SaveType.Procedure ? " " + textBoxProcedureName.Text + " " :  " CONSULT ")
                    + GetTextBoxLocation(saveType).Text + " " + GetTextBoxDoctor(saveType).Text + " " +
                    PatientsDirectoryName(saveType) + ".PDF";
                string destinationPath = dialog.SelectedPath +
                    (dialog.SelectedPath.Last() != '\\' ? @"\" : String.Empty) + fileName;

                if (File.Exists(destinationPath))
                {
                    string prompt = String.Format(
                        "The file \"{0}\" already exists in folder \"{1}\". "+
                        "Do you want to replace it?  If you do, the original file will be lost forever.",
                        fileName, destinationPath);
                    result = AskYesNo(message:prompt,caption: "Warning");
                    if (result == DialogResult.No)
                        return;
                    overwrite = true;
                }
                PageRange pageRange = new PageRange();
                pageRange.Range = GetTextBoxPages(saveType).Text;
                CAcroPDDoc pdoc = document.GetPDDoc();
                object jsObject = pdoc.GetJSObject();
                Type T = jsObject.GetType();
                object[] parameters = {/* pdoc,*/ tempPathPortable, pageRange.Begin-1, pageRange.End-1 };

                try
                {
                    T.InvokeMember("ExtractPagesToFile",
                        BindingFlags.InvokeMethod |
                        BindingFlags.Public |
                        BindingFlags.Instance,
                        null, // no binder
                        jsObject,
                        parameters);
                }
                catch (Exception exception)
                {
                    ShowError("Acrobat reported an error when extracting the pages.\n" + exception.Message);
                    return;
                }
                if (overwrite)
                {
                    try
                    {
                        File.Copy(tempPath, destinationPath, overwrite:true);
                    }
                    catch (Exception exceptionOnFileCopy)
                    {
                        ShowError("Error copying the temporary file over the original file.\n" + exceptionOnFileCopy.Message);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        File.Move(tempPath, destinationPath);
                    }
                    catch (Exception exceptionOnFileMove)
                    {
                        ShowError("Error moving the temporary file to the final destination.\n" + exceptionOnFileMove.Message);
                        return;
                    }
                }
                if(checkBoxView.Checked)
                    Process.Start("explorer.exe", "/select,\"" + destinationPath + "\"");
                if (saveType == SaveType.Procedure)
                {
                    AddPredefinedProcedure(textBoxProcedureName.Text);
                }
                if (concierge)
                {
                    UpdateDirectoryLabels(destinationPath,saveType);
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
