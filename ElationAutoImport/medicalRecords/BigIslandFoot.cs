using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class BigIslandFoot : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public BigIslandFoot(string [] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM)
                Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Brian Sugai DPM";
            docType = "Consult";
            string prevWord = "";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("Patient ID"))
                {
                    patientName = prevWord;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                    if (getDate.Length > 1)
                        appointmentDate = getDate[1].Trim(":; ".ToCharArray());
                    else appointmentDate = printWord.Replace("Encounter Date", "");
                }
                prevWord = printWord;
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
    }
}
