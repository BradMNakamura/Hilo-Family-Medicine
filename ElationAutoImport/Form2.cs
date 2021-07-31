using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using ElationAutoImport.medicalRecords;
namespace ElationAutoImport
{
    public partial class PatientFiles : Form
    {
        public List<(string, GeneralForm)> scannedFiles;
        public Rename_File mainWindow;
        public List<object> changeLabels;

        public PatientFiles(List<(string, GeneralForm)> inputFiles, Rename_File currentWindow)
        {
            InitializeComponent();
            scannedFiles = inputFiles;
            mainWindow = currentWindow;
            if (scannedFiles.Count != 0)
            {
                fileList.Items.Clear();
                for (int i = 0; i < scannedFiles.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileName(scannedFiles[i].Item1);
                    fileList.Items.Add(fileName);
                }
                mainWindow.patientFile.src = scannedFiles[0].Item1;
                GeneralForm printOutput = scannedFiles[0].Item2;
                if (printOutput.docType == "Prescription")
                {
                    string fileName = System.IO.Path.GetFileName(scannedFiles[0].Item1);
                    mainWindow.fileText.Text = printOutput.docType + "_" + fileName;
                }
                else
                {
                    mainWindow.patientText.Text = printOutput.patientName.Replace(@"/", ".");
                    mainWindow.serviceText.Text = printOutput.appointmentDate.Replace(@"/", ".");
                    mainWindow.orderingText.Text = printOutput.reviewerName.Replace(@"/", ".");
                    mainWindow.documentText.Text = printOutput.docType.Replace(@"/", ".");
                    mainWindow.reviewingText.Text = printOutput.ccName.Replace(@"/", ".");
                    mainWindow.prcedureText.Text = printOutput.procedureDesc.Replace(@"/", ".");
                    mainWindow.fileText.Text = printOutput.fileName.Replace(@"/", ".");
                }
                mainWindow.currentFile = scannedFiles[0].Item2;
                mainWindow.patientFile.Show();
            }
        }

        public void RemoveDocument()
        {
 
            if (fileList.SelectedIndex != -1 || fileList.Items.Count > 1)
            {
                int index = 0;
                GeneralForm printOutput = scannedFiles[index].Item2;
                if (printOutput.reviewerName == "COASTAL MEDICAL SUPPLY CPAP" && !printOutput.fileName.Contains("DUPLICATE"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(@"C:\Users\bradm\source\repos\ElationAutoImport\ElationAutoImport\xmlFiles\savedNames.xml");
                    XmlNode addPatient = doc.CreateElement("patient");
                    addPatient.InnerText = (printOutput.patientName + printOutput.procedureDesc).Replace(" ","");
                    doc.DocumentElement.AppendChild(addPatient);
                    doc.Save(@"C:\Users\bradm\source\repos\ElationAutoImport\ElationAutoImport\xmlFiles\savedNames.xml");
                }
                if (fileList.Items.Count > 1)
                {
                    scannedFiles.RemoveAt(0);
                    fileList.Items.RemoveAt(0);
                }
                printOutput = scannedFiles[index].Item2;
                //Console.WriteLine("Size: " + scannedFiles.Count);
                //Console.WriteLine(printOutput.patientName);
                if (printOutput.docType == "Prescription")
                {
                    string fileName = System.IO.Path.GetFileName(scannedFiles[index].Item1);
                    mainWindow.fileText.Text = printOutput.docType + "_" + fileName;
                }
                else
                {
                    mainWindow.patientText.Text = printOutput.patientName.Replace(@"/", ".");
                    mainWindow.serviceText.Text = printOutput.appointmentDate.Replace(@"/", ".");
                    mainWindow.orderingText.Text = printOutput.reviewerName.Replace(@"/", ".");
                    mainWindow.documentText.Text = printOutput.docType.Replace(@"/", ".");
                    mainWindow.reviewingText.Text = printOutput.ccName.Replace(@"/", ".");
                    mainWindow.prcedureText.Text = printOutput.procedureDesc.Replace(@"/", ".");
                    mainWindow.fileText.Text = printOutput.fileName.Replace(@"/", ".");
                }
                mainWindow.currentFile = scannedFiles[0].Item2;
                mainWindow.currentFileIndex = index;
                mainWindow.patientFile.src = scannedFiles[index].Item1;
                mainWindow.patientFile.Show();
                return;
            }
            
            if(fileList.Items.Count == 1)
            {
                scannedFiles.RemoveAt(0);
                fileList.Items.RemoveAt(0);
                mainWindow.ResetDocument();
                mainWindow.patientFile.Hide();
            }

        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            int index = fileList.SelectedIndex;
            if(fileList.SelectedIndex < 0)
            {
                return;
            }
            Console.WriteLine(scannedFiles[index]);
            if (lb != null)
            {
                GeneralForm printOutput = scannedFiles[index].Item2;
                //Console.WriteLine("Size: " + scannedFiles.Count);
                //Console.WriteLine(printOutput.patientName);
                if (printOutput.docType == "Prescription")
                {
                    string fileName = System.IO.Path.GetFileName(scannedFiles[index].Item1);
                    mainWindow.fileText.Text = printOutput.docType + "_" + fileName;
                }
                else
                {
                    mainWindow.patientText.Text = printOutput.patientName.Replace(@"/", ".");
                    mainWindow.serviceText.Text = printOutput.appointmentDate.Replace(@"/", ".");
                    mainWindow.orderingText.Text = printOutput.reviewerName.Replace(@"/", ".");
                    mainWindow.documentText.Text = printOutput.docType.Replace(@"/", ".");
                    mainWindow.reviewingText.Text = printOutput.ccName.Replace(@"/", ".");
                    mainWindow.prcedureText.Text = printOutput.procedureDesc.Replace(@"/", ".");
                    mainWindow.fileText.Text = printOutput.fileName.Replace(@"/", ".");
                }
                mainWindow.currentFileIndex = index;
                mainWindow.patientFile.src = scannedFiles[index].Item1;
                mainWindow.patientFile.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void PatientFiles_Load(object sender, EventArgs e)
        {

        }

    }
}
