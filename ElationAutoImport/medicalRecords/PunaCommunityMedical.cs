using System;

namespace ElationAutoImport
{
    class PunaCommunityMedical : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public PunaCommunityMedical(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Puna Urgent Care";
            docType = "Urgent";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient"))
                {
                    printWord = printWord.Replace("Patient", "");
                    printWord = printWord.TrimStart(":; ".ToCharArray());
                    if (printWord.Contains(","))
                    {
                        string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                        if(lastFirst.Length > 1 ) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                    /*if (getInfo[].Contains(","))
                    {
                        string[] lastFirst = getInfo[].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else
                    {
                        patientName = getInfo[0];
                    }*/
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date/Time"))
                {
                    printWord = printWord.Trim("Date/Time: ".ToCharArray());
                    foreach (char x in printWord)
                    {
                        if (x == ' ') break;
                        appointmentDate += x;
                    }
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Visit Reasons"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Visit Reasons" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].TrimStart(":; ".ToCharArray());
                        procedureDesc = getInfo[1];
                    }
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}
