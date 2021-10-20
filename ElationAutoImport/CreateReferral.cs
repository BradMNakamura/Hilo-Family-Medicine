using System;
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
    public partial class CreateReferral : UserControl
    {
        private string patientName;
        private List<PatientInfo> patientList;
        private struct PatientInfo
        {
            public string firstName;
            public string lastName;
            public string patientID;
            public string filename;
            public string docType;
            public string appointmentDate;
        }
        public CreateReferral()
        {
            InitializeComponent();
            patientName = "";
            patientList = new List<PatientInfo>();
        }

        private void searchPatient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchPatient_TextChanged(object sender, EventArgs e)
        {
            if (searchPatient.Text.Length == 0)
            {
                searchPatient.Items.Clear();
            }
            if (searchPatient.Text.Contains(" ") && searchPatient.Items.Count == 0)
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

        private void searchWorker_DoWork(object sender, DoWorkEventArgs e)
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
        }

        private void searchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate {
                foreach (dynamic patientName in searchPatient.Items)
                {
                    string[] getInfo = patientName.ToString().Split(new string[] { " DOB" }, StringSplitOptions.None);
                    if (searchPatient.Text.ToLower() == getInfo[0].ToLower())
                    {
                        SendKeys.Send(" ");
                        break;
                    }
                }
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var patientInfo = patientList[searchPatient.SelectedIndex];
            ElationAPI test = new ElationAPI();
            var patientRecord = test.ExactPatient(patientInfo.patientID);
            MessageBox.Show(patientRecord.ToString());
            referralText.Text = test.CreateReferral(patientRecord);
        }
    }
}
