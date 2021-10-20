using System;

namespace ElationAutoImport
{
    class DeptOBGYN : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public DeptOBGYN(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundProc = false;
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
                else if (patientName.Length == 0 && printWord.Contains("MRN") && !printWord.Contains("("))
                {
                    string[] patientArray = printWord.Split(new string[] { "MRN" }, StringSplitOptions.None);
                    if (patientArray.Length > 1)
                    {
                        string[] lastFirst = patientArray[0].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = lastFirst[1].Trim('(') + "," + lastFirst[0];
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1].Trim(";:".ToCharArray());
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Patient presents with") || foundProc)
                {
                    if (foundProc) procedureDesc = printWord;
                    foundProc = !foundProc;
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }

    }
}