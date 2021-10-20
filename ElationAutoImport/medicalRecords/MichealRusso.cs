using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class MichealRusso : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public MichealRusso(string[] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM) Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string [] textArray)
        {
            docType = "Consult";
            reviewerName = "Michael Russo MD";
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
                else if (patientName.Length == 0 && printWord.Contains(","))
                {
                    bool foundName = true;
                    if(printWord.Contains("DOB"))
                    {
                        string[] patientArray = printWord.Split(new string[] { "DOB" }, StringSplitOptions.None);
                        printWord = patientArray[0];
                    }
                    foreach (char x in printWord) 
                    {
                        if(char.IsDigit(x))
                        {
                            foundName = false;
                            break;
                        }
                    }
                    if (foundName)
                    {
                        string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                        if(lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }

                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Visit Date"))
                {
                    string[] servDate = printWord.Split(new string[] { "Visit Date" }, StringSplitOptions.None);
                    if (servDate.Length > 1)
                    {
                        servDate[1] = servDate[1].TrimStart(":; ".ToCharArray());
                        appointmentDate = servDate[1];
                    }
                }
                if (ccName.Length != 0 && patientName.Length != 0 && appointmentDate.Length != 0) break;
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
    }
}
