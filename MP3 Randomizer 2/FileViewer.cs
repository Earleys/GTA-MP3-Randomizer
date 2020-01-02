using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MP3_Randomizer_2
{
    public partial class FileViewer : Form
    {
        string path = "";
        bool issueFound = false;
        bool isRenamed = false;
        int issues = 0;
        public FileViewer()
        {
            InitializeComponent();
        }

        public FileViewer(string path, bool isRenamed)
        {
            InitializeComponent();
            this.path = path;
            this.isRenamed = isRenamed;
        }

        private void FileViewer_Load(object sender, EventArgs e)
        {
            lblInfo.MaximumSize = new Size(this.Width - btnRemove.Width - 10, this.Height);
            ReloadFiles();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will remove ALL invalid files in the currently selected path (all files marked in orange). This will also get rid of all album covers in this path. Are you sure you want to continue (you can't revert this!)? ", "Confirm deletion", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DirectoryInfo d = new DirectoryInfo(path);
                FileInfo[] fileArray = d.GetFiles();
                foreach (FileInfo file in fileArray) // for every file
                {
                    if (file.Extension != ".mp3" && file.Extension != ".lnk" || IsAllDigits(Path.GetFileNameWithoutExtension(file.FullName)) && !isRenamed)
                    {
                        file.Delete();
                        issues = 0;
                    }
                }
            }
            ReloadFiles();
        }

        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public void ReloadFiles()
        {
            txtFiles.Clear();
            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
                FileInfo[] fileArray = d.GetFiles();
                txtFiles.AppendText("FILES"+Environment.NewLine+"******************************************************");
                foreach (FileInfo file in fileArray) // for every file
                {
                    string[] f = Directory.GetFiles(path);
                    if (file.Extension != ".mp3" && file.Extension != ".lnk" || IsAllDigits(Path.GetFileNameWithoutExtension(file.FullName)) && !isRenamed)
                    {
                        AppendText(this.txtFiles, Environment.NewLine + file.FullName, Color.OrangeRed);
                        issueFound = true;
                        issues++;
                    }
                    else
                    {
                        txtFiles.AppendText(Environment.NewLine + file.FullName);
                    }

                }

                lblInfo.Text = "This window shows you all files that were found in the selected folder. Some of these files might be invisible to you (like album covers). They can however cause issues. These are marked in orange.\r\nFound " + issues + " potential issues.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Unable to open this window without selecting a path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
         
        }

        public void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void FileViewer_SizeChanged(object sender, EventArgs e)
        {
            lblInfo.MaximumSize = new Size(this.Width - btnRemove.Width - 10, this.Height);
        }
    }
}
