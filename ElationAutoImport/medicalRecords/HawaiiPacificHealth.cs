using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class HawaiiPacificHealth : GeneralForm
    {
        private const int GENERAL_NUM = 1;
        private const int MAMMO_NUM = 2;
        public HawaiiPacificHealth(string [] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM) Traverse_General(textArray);
            else if (fileType == MAMMO_NUM) Traverse_Mammo(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundApp = false;
            bool foundName = false;
            docType = "Consult";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                //Console.WriteLine(printWord.Length + " : " + printWord);
                // Console.WriteLine(printWord);
                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("DOB") || foundName)
                {
                    if (foundName && printWord.Contains(","))
                    {
                        string temp = "";
                        foreach(char x in printWord)
                        {
                            if (Char.IsDigit(x)) break;
                            temp += x;
                        }
                        if (temp.Length != 0) printWord = temp;
                        if (printWord.Contains(","))
                        {
                            string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = (lastFirst[1].Trim('(') + "," + lastFirst[0]).Trim(' ');
                        }
                    }
                    foundName = !foundName;
                }
                else if (reviewerName.Length == 0 && printWord.Contains("Notes") || foundApp)
                {
                    if (foundApp)
                    {
                        string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
                        if (getInfo.Length > 1)
                        {
                            getInfo[1] = getInfo[1].Trim(' ');
                            if (getInfo[1].Length > 10)
                            {
                                foreach (char x in getInfo[1])
                                {
                                    if (x == ' ') break;
                                    appointmentDate += x;
                                }
                            }
                            reviewerName = getInfo[0].TrimEnd(' ');
                        }
                    }
                    foundApp = !foundApp;
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

        private void Traverse_Mammo(string [] textArray)
        {

        }
    }
}
