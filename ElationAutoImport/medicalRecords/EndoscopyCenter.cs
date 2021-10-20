using System;
using System;

namespace ElationAutoImport
{
    class EndoscopyCenter : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public EndoscopyCenter(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            procedureDesc = "Colonscopy Report";
            docType = "Quality";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient Name"))
                {
                    printWord = printWord.Replace("Patient Name", "").TrimStart(";: ".ToCharArray());
                    string[] getInfo = printWord.Split(new string[] { "Gender" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) patientName = getInfo[0].Trim(";: ".ToCharArray());
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    printWord = printWord.Replace("Date", "");
                    printWord = printWord.TrimStart(";: ".ToCharArray());
                    foreach (char x in printWord)
                    {
                        if (x == ' ') break;
                        appointmentDate += x; 
                    }
                }
                else if (reviewerName.Length == 0 && printWord.Contains("Endoscopist(s)"))
                {
                    printWord = printWord.Replace("Endoscopist(s)", "").TrimStart(";: ".ToCharArray());
                    string[] getInfo = printWord.Split(new string[] { "Instrument" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) reviewerName = getInfo[0].Trim(";: ".ToCharArray());
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc + "_" + reviewerName + "_" + ccName;
        }

    }
}
