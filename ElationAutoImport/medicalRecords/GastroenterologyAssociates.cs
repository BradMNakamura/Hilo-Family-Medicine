using System;

namespace ElationAutoImport
{
    class GastroenterologyAssociates : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public GastroenterologyAssociates(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            procedureDesc = "Colonoscopy";
            int procLength = procedureDesc.Length;
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
                    string[] getInfo = printWord.Split(new string[] { "Patient Name" }, StringSplitOptions.None);
                    if (getInfo.Length > 1 && getInfo[1].Contains(","))
                    {
                        string[] lastFirst = getInfo[1].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else patientName = getInfo[1];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Date" }, StringSplitOptions.None);
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
                else if (reviewerName.Length == 0 && printWord.Contains("Provider"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Provider" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) reviewerName = getInfo[1].TrimStart(":; ".ToCharArray());
                }
                else if (procedureDesc.Length == procLength && printWord.Contains("Refer to TEC for procedure"))
                {
                    procedureDesc = "Pre Op Colonoscopy";
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc + "_" + reviewerName + "_" + ccName;
        }

    }
}
