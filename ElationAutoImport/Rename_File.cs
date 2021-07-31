using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Windows.Forms;
using ElationAutoImport.Properties;
using IronOcr;
using ElationAutoImport.medicalRecords;

//Try to put the parsing logic in its own class.
//It is likely that the Adobe and iText public key are messing each other up. See if keeing them seperated does anything.
//You can do it! Don't worry about it.
namespace ElationAutoImport
{

    public partial class Rename_File : UserControl
    {
        public ListBox showInfo;
        public GeneralForm currentFile;
        public List<(string, GeneralForm)> scannedFiles = new List<(string, GeneralForm)>();
        public int currentFileIndex;
        private const string SAVED_SETTINGS = "savedFolder";
        private const string RENAME_NAME = "Rename_File";
        private Dictionary<string, string> faxNumbers = new Dictionary<string, string>();
        private bool hasClicked;
        private int currentTotal;
        private int totalFiles;
        private string openSettings;
        private string folderPath;

        private PatientFiles showList;
        public Rename_File()
        {
            InitializeComponent();
            hasClicked = false; 
            patientFile.Hide();
            currentTotal = 0;
            folderPath = "";
            progressBar1.Style = ProgressBarStyle.Continuous;
            XmlDocument doc = new XmlDocument();
            //string settingsPath = System.IO.Path.GetFullPath(SAVED_SETTINGS);
            //Console.WriteLine("Path: " + settingsPath);
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var savedPath = Path.Combine(outPutDirectory, "ResourceFiles\\savedFolder.xml");
            savedPath = savedPath.Replace(@"file:\", "");
            openSettings = savedPath;
            doc.Load(openSettings);
            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if(node.SelectSingleNode("Name").InnerText == RENAME_NAME)
                {
                    folderPath = node.SelectSingleNode("Path").InnerText;
                    break;
                }
            }
            patientText.Text = "";
            
        }


        private void nameChange(string oldName, string newName,ref int fileCount)
        {
            /*if (!File.Exists(fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf"))
            {
                File.Move(@medicalRecord, fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf");
                //MessageBox.Show(fileDir + @"\" + "HRA_X-ray Report" + HMC_count + ".pdf");
            }*/
            bool isRenamed = false;
            /*string findDirPath = @"PathRename.txt";
            string dirPath = System.IO.Path.GetFullPath(findDirPath);
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
            }*/
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string faxPath = Path.Combine(outPutDirectory, "ResourceFiles\\faxNumbers.txt");
            string savedPath = Path.Combine(outPutDirectory, "ResourceFiles\\savedNames.xml");
            string loadPath = Path.Combine(outPutDirectory, "ResourceFiles\\cpcpCheckNames.xml");
            faxPath = faxPath.Replace(@"file:\", "");
            savedPath = savedPath.Replace(@"file:\", "");
            loadPath = loadPath.Replace(@"file:\", "");
            doc.Load(savedPath);
            doc.Save(loadPath);
            bool readPrescriptions = false;
            if(!Directory.Exists(folderPath)) 
            {
                MessageBox.Show("Please select a folder path!");
                return;
            }
            totalFiles = progressBar1.Maximum = Directory.GetFiles(folderPath).Length;
            currentTotal = 0;
            progressBar1.Value = 0;
            tasksCompleted.Text = currentTotal + "/" + totalFiles;
            string [] faxFile= System.IO.File.ReadAllLines(faxPath);
            if (faxNumbers.Count == 0)
            {
                for (int i = 0; i < faxFile.Length; i++)
                {
                    if (faxFile[i] == "PRESCRIPTIONS {" || readPrescriptions == true)
                    {
                        readPrescriptions = true;
                        faxNumbers.Add(faxFile[i], "Prescriptions");
                        if (faxFile[i] == "}")
                            readPrescriptions = false;
                    }
                }
                Console.WriteLine(faxNumbers);
            }
            Console.WriteLine(faxFile);
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }  
        }

        public static string bingPathToAppDir(string localPath)
        {
            string currentDir = Environment.CurrentDirectory;
            MessageBox.Show(currentDir);
            DirectoryInfo directory = new DirectoryInfo(
                Path.GetFullPath(Path.Combine(currentDir, @"\ResourceFiles\" + localPath)));
            return directory.ToString();
        }

        private void patientText_TextChanged(object sender, EventArgs e)
        {

        }
        private void serviceText_TextChanged(object sender, EventArgs e)
        {


        }
        private void orderingText_TextChanged(object sender, EventArgs e)
        {

        }

        private void documentText_TextChanged(object sender, EventArgs e)
        {

        }
        private void reviewingText_TextChanged(object sender, EventArgs e)
        {

        }
        private void prcedureText_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            scannedFiles.Clear(); //MAYBE REMOVE
            //Progress Bar of the files being scanned
            //var Ocr = new IronTesseract();
            bool isDesktop = true;
            tasksCompleted.Text = currentTotal + "/" + totalFiles;
            foreach (string sourceFile in Directory.GetFiles(folderPath))
            {
                bool scanFile = false;
                bool foundType = false;
                string currentFile = System.IO.Path.GetFileNameWithoutExtension(sourceFile);
                string[] checkFaxNumber = currentFile.Split('_');
                if(checkFaxNumber.Length != 0)
                {
                    if (faxNumbers.ContainsKey(checkFaxNumber[checkFaxNumber.Length - 1]))
                    {
                        Console.WriteLine(System.IO.Path.GetFileName(sourceFile));
                        printFile addFile = new printFile("PRINT - Prescription");
                        scannedFiles.Add((sourceFile, addFile));
                        currentTotal++;
                        backgroundWorker1.ReportProgress(1); //Redundant var. Only used to start function.
                        continue;
                    } else
                    {
                        scanFile = true;
                    }
                }
                if (scanFile == true)
                {
                    bool isHRA = false;
                    string fileName = sourceFile;
                    CaptureText pleaseWork = new CaptureText(sourceFile);
                    string text = pleaseWork.ReturnInfo();
                    text = text.Replace("©", "0");
                    string[] textArray = text.Split('\n');
                    if (foundType == false && textArray[0].Contains("Hawaii Radiologic Associates"))
                    {
                        hraFile addFile = new hraFile(textArray);
                        scannedFiles.Add((sourceFile, addFile));
                        foundType = true;

                    }
                    if (foundType == false && textArray[0].Contains("Hilo Medical Center"))
                    {
                        if (text.ToString().Contains("Otolaryngology Service "))
                        {
                            hmcClinicFile addFile = new hmcClinicFile(textArray, 4);
                            scannedFiles.Add((sourceFile, addFile));
                            foundType = true;
                        }
                        else if (text.ToString().Contains("Diagnostic Imaging Report"))
                        {
                            hmcFile addFile = new hmcFile(textArray,1);
                            scannedFiles.Add((sourceFile, addFile));
                            foundType = true;
   
                        }
                        else if (text.ToString().Contains("ED Provider:"))
                        {
                            Console.WriteLine(fileName);
                            hmcFile addFile = new hmcFile(textArray, 2);
                            scannedFiles.Add((sourceFile, addFile));
                            foundType = true;
                        }

                        //test.printInfo();
                    }
                    if (foundType == false && textArray[0].Contains("Hawaii Pacific Oncology Center"))
                    {
                        if (text.ToString().Contains("Linda Gemer"))
                        {
                            hmcClinicFile addFile = new hmcClinicFile(textArray, 1);
                            scannedFiles.Add((sourceFile, addFile));
                            foundType = true;
                        }

                        else
                        {
                            hmcClinicFile addFile = new hmcClinicFile(textArray, 2);
                            scannedFiles.Add((sourceFile, addFile));
                            foundType = true;
                        }

                    }
                    if (foundType == false && textArray[0].Contains("Hilo UrologyClinic"))
                    {
                        hmcClinicFile addFile = new hmcClinicFile(textArray, 3);
                        scannedFiles.Add((sourceFile, addFile));
                        foundType = true;
                    }
                    if (textArray[0].Contains("Queens Medical"))
                    {
                     
                        if (text.ToString().Contains("H&P"))
                        {
                            QueensMedicalFile addFile = new QueensMedicalFile(textArray, 1);
                            scannedFiles.Add((sourceFile, addFile));
                        }
                        else if (text.ToString().Contains("ED PROVIDER"))
                        {
                            QueensMedicalFile addFile = new QueensMedicalFile(textArray, 2);
                            scannedFiles.Add((sourceFile, addFile));
                        }
                    }
                    if (foundType == false && text.ToString().Contains("East Hawaii Health General Surgery"))
                    {
                        hmcClinicFile addFile = new hmcClinicFile(textArray, 5);
                        scannedFiles.Add((sourceFile, addFile));
                        foundType = true;
                    }
                    if (foundType == false && text.ToString().IndexOf("COASTAL MEDICAL SUPPLY", 0, StringComparison.CurrentCultureIgnoreCase) != -1 && text.ToString().IndexOf("CPAP/BIPAP and Supplies", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        coastalMedicalFile addFile = new coastalMedicalFile(textArray, 1);
                        scannedFiles.Add((sourceFile, addFile));
                        foundType = true;
                    }
                    currentTotal++;
                    backgroundWorker1.ReportProgress(1); //Redundant var. Only used to start function.
                }
            }
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Increment(1);
            tasksCompleted.Text = currentTotal + "/" + totalFiles;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            showList = new PatientFiles(scannedFiles, this);
            //showList.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openSettings);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.SelectSingleNode("Name").InnerText == RENAME_NAME)
                    {
                        folderPath = node.SelectSingleNode("Path").InnerText = FBD.SelectedPath;
                        doc.Save(openSettings);
                        break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)

        {
            if (patientFile.src == null || showList == null || fileText.Text == "") //No file to rename;   
            {
                return;
            }
            if(!fileText.Text.Contains(".pdf"))
            {
                fileText.Text += ".pdf";
            }
            int count = 1;
            string oldPath = patientFile.src.ToString().Replace(@"file://", "");
            string fullPath = folderPath + @"\" + fileText.Text;
            string fileNameOnly = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            string extension = System.IO.Path.GetExtension(fullPath);
            string path = System.IO.Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;

            if(oldPath == fullPath)
            {
                showList.RemoveDocument();
                return;
            }
            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = System.IO.Path.Combine(path, tempFileName + extension);
            }
            File.Move(oldPath, newFullPath);
            showList.RemoveDocument();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showList.RemoveDocument();
        }

        private void fileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void RenameFile(string sourceFile, GeneralForm file)
        {
            if (file.fileName == "")
            {
                return;
            }
            int count = 1;
            string fileName = file.fileName;

            fileName = fileName.Replace(@"\", ".");
            fileName = fileName.Replace(":", "");
            fileName = fileName.Replace("<", "");
            fileName = fileName.Replace(">", "");
            fileName = fileName.Replace('"', ' ');
            fileName = fileName.Replace("|", "");
            fileName = fileName.Replace("?", "");
            fileName = fileName.Replace("*", "");
            fileName = fileName.Replace("/", ".");
            string oldPath = sourceFile;
            string fullPath = folderPath + @"\" + fileName  + ".pdf";
            string fileNameOnly = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            string extension = System.IO.Path.GetExtension(fullPath);
            string path = System.IO.Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;

            if (oldPath == fullPath)
            {
                return;
            }


            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = System.IO.Path.Combine(path, tempFileName + ".pdf");
            }
            File.Move(oldPath, newFullPath);
        }


        public void ResetDocument()
        {
            patientFile.src = null;
            patientText.Text = "";
            serviceText.Text = "";
            orderingText.Text = "";
            documentText.Text = "";
            reviewingText.Text = "";
            prcedureText.Text = "";
            fileText.Text = "";
        }

        private void patientFile_Enter(object sender, EventArgs e)
        {

        }

        private void Rename_File_Load(object sender, EventArgs e)
        {

        }
    }
}
