using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class SmigelOffice : GeneralForm
    {
        const int GENERAL_NUM = 1;
        const int EMG_NUM = 2;
        public SmigelOffice(string[] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM)
                Traverse_General(textArray);
            else if (fileType == EMG_NUM)
                Traverse_EMG(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundName = false;
            docType = "Consult";
            reviewerName = "Liza Smigel MD";
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
                else if (patientName.Length == 0 && printWord.Contains("ENCOUNTER") || foundName)
                {
                    if (foundName)
                    {
                        int i = 0;
                        foreach(char x in printWord)
                        {
                            if(x == ' ')
                            {
                                i++;
                                if (i == 2) break;
                            }
                            patientName += x;
                        }
                    }
                    foundName = !foundName;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("DATE"))
                {
                    string[] findDate = printWord.Split(new string[] { "DATE" }, StringSplitOptions.None);
                    if (findDate.Length > 1)
                        appointmentDate = findDate[1];
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
        private void Traverse_EMG(string [] textArray)
        {
            docType = "Consult";
            procedureDesc = "EMG";
            reviewerName = "Liza Smigel MD";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                //Console.WriteLine(printWord);
                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("Patient"))
                {
                    patientName = printWord.TrimStart("Patient: ".ToCharArray());
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date of test"))
                {
                    appointmentDate = printWord.TrimStart("Date of test: ".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc+ "_" + reviewerName + "_" + ccName;
        }
    }
}
