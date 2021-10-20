using System;

namespace ElationAutoImport
{
    class EyeCareHawaii : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public EyeCareHawaii(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Eye Care Hawaii";
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
                else if (patientName.Length == 0 && printWord.Contains("RE"))
                {
                    printWord = printWord.TrimStart("RE".ToCharArray());
                    printWord = printWord.TrimStart(":; ".ToCharArray());
                    if (printWord.Contains("DOB"))
                    {
                        string[] getInfo = printWord.Split(new string[] { "DOB" }, StringSplitOptions.None);
                        printWord = getInfo[0];
                    }
                    if (printWord.Contains(","))
                    {
                        string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]).Trim(' ');
                    }
                    else patientName = printWord;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("findings from"))
                {
                    string[] getInfo = printWord.Split(new string[] { "findings from" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        if (getInfo[1].Length > 10)
                        {
                            string temp = getInfo[1];
                            for (int i = 0; i < 10; i++)
                            {
                                appointmentDate += temp[i];
                            }
                        }
                        else appointmentDate = getInfo[1];
                    }
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
