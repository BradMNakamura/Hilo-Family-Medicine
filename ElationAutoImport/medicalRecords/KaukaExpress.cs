using System;

namespace ElationAutoImport
{
    class KaukaExpress : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public KaukaExpress(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            docType = "Urgent";
            reviewerName = "Kauka Express Urgent Care";
            bool foundProc = false;
            int procCount = 0;
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
                else if (patientName.Length == 0 && printWord.Contains("Visit Note"))
                {
                    printWord = printWord.Replace("Re", "");
                    printWord = printWord.TrimStart(": ".ToCharArray());
                    string[] getInfo = printWord.Split(new string[] { "Visit Note" }, StringSplitOptions.None);
                    getInfo[0] = getInfo[0].TrimEnd("- ".ToCharArray());
                    if (getInfo[0].Contains(","))
                    {
                        string[] lastFirst = getInfo[0].Split(new string[] { "," }, StringSplitOptions.None);
                        if(lastFirst.Length > 1) patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else patientName = getInfo[0];

                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Visit Date"))
                {
                    bool getDate = false;
                    for(int i = 0; i < printWord.Length-2; i++)
                    {
                        if(getDate)
                        {
                            if (printWord[i + 2] == ':') break;
                            else appointmentDate += printWord[i];
                        }
                        if (printWord[i] == ',') getDate = true;
                    }
                    appointmentDate = appointmentDate.Trim(' ');
                }
                else if(procedureDesc.Length == 0 && printWord.Contains("SUBJECTIVE") || foundProc)
                {
                    foundProc = true;
                    procCount++;
                    if (procCount == 3)
                    {
                        bool getProc = false;
                        string[] getInfo = printWord.Split(new string[] { "." }, StringSplitOptions.None);

                        if (getInfo.Length > 2)
                        {
                            foreach (char x in getInfo[2])
                            {
                                if (x == '.') break;
                                procedureDesc += x;
                            }
                        }
                    }
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}
