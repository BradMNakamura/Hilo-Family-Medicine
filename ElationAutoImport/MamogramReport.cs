using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
//using IronOcr;

namespace ElationAutoImport
{
    public partial class MamogramReport : UserControl
    {
        string actualPath = "";
        private string patientName;
        private bool doneSearching;
        private List<PatientInfo> patientList;
        private List<string> fileNames;
        private Dictionary<string, string> reviewerDoctors;
        private Dictionary<string, int> docTypes;
        private int sourceIndex; //Current index for adobe file.
        private struct PatientInfo
        {
            public string firstName;
            public string lastName;
            public string patientID;
            public string filename;
            public string docType;
            public string appointmentDate;
        }
        public MamogramReport()
        {
            InitializeComponent();
            patientName = "";
            doneSearching = false;
            patientList = new List<PatientInfo>();
            fileNames = new List<string>();
            reviewerDoctors = new Dictionary<string, string>();
            docTypes = new Dictionary<string, int>();

            reviewerDoctors.Add("nak", "David Nakamura, MD");
            reviewerDoctors.Add("arak", "Melanie Arakaki, MD");

            docTypes.Add("Cardiac", 0);
            docTypes.Add("Consult", 1);
            docTypes.Add("Hospital", 2);
            docTypes.Add("Imaging", 3);
            docTypes.Add("Laboratory", 4);
            docTypes.Add("Legal", 5);
            //docTypes.Add("Medical");
            docTypes.Add("Misc", 7);

            sourceIndex = -1;
        }


        private void button1_Click(object sender, EventArgs e) //Folder Path
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                actualPath = FBD.SelectedPath;
                string[] fileNames = Directory.GetFiles(FBD.SelectedPath);
                //listBox1.Items.Clear();
                foreach (string patientFile in fileNames)
                {
                    // listBox1.Items.Add(System.IO.Path.GetFileName(patientFile));
                }
            }
        }




        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (searchPatient.Text.Length == 0)
            {
                searchPatient.Items.Clear();
                doneSearching = false;
            }
            if(searchPatient.Text.Contains(" ") && searchPatient.Items.Count == 0)
            {
                string[] getInfo = searchPatient.Text.Split(new string[] { " " }, StringSplitOptions.None);
                patientName = getInfo[0];
                if (!searchWorker.IsBusy)
                {
                    //searchPatient.Items.Clear();
                    searchPatient.SelectionLength = 0;
                    searchWorker.RunWorkerAsync();
                }
            }
        }

        private void searchWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ElationAPI test = new ElationAPI();
            var foundPatients = test.FindPatient(patientName);
            foreach (dynamic patientFile in foundPatients["results"])
            {
                this.Invoke(new MethodInvoker(delegate { searchPatient.Items.Add(patientFile["first_name"] + " " + patientFile["last_name"] + " DOB: " + patientFile["dob"]); searchPatient.SelectionLength = 0; }));
                PatientInfo addPatient = new PatientInfo();
                string fullName = patientFile["first_name"] + " " + patientFile["last_name"];
                fullName = fullName.ToLower();
                addPatient.firstName = patientFile["first_name"];
                addPatient.lastName = patientFile["last_name"];
                addPatient.patientID = patientFile["id"];
                patientList.Add(addPatient);
            }
            //MessageBox.Show(test.FindPatient(patientName).ToString());
        }

        private void searchWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate {
                foreach (dynamic patientName in searchPatient.Items)
                {
                    string[] getInfo = patientName.ToString().Split(new string[] { "DOB" }, StringSplitOptions.None);
                    if (searchPatient.Text.ToLower() == getInfo[0].ToLower())
                    {
                        SendKeys.Send(" ");
                        break;
                    }
                }
            }));
            //this.Invoke(new MethodInvoker(delegate { searchPatient.SelectionLength = 0; }));
            doneSearching = true;
        }


        private void openFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                actualPath = FBD.SelectedPath;
                string[] tempNames = Directory.GetFiles(FBD.SelectedPath);
                fileNames.Clear();
                foreach (string fileName in tempNames)
                {
                    fileNames.Add(fileName);
                }
                patientFile.src = tempNames[0];
                sourceIndex = 0;
                FillReport();
            }
        }

        private void prevButon_Click(object sender, EventArgs e)
        {
            if (sourceIndex == -1) return; //No source chosen.
            if (sourceIndex > 0)
            {
                sourceIndex--;
                patientFile.src = fileNames[sourceIndex];
                FillReport();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (sourceIndex == -1) return;
            if (sourceIndex < fileNames.Count - 1)
            {
                sourceIndex++;
                patientFile.src = fileNames[sourceIndex];
                FillReport();
            }
        }

        private void FillReport()
        {
            if (sourceIndex == -1) return;
            fileText.Text = Path.GetFileNameWithoutExtension(fileNames[sourceIndex]);
            reviewerText.Text = "";
            //MessageBox.Show(Path.GetFileNameWithoutExtension(fileNames[sourceIndex]));
            string tempFile = Path.GetFileNameWithoutExtension(fileNames[sourceIndex]);
            string[] getInfo = tempFile.Split(new string[] { "_" }, StringSplitOptions.None);
            if (getInfo.Length > 3)
            {
                getInfo[1] = getInfo[1].Replace(".", "/");
                dateText.Text = getInfo[1];
                if (docTypes.ContainsKey(getInfo[2]))
                {
                    docBox.SelectedIndex = docTypes[getInfo[2]];
                }
                if (reviewerDoctors.ContainsKey(getInfo[getInfo.Length - 1])) ;
                {
                    reviewerText.Text = reviewerDoctors[getInfo[getInfo.Length - 1]];
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(searchPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient");
                return;
            }
            if(sourceIndex == -1)
            {
                MessageBox.Show("Please select a file");
                return;
            }
            ElationAPI test = new ElationAPI();
            //test.UploadReport(fileNames[sourceIndex], null, )
            Dictionary<string, string> patInfo = new Dictionary<string, string>();
            //Need appointment date
            //Need doctype
            //need filename
            string[] getInfo = fileNames[sourceIndex].Split(new string[] { "_" }, StringSplitOptions.None);
            foreach(string findType in getInfo)
            {
                if(docTypes.ContainsKey(findType))
                {
                    patInfo.Add("doc_type", findType);
                }
            }
            patInfo.Add("date", getInfo[1].Replace(" ", ""));
            patInfo.Add("filename", fileNames[sourceIndex]);

            var patFile = test.ExactPatient(patientList[searchPatient.SelectedIndex].patientID);
            MessageBox.Show(test.UploadReport(patInfo, patFile));

        }
    }
}