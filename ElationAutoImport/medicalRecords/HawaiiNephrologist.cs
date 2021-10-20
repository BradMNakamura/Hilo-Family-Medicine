using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class HawaiiNephrologist : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HawaiiNephrologist(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Hawaii Nephrologists";
            docType = "Consult";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient Name"))
                {
                    /*string[] patientArray = printWord.Split(new string[] { "Patient Name" }, StringSplitOptions.None);
                    patientArray[1] = patientArray[1].Trim(":; ".ToCharArray());
                    patientName = patientArray[1];*/
                    patientName = printWord.Replace("Patient Name", "").TrimStart(":; ".ToCharArray());
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Date" }, StringSplitOptions.None);
                    if (getDate.Length > 1)
                    {
                        getDate[1] = getDate[1].TrimStart(":; ".ToCharArray());
                        if (!getDate[1].Contains(":") && !getDate[1].Contains(";"))
                        {
                            appointmentDate = getDate[1];
                        }
                    }

                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
