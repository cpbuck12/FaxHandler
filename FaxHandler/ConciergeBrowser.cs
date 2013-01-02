using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace FaxHandler
{
    public partial class ConciergeBrowser : Form
    {
        string startingDirectory;
        DirectoryInfo selectedDirectory;
        public DirectoryInfo SelectedDirctory
        {
            get
            {
                return selectedDirectory;
            }
        }
        public ConciergeBrowser(string startingDirectory)
        {
            this.startingDirectory = startingDirectory;
            InitializeComponent();
        }
        TreeNode CreateTreeNode(DirectoryInfo directoryInfo, TreeNode parent)
        {
            var newTreeNode = new TreeNode(directoryInfo.Name);
            var dictionary = new Dictionary<string, object>();
            newTreeNode.Tag = dictionary;
            dictionary.Add("DirectoryInfo", directoryInfo);
            if (parent != null)
                parent.Nodes.Add(newTreeNode);
            return newTreeNode;
        }

        Object GetNodeValue(TreeNode node, string key)
        {
            if (node == null || node.Tag == null)
                return null;
            var dictionary = node.Tag as Dictionary<string, object>;
            return dictionary[key];
        }
        DirectoryInfo GetDirectoryInfo(TreeNode node)
        {
            return GetNodeValue(node, "DirectoryInfo") as DirectoryInfo;
        }

        private void ConciergeBrowser_Load(object sender, EventArgs e)
        {
            treeViewDirectories.Nodes.Clear();
            string root = Properties.Settings.Default.ConciergeLocation;
            DirectoryInfo directoryInfoRoot = new DirectoryInfo(root);
            TreeNode treeNodeRoot = CreateTreeNode(directoryInfoRoot,null);
            treeViewDirectories.Nodes.Add(treeNodeRoot);
            var stack = new Stack<TreeNode>();
            stack.Push(treeNodeRoot);
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                try
                {
                    DirectoryInfo[] directoryInfos = GetDirectoryInfo(top).GetDirectories();
                    Regex rName = new Regex(@"^[a-zA-Z]+, [a-zA-Z]+$");
                    Regex rNonName = new Regex(@"^[0-9].+$");
                    foreach (var d in directoryInfos)
                    {
                        bool doIt = false;
                        if (top.Parent == null)
                            if (rName.IsMatch(d.Name))
                                doIt = true;
                            else
                                doIt = false;
                        else
                            if (rNonName.IsMatch(d.Name))
                                doIt = false;
                            else
                                doIt = true;
                        if(doIt)
                        {
                            var t = CreateTreeNode(d, top);
                            stack.Push(t);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                } 
            }
            var sequence = (from TreeNode node in treeNodeRoot.Nodes.Cast<TreeNode>()
                                        where GetDirectoryInfo(node).FullName == startingDirectory
                                        select node);
            if (sequence.Count() == 1)
            {
                TreeNode patientTreeNode = sequence.First();
                treeViewDirectories.SelectedNode = patientTreeNode;
                patientTreeNode.EnsureVisible();
                patientTreeNode.Expand();
            }
        }

        private void treeViewDirectories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null || e.Node.Parent.Parent == null)
                buttonSave.Enabled = false;
            else
                buttonSave.Enabled = true;
            selectedDirectory = GetDirectoryInfo(e.Node);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
