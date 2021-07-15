using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ElationAutoImport
{

    public partial class Rename_File : UserControl
    {
        const string HRA_CODE = "8089353565";
        const string HMC_CODE = "8089323000";
        public Rename_File()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string findDirPath = @"PathRename.txt";
            string dirPath = Path.GetFullPath(findDirPath);
            if(!File.Exists(dirPath))
            {
                MessageBox.Show("Please add a folder path\n");
                return;
            }
            string fileDir = File.ReadAllText(@dirPath);
            string[] fileList = Directory.GetFiles(fileDir + @"\");
            
            int HMC_count = 1; //What to name file i.e HRA Xray 1
            int HRA_count = 1;
            int fileCount = fileList.Length;
            int afterCount = 0;
            foreach (string medicalRecord in fileList)
            {
                if (medicalRecord.Contains(HMC_CODE))
                {
                    nameChange(medicalRecord, "HRA Xray Report",ref HMC_count);
                   
                }
                if (medicalRecord.Contains(HRA_CODE))
                {
                    nameChange(medicalRecord, "HMC X-Ray Report", ref HRA_count);
                }
                afterCount++;
            }
            string showMessage = "Files Renamed!\n";
            showMessage += "File Count Before: " + fileCount + "\nFile Count After: " + afterCount;
            MessageBox.Show(showMessage);
        }

        private void nameChange(string oldName, string newName,ref int fileCount)
        {
            /*if (!File.Exists(fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf"))
            {
                File.Move(@medicalRecord, fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf");
                //MessageBox.Show(fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf");
            }*/
            bool isRenamed = false;
            string findDirPath = @"PathRename.txt";
            string dirPath = Path.GetFullPath(findDirPath);
            string fileDir = File.ReadAllText(@dirPath);
            while (isRenamed == false)
            {
                if (!File.Exists(fileDir + @"\" + newName + fileCount + ".pdf"))
                {
                    //MessageBox.Show(fileDir + @"\" + newName + fileCount + ".pdf");
                    File.Move(@oldName, fileDir + @"\" + newName + fileCount + ".pdf");
                    isRenamed = true;
                }
                fileCount++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = @"PathRename.txt";
            string path = Path.GetFullPath(fileName);
            FolderBrowserDialog renameXrays = new FolderBrowserDialog();
            if (renameXrays.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(@path, renameXrays.SelectedPath);
                string text = File.ReadAllText(@path);
                MessageBox.Show("This is the file path: " + text);
            }
        }
    }
}
