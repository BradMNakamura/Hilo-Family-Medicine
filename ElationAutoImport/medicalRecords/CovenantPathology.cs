using System;
namespace ElationAutoImport
{
    class CovenantPathology : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public CovenantPathology(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            docType = "Quality";
            docLocation = "Covenant Pathology Services";
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
                else if (patientName.Length == 0 && printWord.Contains("Name"))
                {
                    printWord = printWord.Replace("Name", "").TrimStart(":; ".ToCharArray());
                    if (printWord.Contains("Access"))
                    {
                        string[] getInfo = printWord.Split(new string[] { "Access" }, StringSplitOptions.None);
                        if (getInfo[0].Contains(","))
                        {
                            string[] lastFirst = getInfo[0].Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                        }
                        else patientName = getInfo[0];
                    }
                    
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Collected"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Collected" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].TrimStart(":; ".ToCharArray());
                        foreach (char x in getInfo[1])
                        {
                            if (x == ' ') break;
                            appointmentDate += x;
                        }
                    }
                }
                else if (reviewerName.Length == 0 && printWord.Contains("Submitted by"))
                {
                    
                    string[] getInfo = printWord.Split(new string[] { "Submitted by" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].TrimStart(":; ".ToCharArray());
                        reviewerName = getInfo[1];
                    }
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + docLocation + "_" + reviewerName + "_" + ccName;
        }

    }
}
