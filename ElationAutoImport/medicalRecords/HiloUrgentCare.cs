using System;

namespace ElationAutoImport
{
    class HiloUrgentCare : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HiloUrgentCare(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            docType = "Urgent";
            reviewerName = "Hilo Urgent Care";
            bool foundDate = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
               // Console.WriteLine(printWord);
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
                    printWord = printWord.Replace("prohibited", "").TrimStart(":;. ".ToCharArray());
                    printWord = printWord.Replace("Patient Name", "").TrimStart(":; ".ToCharArray());
                    //Console.WriteLine(printWord);
                    if (printWord.Contains("DOB"))
                    {
                        string[] getInfo = printWord.Split(new string[] { "DOB" }, StringSplitOptions.None);
                        if (getInfo[0].Contains(","))
                        {
                            string[] lastFirst = getInfo[0].Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                        }
                        else patientName = getInfo[0];
                    }
                    else patientName = printWord;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date"))
                {
                    string[] getInfo = printWord.Split(new string[] { "Date" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].Trim(":; ".ToCharArray());
                        if (getInfo[1].Length == 10)
                        {
                            foreach (char x in getInfo[1])
                            {
                                if (char.IsLetter(x)) break;
                            }
                            appointmentDate = getInfo[1];
                        }
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Progress Note"))
                {
                    bool copyDate = false;
                    printWord = printWord.TrimEnd(' ');
                    foreach(char x in printWord)
                    {
                        if (Char.IsDigit(x)) copyDate = true;
                        if (copyDate) appointmentDate += x;
                        if (appointmentDate.Length == 10) break;
                    }
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("1."))
                {
                    string[] getInfo = printWord.Split(new string[] { "1." }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        getInfo[1] = getInfo[1].TrimStart(' ');
                        int i = 0;
                        foreach (char x in getInfo[1])
                        {
                            if (x == '.') break;
                            procedureDesc += x;
                        }
                    }
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}
