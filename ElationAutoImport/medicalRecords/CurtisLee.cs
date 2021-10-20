using System;


namespace ElationAutoImport
{
    class CurtisLee : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public CurtisLee(string[] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM) Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            docType = "Consult";
            reviewerName = "Curtis Lee MD";
            bool foundProc = false;
            bool foundDate = false;
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
                else if (patientName.Length == 0 && printWord.Contains("Patient Name"))
                {
                    //Changed
                    /*string[] patientArray = printWord.Split(new string[] { "Patient Name" }, StringSplitOptions.None);
                    patientArray[1] = patientArray[1].Trim(":; ".ToCharArray());*/
                    printWord = printWord.Replace("Patient Name", "");
                    printWord = printWord.TrimStart(":; ".ToCharArray());
                    int i = 0;
                    foreach (char x in printWord)
                    {
                        if (x == ' ') i++;
                        if (i == 2) break;
                        patientName += x;
                    }
                    patientName = patientName.TrimEnd(',');

                    if (patientName.Contains(","))
                    {
                        string[] lastFirst = patientName.Split(new string[] { "," }, StringSplitOptions.None);
                        if(lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Reason for Appointment") || foundProc)
                {
                    foundProc = true;
                    if (printWord.Contains("1."))
                    {
                        /* Changed
                        string[] getProc = printWord.Split(new string[] { "1." }, StringSplitOptions.None);
                        procedureDesc = getProc[1].TrimEnd(' ');*/
                        printWord = printWord.Replace("1.", "").Trim(' ');
                        foundProc = false;
                    } 
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Appointment Facility") || foundDate)
                {
                    printWord = printWord.TrimStart(' ');
                    if (foundDate && printWord.Length > 10)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            if (Char.IsUpper(printWord[i])) break;
                            appointmentDate += printWord[i];
                        }
                        foundDate = false;
                    } else
                    {
                        foundDate = true;
                    }
                }

            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}