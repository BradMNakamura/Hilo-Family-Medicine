using System;

namespace ElationAutoImport
{
    class HylaFax : GeneralForm
    {
        const int CHIKARA_NUM = 1;
        const int KEOLA_NUM = 2;
        const int HAWAII_PHYS_NUM = 3;
        public HylaFax(string [] textArray)
        {
            Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            bool foundName = false;
            docType = "Physical Therapy";
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
                else if(reviewerName.Length == 0 && printWord.Contains("Page 2") || foundName)
                {
                    if(foundName)
                    {
                        reviewerName = printWord;
                    }

                    foundName = !foundName;
                }
                else if (patientName.Length == 0 && printWord.Contains("Patient Name"))
                {
                    printWord = printWord.Replace("Patient Name", "").TrimStart(":; ".ToCharArray());
                    if (printWord.Contains("Date"))
                    {
                        string[] getInfo = printWord.Split(new string[] { "Date" }, StringSplitOptions.None);
                        if (getInfo[0].Contains(","))
                        {
                            string[] lastFirst = getInfo[0].Split(new string[] { "," }, StringSplitOptions.None);
                            if (lastFirst.Length > 1)
                                patientName = (lastFirst[1] + "," + lastFirst[0]).TrimStart(":; ".ToCharArray());
                        }
                        else patientName = getInfo[0];

                        bool getDate = false;
                        if (getInfo.Length > 1)
                        {
                            foreach (char x in getInfo[1])
                            {
                                if (Char.IsDigit(x)) getDate = true;
                                if (getDate) appointmentDate += x;
                            }
                        }
                    }
                    
                }
                //string[] getInfo = printWord.Split(new string[] { "at" }, StringSplitOptions.None);
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
