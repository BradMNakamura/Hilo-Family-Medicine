using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
namespace ElationAutoImport
{
    public partial class customFile : UserControl
    {
        private List<string> scannedFiles;
        private int selectedIndex;
        public customFile()
        {
            selectedIndex = -1;
            scannedFiles = new List<string>();
            InitializeComponent();
            //patientFile.Hide();
        }

        private void patientFile_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                string[] fileNames = Directory.GetFiles(FBD.SelectedPath);
                listBox1.Items.Clear();
                foreach (string patientFile in fileNames)
                {
                    listBox1.Items.Add(System.IO.Path.GetFileName(patientFile));
                    scannedFiles.Add(patientFile);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex= listBox1.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            //
            patientFile.src = scannedFiles[selectedIndex];
            patientFile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newFilename = filenameText.Text;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Please select a file.");
                return;
            }

            string newName = scannedFiles[selectedIndex].Replace(listBox1.Items[selectedIndex].ToString(), "");
            newName += filenameText.Text;
            //Try to rename file
            int addANumber = 0;
            string checkFile = newName + ".pdf";
            if (File.Exists(checkFile))
            {
                do
                {
                    checkFile = newName + "[" + addANumber.ToString() + "].pdf";
                    addANumber++;
                    MessageBox.Show(checkFile);
                } while ((File.Exists(checkFile)));
            }
            {}
            newName = checkFile;
            System.IO.File.Move(scannedFiles[selectedIndex].ToString(), newName);
            scannedFiles[selectedIndex] = newName;
            listBox1.Items[selectedIndex] = System.IO.Path.GetFileName(newName);
            //Catch if name is taken.
            //While loop to run
            filenameText.Text = "";
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
           /*PdfReader reader = new PdfReader(scannedFiles[selectedIndex]);
            int pagesCount = reader.NumberOfPages;
            PdfDictionary page = reader.GetPageN(1);
            PdfNumber rotate = page.GetAsNumber(PdfName.ROTATE);
            page.Put(PdfName.ROTATE, new PdfNumber(90));
            FileStream fs = new FileStream("created.pdf", FileMode.Create,
            FileAccess.Write, FileShare.None);
            PdfStamper stamper = new PdfStamper(reader, fs);
            //patientFile.src = stamper.ToString();
            //patientFile.Show();*/
        }

        private void filenameText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
