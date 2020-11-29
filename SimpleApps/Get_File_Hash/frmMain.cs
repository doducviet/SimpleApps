//    Copyright(C) 2020  Viet Do <https://github.com/doducviet>
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Get_File_Hash
{
    public partial class frmMain : Form
    {
        private string hashFileType = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetHashFileType("SHA1");
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                dgv.Rows.Add(file, GetHashFile(file));
            }
        }

        private string GetHashFile(string file)
        {
            string hasFile = string.Empty;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c certutil -hashfile \"" + file + "\" " + hashFileType;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    hasFile = reader.ReadToEnd();

                    hasFile = hashFileType + " : " + hasFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList()[1];
                }
            }

            return hasFile;
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void SHA1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHashFileType("SHA1");
        }

        private void SHA256ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHashFileType("SHA256");
        }

        private void SHA512ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHashFileType("SHA512");
        }

        private void MD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHashFileType("MD5");
        }

        private void SetHashFileType(string newHashFileType)
        {
            hashFileType = newHashFileType;
            this.Text = "Get File Hash <" + hashFileType + ">";
        }
    }
}
