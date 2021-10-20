using System;
using System;

namespace ElationAutoImport
{
    class HMSA : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HMSA(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundName = false;
            docType = "homeHealth";
            reviewerName = "HMSA Hospital Discharge Plan of Care";
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
                else if (patientName.Length == 0 && printWord.Contains("First Name") || foundName) 
                {
                    if (foundName)
                    {
                        printWord = printWord.Trim(' ');
                        foreach(char x in printWord)
                        {
                            if (Char.IsDigit(x)) break;
                            patientName += x;
                        }
                    }
                    foundName = !foundName;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    printWord = printWord.Replace("Date", "").TrimStart(":; ".ToCharArray());
                    appointmentDate = printWord;
                }

                if (ccName.Length != 0 && patientName.Length != 0 && appointmentDate.Length != 0) break;
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
