using System;

namespace ElationAutoImport
{
    class GlennKunimura : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public GlennKunimura(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("NAKAMURA"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("ARAKAKI"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("For"))
                {
                    docType = "Consult";
                    reviewerName = "Glenn Kunimura OD";
                    if (printWord.Contains("Mrs")) printWord = printWord.Replace("Mrs", "");
                    else if (printWord.Contains("Mr")) printWord = printWord.Replace("Mr", "");
                    else printWord = printWord.Replace("Ms", "");

                    string[] getInfo = printWord.Split(new string[] { "For" }, StringSplitOptions.None);
                    if (getInfo.Length > 1 && getInfo[1].Contains(","))
                    {
                        getInfo[1] = getInfo[1].TrimStart(";: ".ToCharArray());
                        string[] lastFirst = getInfo[1].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":;. ".ToCharArray());
                    }
                    else patientName = getInfo[1];
                    
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Exam Date"))
                {
                    /* Changed
                    string[] getInfo = printWord.Split(new string[] { "Exam Date" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) appointmentDate = getInfo[1].TrimStart(":; ".ToCharArray()); */
                    appointmentDate = printWord.Replace("Exam Date", "").TrimStart(":; ".ToCharArray());
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("CHIEF COMPLAINT"))
                {
                    /* Changed
                     * string[] getInfo = printWord.Split(new string[] { "CHIEF COMPLAINT" }, StringSplitOptions.None);
                    procedureDesc = getInfo[1];*/
                    procedureDesc = printWord.Replace("CHIEF COMPLAINT", "");
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}