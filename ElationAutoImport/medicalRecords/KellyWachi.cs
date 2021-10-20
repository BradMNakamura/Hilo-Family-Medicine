using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class KellyWachi : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public KellyWachi(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Kelly Wachi MD";
            docType = "Consutl";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient name"))
                {
                    printWord = printWord.TrimStart("Patient name".ToCharArray());
                    printWord = printWord.TrimStart(":; ".ToCharArray()); //Need this just incase it overlaps into patient name
                    if (printWord.Contains("Date of birth"))
                    {
                        string[] getPat = printWord.Split(new string[] { "Date of birth" }, StringSplitOptions.None);
                        getPat[0] = getPat[0].TrimStart(":; ".ToCharArray());
                        if (getPat[0].Contains(","))
                        {
                            string[] lastFirst = getPat[0].Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]);
                        }
                    }
                    else
                    {
                        patientName = printWord.TrimStart(":; ".ToCharArray());
                        if (printWord.Contains(","))
                        {
                            string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                            if (lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]);
                        }
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    printWord = printWord.Trim("Date:; ".ToCharArray());
                    if(printWord.Length == 10)
                    {
                        appointmentDate = printWord;
                    }
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
