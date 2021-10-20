using System;

namespace ElationAutoImport
{
    class VAClinic : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public VAClinic(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "VA Clinic";
            docType = "Consult";

            bool foundName = false;
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
                else if (patientName.Length == 0 && printWord.Contains("SPARK M. MATSUNAGA VAMC") && !printWord.Contains("CLIA")) 
                { 
                    string[] getInfo = printWord.Split(new string[] { "SPARK M. MATSUNAGA VAMC" }, StringSplitOptions.None);
                    if (getInfo[0].Contains(","))
                    {
                        string[] lastFirst = getInfo[0].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1)
                            patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else patientName = getInfo[0];
                    patientName = patientName.Replace(" ", "");
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("NOTE DATED"))
                {
                    printWord = printWord.Replace("NOTE DATED", "").TrimStart(":; ".ToCharArray());
                    if (printWord.Length > 10)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            appointmentDate += printWord[i];
                        }
                    }
                    else appointmentDate = printWord;
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
