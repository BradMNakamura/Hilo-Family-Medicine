using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class OrthopedicAssociates : GeneralForm
    {
        const int GENERAL_NUM = 1;

        public OrthopedicAssociates (string [] textArray, int fileType) 
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }
        

        private void Traverse_General(string [] textArray)
        {
            reviewerName = "Orthopedic Associates";
            docType = "Consult";
            string prevWord;
            bool foundDate = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                //Console.WriteLine(printWord);
                if (ccName.Length == 0 && printWord.Contains("NA"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("HILO CLINIC") || foundDate)
                {
                    if (foundDate)
                        appointmentDate = printWord;
                    foundDate = !foundDate;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    string[] patientArray = printWord.Split(new string[] { "Date" }, StringSplitOptions.None);
                    if(patientArray.Length > 1) appointmentDate = patientArray[1].TrimStart(' ');
                }
                else if (patientName.Length == 0 && printWord.Contains("PATIENT NAME"))
                {
                    string[] patientArray = printWord.Split(new string[] { "PATIENT NAME" }, StringSplitOptions.None);
                    if (patientArray.Length > 1 && patientArray[1].Contains(","))
                    {
                        string[] lastFirst = patientArray[1].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else
                    {
                        patientName = patientArray[1];
                    }
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("FOLLOW UP"))
                {
                    procedureDesc = "Follow Up";
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("DIAGNOSIS"))
                {
                    string[] getDate = printWord.Split(new string[] { "DIAGNOSIS" }, StringSplitOptions.None);
                    if(getDate.Length > 1) getDate[1] = getDate[1].Trim(":; ".ToCharArray());
                }
                prevWord = printWord;
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
    }
}
