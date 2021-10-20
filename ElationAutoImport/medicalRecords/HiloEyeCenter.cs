using System;

namespace ElationAutoImport
{
    class HiloEyeCenter : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HiloEyeCenter(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);

            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Hilo Eye Center";
            docType = "Consult";
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
                else if (patientName.Length == 0 && printWord.Contains("patient"))
                {
                    string[] getInfo = printWord.Split(new string[] { "patient" }, StringSplitOptions.None);
                    if (getInfo.Length > 1)
                    {
                        foreach (char x in getInfo[1])
                        {
                            if (x == '(') break;
                            patientName += x;
                        }
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("findings on"))
                {
                    string[] getInfo = printWord.Split(new string[] { "findings on" }, StringSplitOptions.None);
                    if(getInfo.Length > 1) appointmentDate = getInfo[1].Trim(". ".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

    }
}
