using System;

namespace ElationAutoImport
{
    class OahuPainCare : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public OahuPainCare(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            docType = "Consult";
            string prevWord = "";
            bool foundName = false;
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
                else if (reviewerName.Length == 0 && printWord.Contains("SEEN BY"))
                {
                    string[] getInfo = printWord.Split(new string[] { "SEEN BY" }, StringSplitOptions.None);
                    reviewerName = getInfo[1];
                }
                else if (patientName.Length == 0 && printWord.Contains("ENCOUNTER") || foundName)  
                {
                    if(foundName)
                    {
                        int i = 0;
                        printWord = printWord.TrimStart(' ');
                        foreach(char x in printWord)
                        {
                            if (x == ' ') i++;
                            if (i == 2) break;
                            patientName += x;
                        }
                    }
                    foundName = !foundName;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("DATE"))
                {
                    string[] getInfo = printWord.Split(new string[] { "DATE" }, StringSplitOptions.None);
                    if(getInfo.Length > 1 ) appointmentDate = getInfo[1];
                }

                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
