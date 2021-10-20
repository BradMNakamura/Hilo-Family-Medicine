using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Windows.Forms;

namespace ElationAutoImport
{

    public partial class Rename_File : UserControl
    {
        public ListBox showInfo;
        public GeneralForm currentFile;
        public List<(string, GeneralForm)> scannedFiles;
        public List<(string, GeneralForm)> missedFiles;
        public int currentFileIndex;
        private const string RENAME_NAME = "Rename_File";
        private HashSet<string> faxNumbers;
        private int currentTotal;
        private int totalFiles;
        private string openSettings;
        private string folderPath;
        private const string test = @"ResourceFiles\savedFolder.xml";
        private const string test1 = @"ResourceFiles\faxNumbers.xml";
        private PatientFiles showList;
        private bool openSaved;
        public Rename_File()
        {
            //DO NOT READ FILES IN CONSTRUCTOR. CRASHES PROGRAM.
            InitializeComponent();
            patientFile.Hide();
            currentTotal = 0;
            folderPath = "";
            progressBar1.Style = ProgressBarStyle.Continuous;
            openSaved = false;
            scannedFiles = new List<(string, GeneralForm)>();
            missedFiles = new List<(string, GeneralForm)>();
            faxNumbers = new HashSet<string>();
        }

        private void FolderPath()
        {
            XmlDocument doc = new XmlDocument();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var savedPath = Path.Combine(outPutDirectory, test);
            savedPath = savedPath.Replace(@"file:\", "");
            openSettings = savedPath;
            doc.Load(@savedPath); //Comment this out to edit Form1
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.SelectSingleNode("Name").InnerText == RENAME_NAME)
                {
                    folderPath = node.SelectSingleNode("Path").InnerText;
                    break;
                }
            }
            openSaved = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(openSaved == false)
                FolderPath();
            XmlDocument doc = new XmlDocument();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string faxPath = Path.Combine(outPutDirectory, @"ResourceFiles\faxNumbers.xml");
            string savedPath = Path.Combine(outPutDirectory, @"ResourceFiles\savedNames.xml");
            string loadPath = Path.Combine(outPutDirectory, @"ResourceFiles\cpcpCheckNames.xml");
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var savedPath = Path.Combine(outPutDirectory, test1);
            savedPath = savedPath.Replace(@"file:\", "");
            doc.Load(savedPath);
            string temp = "";
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (!faxNumbers.Contains(node.InnerText)) faxNumbers.Add(node.InnerText);
            }
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
                    bool found = false;
                    foreach (string potentialCheck in checkFaxNumber)
                    {
                        if (faxNumbers.Contains(potentialCheck))
                        {
                            Console.WriteLine(System.IO.Path.GetFileName(sourceFile));
                            printFile addFile = new printFile("PRINT - Prescription");
                            scannedFiles.Add((sourceFile, addFile));
                            found = true;
                        }
                    }
                    if (!found) scanFile = true;
                }
                if (scanFile)
                {
                    Traverse_Files(sourceFile, 1);
                }
                currentTotal++;
                backgroundWorker1.ReportProgress(1); //Redundant var. Only used to start function.
            }
        }

        private void Traverse_Files(string sourceFile, int captureType)
        {
            bool isHRA = false;
            string fileName = sourceFile;
            string text = "";
            
            if (captureType == 1)
            {
                CaptureAdvText pleaseWork = new CaptureAdvText(sourceFile);
                text = pleaseWork.ReturnInfo();
                //if (text.Length == 0) MessageBox.Show(sourceFile);
                text = text.Replace("©", "0");
            }
            else
            {
                CaptureSimpleText pleaseWork = new CaptureSimpleText(sourceFile);
                text = pleaseWork.ReturnInfo();
                //if (text.Length == 0) MessageBox.Show("2" + sourceFile);
                text = text.Replace("©", "0");
            }
            text = text.Replace("©", "0");
            string[] textArray = text.Split('\n');
            //Console.WriteLine (sourceFile);
            //Console.WriteLine(text.ToString());
            //continue;

            if (textArray[0].Contains("Hawaii Radiologic Associates"))
            {
                hraFile addFile = new hraFile(textArray);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Hilo Medical Center"))
            {
                if (text.ToString().Contains("Otolaryngology Service "))
                {
                    hmcClinicFile addFile = new hmcClinicFile(textArray, 4);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("Diagnostic Imaging Report"))
                {
                    hmcFile addFile = new hmcFile(textArray, 1);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("ED Provider:"))
                {
                    hmcFile addFile = new hmcFile(textArray, 2);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("Transthoracic"))
                {
                    hmcClinicFile addFile = new hmcClinicFile(textArray, 8);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("Health Orthopedics"))
                {
                    hmcClinicFile addFile = new hmcClinicFile(textArray, 9);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else
                {
                    DefaultForm addFile = new DefaultForm("No Implementation");
                    missedFiles.Add((sourceFile, addFile));
                }

                //test.printInfo();
            }
            else if (textArray[0].Contains("Hilo Neurology Clinic"))
            {
                hmcClinicFile addFile = new hmcClinicFile(textArray, 6);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("EHHC GASTROENTEROLOGY"))
            {
                hmcClinicFile addFile = new hmcClinicFile(textArray, 7);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Hawaii Pacific Oncology Center"))
            {
                if (text.ToString().Contains("Linda Gemer"))
                {
                    hmcClinicFile addFile = new hmcClinicFile(textArray, 1);
                    scannedFiles.Add((sourceFile, addFile));
                }

                else
                {
                    hmcClinicFile addFile = new hmcClinicFile(textArray, 2);
                    scannedFiles.Add((sourceFile, addFile));
                }

            }
            else if (textArray[0].Contains("Hilo UrologyClinic"))
            {
                hmcClinicFile addFile = new hmcClinicFile(textArray, 3);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Queens Medical"))
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
                else if (text.ToString().Contains("ISLAND UROLOGY"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 3);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("Pulmonary"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 4);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("HEAD AND NECK"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 5);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("ALFRED J LIU"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 6);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("LANCE K MITSUNAGA"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 7);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else if (text.ToString().Contains("CV INTERVENTIONAL"))
                {
                    QueensMedicalFile addFile = new QueensMedicalFile(textArray, 8);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else
                {
                    DefaultForm addFile = new DefaultForm("No Implementation");
                    missedFiles.Add((sourceFile, addFile));
                }
            }
            else if (text.ToString().Contains("East Hawaii Health General Surgery"))
            {
                hmcClinicFile addFile = new hmcClinicFile(textArray, 5);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().IndexOf("COASTAL MEDICAL SUPPLY", 0, StringComparison.CurrentCultureIgnoreCase) != -1 && text.ToString().IndexOf("CPAP/BIPAP and Supplies", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                coastalMedicalFile addFile = new coastalMedicalFile(textArray, sourceFile, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Dr Smigel"))
            {
                if (text.ToString().Contains("Electrophysiological Report"))
                {
                    SmigelOffice addFile = new SmigelOffice(textArray, 2);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else
                {
                    SmigelOffice addFile = new SmigelOffice(textArray, 1);
                    scannedFiles.Add((sourceFile, addFile));
                }
            }
            else if (textArray[0].Contains("BIG ISLAND FOOT CARE INC"))
            {
                BigIslandFoot addFile = new BigIslandFoot(textArray, 1);

            }
            else if (textArray[0].Contains("Fax Hawaii Vision Specialists"))
            {
                HawaiiVisionSpecialist addFile = new HawaiiVisionSpecialist(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Kelly M"))
            {
                KellyWachi addFile = new KellyWachi(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Hilo Eye Center"))
            {
                HiloEyeCenter addFile = new HiloEyeCenter(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("OAHU PAIN CARE"))
            {
                OahuPainCare addFile = new OahuPainCare(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("VA PALO ALTO"))
            {
                VAClinic addFile = new VAClinic(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("PCMC"))
            {
                PunaCommunityMedical addFile = new PunaCommunityMedical(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("Kauka Express"))
            {
                KaukaExpress addFile = new KaukaExpress(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("COVENANT"))
            {
                CovenantPathology addFile = new CovenantPathology(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("HMSA"))
            {
                if (text.ToString().Contains("HOSPITAL POST DISCHARGE PLAN"))
                {
                    HMSA addFile = new HMSA(textArray, 1);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else
                {
                    DefaultForm addFile = new DefaultForm("No Implementation");
                    missedFiles.Add((sourceFile, addFile));
                }
            }
            else if (textArray[0].Contains("HylaFAX"))
            {
                HylaFax addFile = new HylaFax(textArray);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (textArray[0].Contains("rthopaedic Associates of Hawaii"))
            {
                OrthopedicAssociates addFile = new OrthopedicAssociates(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Hilo Urgent Care"))
            {
                HiloUrgentCare addFile = new HiloUrgentCare(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Michael B Russo"))
            {
                MichealRusso addFile = new MichealRusso(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("OAHawaii"))
            {
                OrthopedicAssociates addFile = new OrthopedicAssociates(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Hearing Doctors of Hawaii"))
            {
                HearingDoctor addFile = new HearingDoctor(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("HawaiiNephrologist"))
            {
                HawaiiNephrologist addFile = new HawaiiNephrologist(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Curtis Lee, M.D"))
            {
                CurtisLee addFile = new CurtisLee(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("HawaiiPacificHealth"))
            {
                HawaiiPacificHealth addFile = new HawaiiPacificHealth(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("BIG ISLAND DERM"))
            {
                DefaultForm addFile = new DefaultForm("Charles Mauro MD");
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Eye Care Hawaii"))
            {
                EyeCareHawaii addFile = new EyeCareHawaii(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Mahinakealo Dermatology"))
            {
                MahinakealoDermatology addFile = new MahinakealoDermatology(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("The Endoscopy Center"))
            {
                EndoscopyCenter addFile = new EndoscopyCenter(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("Gastroenterology Associates"))
            {
                GastroenterologyAssociates addFile = new GastroenterologyAssociates(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("DEPARTMENT OF OB/GYN OFFICE"))
            {
                DeptOBGYN addFile = new DeptOBGYN(textArray, 1);
                scannedFiles.Add((sourceFile, addFile));
            }
            else if (text.ToString().Contains("101 Aupuni Street, Suite 305")) //Glenn Kunimura OD Inc
            {
                if (text.ToString().Contains("EXAMINATION RECORD"))
                {
                    GlennKunimura addFile = new GlennKunimura(textArray, 1);
                    scannedFiles.Add((sourceFile, addFile));
                }
                else
                {
                    DefaultForm addFile = new DefaultForm("No Implementation");
                    missedFiles.Add((sourceFile, addFile));
                }
            }
            else
            {
                DefaultForm addFile = new DefaultForm("No Implementation");
                missedFiles.Add((sourceFile, addFile));
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Increment(1);
            tasksCompleted.Text = currentTotal + "/" + totalFiles;
        }

       
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int scanCount = 0;
            int missedCount = 0;
            if (scannedFiles.Count == null) scanCount = 0;
            else scanCount = scannedFiles.Count;
            if (missedFiles.Count == null) missedCount = 0;
            else missedCount = missedFiles.Count;

            tasksCompleted.Text = "Scanned: " + scannedFiles.Count + "  Missed: " + missedFiles.Count;
            foreach ((string, GeneralForm) x in missedFiles.ToArray())
            {
                Traverse_Files(x.Item1, 2);
                missedFiles.RemoveAt(0);
            }


            foreach ((string, GeneralForm) x in missedFiles)
            {
                scannedFiles.Add(x);
            }
            missedFiles = null; //Release Memory;
            showList = new PatientFiles(scannedFiles, this);
            //showList.Show();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openSaved == false)
                FolderPath();
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

            fileText.Text = fileText.Text.Replace(@"\", ".");
            fileText.Text = fileText.Text.Replace(":", "");
            fileText.Text = fileText.Text.Replace("<", "");
            fileText.Text = fileText.Text.Replace(">", "");
            fileText.Text = fileText.Text.Replace('"', ' ');
            fileText.Text = fileText.Text.Replace("|", "");
            fileText.Text = fileText.Text.Replace("?", "");
            fileText.Text = fileText.Text.Replace("*", "");
            fileText.Text = fileText.Text.Replace("/", ".");
            string oldPath = patientFile.src.ToString().Replace(@"file://", "");
            string fullPath = folderPath + @"\" + fileText.Text;
            string fileNameOnly = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            string extension = System.IO.Path.GetExtension(fullPath);
            string path = System.IO.Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;



            if (oldPath == fullPath)
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

        private void prcedureText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
