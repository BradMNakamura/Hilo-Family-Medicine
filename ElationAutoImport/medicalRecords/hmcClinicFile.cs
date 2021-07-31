using System;

namespace ElationAutoImport.medicalRecords
{
    public class hmcClinicFile : GeneralForm
    {
        const int RADIATION_ONCOLOGY_NUM = 1;
        const int ONCOLOGY_NUM = 2;
        const int UROLOGY_NUM = 3;
        const int ENT_NUM = 4;
        const int SURGERY_NUM = 5;
        const string DOC_TYPE_CONSULT = "Consult";

        //NOTE: traverse_GeneralForm works for Urology, ENT, Surgery since they are the same file format besides clinic.
        public hmcClinicFile(string[] textArray, int fileType)
        {
            if (RADIATION_ONCOLOGY_NUM == fileType)
            {
                traverse_RadiationOncology(textArray);
            }
            else if (ONCOLOGY_NUM == fileType)
            {
                traverse_Radiation(textArray);
            }
            else if (UROLOGY_NUM == fileType)
            {
                reviewerName = "HMC Urology Clinic";
                traverse_GeneralForm(textArray);

            }
            else if (ENT_NUM == fileType)
            {
                reviewerName = "HMC ENT";
                traverse_GeneralForm(textArray);
            }
            else if (SURGERY_NUM == fileType)
            {
                reviewerName = "HMC General Surgery";
                traverse_GeneralForm(textArray);
            }

            printInfo();
        }

        private void traverse_RadiationOncology(string[] textArray)
        {
            reviewerName = "HMC Radiation Oncology";
            docType = DOC_TYPE_CONSULT;
            docLocation = "HMC";
            bool foundPatient = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (printWord.Contains("David Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                if (printWord.Contains("Melanie Nakamura"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                if (foundPatient == false && printWord.Contains("PATIENT:"))
                {
                    string[] patientArray = printWord.Split(new string[] { "PATIENT: " }, StringSplitOptions.None);
                    string[] lastFirst = patientArray[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                    foundPatient = true;
                    /*Console.WriteLine(lastFirst[1] + "," + lastFirst[0]);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                    patientName = patientName.TrimEnd(' ');*/
                }
                if (printWord.Contains("DATE OF SERVICE:"))
                {
                    string[] servDate = printWord.Split(new string[] { "DATE OF SERVICE: " }, StringSplitOptions.None);
                    appointmentDate = servDate[1];
                }

            }
            RenameFile();
        }

        private void traverse_Radiation(string[] textArray)
        {
            reviewerName = "HMC Oncology Clinic";
            docLocation = "HMC";
            docType = DOC_TYPE_CONSULT;
            bool foundCC = false;
            bool foundPatinet = false;
            bool foundVisit = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura, David"))
                {
                    ccName = DOCTOR_NAKAMURA;
                    foundCC = true;
                }
                if (ccName.Length == 0 && printWord.Contains("Arakaki, Melanie"))
                {
                    ccName = DOCTOR_ARAKAKI;
                    foundCC = true;
                }
                if (foundPatinet == false && printWord.Contains("Patient: "))
                {
                    Console.WriteLine(printWord);
                    string[] patName = printWord.Split(new string[] { "Patient: " }, StringSplitOptions.None);
                    string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                    foundPatinet = true;
                }
                if (foundVisit == false && printWord.Contains("Visit Date:"))
                {
                    string[] visitDate = printWord.Split(new string[] { "Visit Date: " }, StringSplitOptions.None);
                    appointmentDate = visitDate[1];
                    foundVisit = true;
                }
            }
            RenameFile();
        }

        private void traverse_GeneralForm(string[] textArray)
        {
            docType = DOC_TYPE_CONSULT;
            docLocation = "HMC";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                if (printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                if (printWord.Contains("Patient: "))
                {

                    string[] patName = printWord.Split(new string[] { "Patient: " }, StringSplitOptions.None);
                    string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                }
                if (printWord.Contains("Date/Time: "))
                {
                    string[] findDate = printWord.Split(new string[] { "Date/Time:" }, StringSplitOptions.None);
                    appointmentDate = findDate[1].Substring(0, findDate[1].Length - 5);
                }
                if (printWord.Contains("Visit Reasons:"))
                {
                    string[] captureProc = printWord.Split(new string[] { "Visit Reasons: " }, StringSplitOptions.None);
                    procedureDesc = captureProc[1];
                }
            }
            RenameFile();
        }

    }
}
