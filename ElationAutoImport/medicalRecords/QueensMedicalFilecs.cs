using System;

namespace ElationAutoImport.medicalRecords
{
    class QueensMedicalFile : GeneralForm
    {
        const int H_AND_P = 1;
        const int NORTH_HI = 2;
        public QueensMedicalFile(string[] textArray, int fileType)
        {
            if (fileType == H_AND_P)
            {
                Traverse_HandP(textArray);
            }
            else if (fileType == NORTH_HI)
            {
                Traverse_NorthHI(textArray);
            }

            printInfo();
        }

        private void Traverse_HandP(string[] textArray)
        {
            docType = "hospital";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura, David"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki, Melanie"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (printWord.Contains("PATIENT NAME:"))
                {
                    string[] patName = printWord.Split(new string[] { "PATIENT NAME:" }, StringSplitOptions.None);
                    string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                }
                else if (reviewerName.Length == 0 && printWord.Contains("ATTENDING:"))
                {
                    string[] getDoc = printWord.Split(new string[] { "ATTENDING:" }, StringSplitOptions.None);
                    reviewerName += "H&P QMC - " + getDoc[1];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("DATE OF ADMISSION:"))
                {
                    string[] getDate = printWord.Split(new string[] { "DATE OF ADMISSION:" }, StringSplitOptions.None);
                    appointmentDate = getDate[1];
                }
            }

            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }

        private void Traverse_NorthHI(string[] textArray)
        {
            docType = "hospital";
            reviewerName = "ER Report North HI QMC";
            bool foundName = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura, David"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki, Melanie"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("ED Date of Service:"))
                {
                    string[] getDate = printWord.Split(new string[] { "ED Date of Service:" }, StringSplitOptions.None);
                    appointmentDate = getDate[1];
                }
                else if (patientName.Length == 0 && printWord.Contains("Name Patient ID"))
                {
                    foundName = true;
                    continue;
                }
                else if (foundName == true)
                {
                    string tempName = "";
                    for (int i = 0; i < printWord.Length; i++)
                    {
                        if (!Char.IsDigit(printWord[i]) || printWord[i] == ' ' || printWord[i] == ',')
                        {
                            tempName += printWord[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    string[] lastFirst = tempName.Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                    foundName = false;
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
    }
}
