using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{

    class HawaiiVisionSpecialist : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HawaiiVisionSpecialist(string[] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM) Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundDate = false;
            docType = "Consult";
            reviewerName = "Hawaii Vision Specialists";
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
                else if (appointmentDate.Length == 0 && printWord.Contains("hawaiivisionspecialists") || foundDate)
                {
                    foundDate = true;
                    int i = 0;
                    foreach(char x in printWord)
                    {
                        if (x == '/') i++;
                    }
                    if (i == 2) { appointmentDate = printWord; foundDate = false; }
                }
                else if (patientName.Length == 0 && printWord.Contains("RE"))
                {
                    /*string[] findPat = printWord.Split(new string[] { "RE" }, StringSplitOptions.None);
                    printWord = findPat[1];
                    printWord = printWord.TrimStart(" :;".ToCharArray());*/
                    printWord = printWord.Replace("RE", "").TrimStart(":; ".ToCharArray());
                    int i = 0;
                    foreach (char x in printWord)
                    {
                        if (x == ' ' || x == ',')
                        {
                            i++;
                            if (i == 2) break;
                        }
                        patientName += x;
                    } 
                }
                else if (reviewerName == "Hawaii Vision Specialists" && printWord.Contains("upcoming surgery"))
                {
                    reviewerName += " needs pre op clearance";
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;

        }
      
    }
}
