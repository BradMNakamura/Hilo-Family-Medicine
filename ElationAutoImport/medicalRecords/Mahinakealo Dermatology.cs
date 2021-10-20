using System;

namespace ElationAutoImport
{
    class MahinakealoDermatology : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public MahinakealoDermatology(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Mahinakealo Dermatology";
            docType = "Consult";
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
                else if (patientName.Length == 0 && printWord.Contains("referring patient"))
                {
                    string[] getInfo = printWord.Split(new string[] { "referring patient" }, StringSplitOptions.None);
                    if(getInfo.Length > 1 && getInfo[1].Contains("to us"))
                    {
                        string[] temp = getInfo[1].Split(new string[] { "to us" }, StringSplitOptions.None);
                        getInfo[1] = temp[0];
                    }
                    getInfo[1] = getInfo[1].Trim(". ".ToCharArray());
                    if (getInfo.Length > 1 && getInfo[1].Contains(","))
                    {
                        string[] lastFirst = getInfo[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if(lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]).Trim(' ');
                    }
                    else patientName = getInfo[1];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Visit Note"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Visit Note" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].Trim(" -".ToCharArray());
                        int i = 0;
                        foreach (char x in getInfo[1])
                        {
                            if (Char.IsDigit(x)) i++;
                            if (i >= 5)
                            {
                                if (char.IsLetter(x)) break;
                            }
                            appointmentDate += x;
                        }
                    }
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Chief Complaint"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Chief Complaint" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) procedureDesc = getInfo[1].TrimStart(";: ".ToCharArray());
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}
