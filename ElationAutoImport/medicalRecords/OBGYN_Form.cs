using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class OBGYN_Form : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public OBGYN_Form(string [] textArray, int fileType)
        {
            if (fileType == GENERAL_NUM) Traverse_General(textArray);
        }

        private void Traverse_General(string[] textArray)
        {
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
            }
        }
    }
}
