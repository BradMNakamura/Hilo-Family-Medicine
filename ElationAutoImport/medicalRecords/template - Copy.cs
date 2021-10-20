using System;

namespace ElationAutoImport
{
    class Default : GeneralForm
    {
        const int GENERAL_NUM = 1;
        /*public (string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }*/

        private void Traverse_General(string[] textArray)
        {
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                Console.WriteLine(printWord);
                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains(""))
                {
                    /*if (getInfo[].Contains(","))
                    {
                        string[] lastFirst = getInfo[].Split(new string[] { "," }, StringSplitOptions.None);
                        patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                    }
                    else patientName = getInfo[0];
                    */
                }
                else if (appointmentDate.Length == 0 && printWord.Contains(""))
                {

                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}