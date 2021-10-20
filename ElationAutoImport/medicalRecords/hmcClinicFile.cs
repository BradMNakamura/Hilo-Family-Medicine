using System;

namespace ElationAutoImport
{
    public class hmcClinicFile : GeneralForm
    {
        const int RADIATION_ONCOLOGY_NUM = 1;
        const int ONCOLOGY_NUM = 2;
        const int UROLOGY_NUM = 3;
        const int ENT_NUM = 4;
        const int SURGERY_NUM = 5;
        const int NEUROLOGY_NUM = 6;
        const int GASTROENTEROLOGY_NUM = 7;
        const int ECHO_NUM = 8;
        const int ORTHO_NUM = 9;
        const int MAX_DESC_LENGTH = 5;
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
            } else if (NEUROLOGY_NUM == fileType)
            {
                reviewerName = "HMC Neurology Service";
                Traverse_Neurology(textArray);
            } else if (GASTROENTEROLOGY_NUM == fileType)
            {
                reviewerName = "HMC Gastro";
                traverse_GeneralForm(textArray);
            } else if(ECHO_NUM == fileType) Traverse_Echo(textArray);
            else if (fileType == ORTHO_NUM)
            {
                reviewerName = "HMC Ortho";
                traverse_GeneralForm(textArray);
            }
            printInfo();
        }


        private void Traverse_Echo(string [] textArray)
        {
            int j = 0;
            bool foundName = false;
            docType = "Cardiac";
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                j++;
                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (reviewerName.Length == 0 && printWord.Contains("Ordering Physician"))
                {
                    reviewerName = "ECHO ";
                    string[] captureDoc = printWord.Split(new string[] { "Ordering Physician" }, StringSplitOptions.None);
                    if (captureDoc.Length > 1)
                    {
                        procedureDesc += captureDoc[1];
                        procedureDesc = procedureDesc.Trim("_; ".ToCharArray());
                        if (printWord.Contains("Exam Date"))
                        {
                            string[] findDate = printWord.Split(new string[] { "Exam Date" }, StringSplitOptions.None);
                            if (findDate.Length > 1)
                            {
                                string getDate = findDate[1].TrimEnd(' ');
                                getDate = getDate.Remove(0, 0);
                                for (int i = 0; i < getDate.Length; i++)
                                {
                                    if (i == 12)
                                        break;
                                    appointmentDate += getDate[i];
                                }
                            }
                        }
                        appointmentDate = appointmentDate.Trim("_; ".ToCharArray());
                    }
                }
                else if ((patientName.Length == 0 && printWord.Contains("Echo Report")) || foundName == true)
                {
                    foundName = true;
                    if (printWord.Contains(",") && foundName == true)
                    {
                        string[] lastFirst = printWord.Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                        foundName = false;
                    }
                } 

            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void Traverse_Neurology(string[] textArray)
        {
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                docType = DOC_TYPE_CONSULT;

                if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("Patient:"))
                {
                    string[] patientArray = printWord.Split(new string[] { "Patient: " }, StringSplitOptions.None);
                    string[] lastFirst = patientArray[1].Split(new string[] { "," }, StringSplitOptions.None);
                    if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date/Time: "))
                {
                    string[] findDate = printWord.Split(new string[] { "Date/Time:" }, StringSplitOptions.None);
                    if (findDate.Length > 1) appointmentDate = findDate[1].Substring(0, findDate[1].Length - 5);
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Visit Reasons:"))
                {
                    string[] captureProc = printWord.Split(new string[] { "Visit Reasons: " }, StringSplitOptions.None);
                    if(captureProc.Length > 1 ) procedureDesc = captureProc[1];
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void traverse_RadiationOncology(string[] textArray)
        {
            reviewerName = "HMC Radiation Oncology";
            docType = DOC_TYPE_CONSULT;

            bool foundDesc = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("David Nakamura"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Melanie Arakaki"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("PATIENT:"))
                {
                    string[] patientArray = printWord.Split(new string[] { "PATIENT: " }, StringSplitOptions.None);
                    if (patientArray.Length > 1)
                    {
                        string[] lastFirst = patientArray[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("DATE OF SERVICE:"))
                {
                    string[] servDate = printWord.Split(new string[] { "DATE OF SERVICE: " }, StringSplitOptions.None);
                    if(servDate.Length > 1 ) appointmentDate = servDate[1];
                }
                else if(procedureDesc.Length == 0 && printWord.Contains("DIAGNOSIS AND STAGING:") || foundDesc)
                {
                    if(foundDesc)
                    {
                        if (printWord.Contains("-"))
                        {
                            string[] procDesc = printWord.Split(new string[] { "-" }, StringSplitOptions.None);
                            int currCount = 0;
                            if (procDesc.Length > 1)
                            {
                                string currDesc = procDesc[1].TrimStart(' ');
                                for (int i = 0; i < currDesc.Length; i++)
                                {
                                    if (currCount == MAX_DESC_LENGTH)
                                        break;
                                    if (currDesc[i] == ' ')
                                        currCount++;
                                    procedureDesc += currDesc[i];
                                }
                            }                        }
                    }
                    foundDesc = !foundDesc;
                }

            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void traverse_Radiation(string[] textArray)
        {
            reviewerName = "HMC Oncology Clinic";
            docType = DOC_TYPE_CONSULT;
            bool foundCC = false;
            bool foundPatinet = false;
            bool foundVisit = false;
            bool foundDesc = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura, David"))
                {
                    ccName = DOCTOR_NAKAMURA;
                    foundCC = true;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki, Melanie"))
                {
                    ccName = DOCTOR_ARAKAKI;
                    foundCC = true;
                }
                else if (foundPatinet == false && printWord.Contains("Patient: "))
                {
                    string[] patName = printWord.Split(new string[] { "Patient: " }, StringSplitOptions.None);
                    if (patName.Length > 1)
                    {
                        string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                    foundPatinet = true;
                }
                else if (foundVisit == false && printWord.Contains("Visit Date:"))
                {
                    string[] visitDate = printWord.Split(new string[] { "Visit Date: " }, StringSplitOptions.None);
                    if(visitDate.Length > 1 ) appointmentDate = visitDate[1];
                    foundVisit = true;
                } 
                else if(procedureDesc.Length == 0 && printWord.Contains("Diagnosis") || foundDesc)
                {
                    if(foundDesc)
                    {
                        procedureDesc = printWord;
                        //foundDesc = false;
                    }
                    foundDesc = !foundDesc;
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName +  "_" + procedureDesc + "_"  + ccName;
        }
        private void traverse_GeneralForm(string[] textArray)
        {
            docType = DOC_TYPE_CONSULT;
            docLocation = "HMC";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient: "))
                {

                    string[] patName = printWord.Split(new string[] { "Patient: " }, StringSplitOptions.None);
                    if (patName.Length > 1)
                    {
                        string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                        else patientName = patName[1];
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date/Time: "))
                {
                    string[] findDate = printWord.Split(new string[] { "Date/Time:" }, StringSplitOptions.None);
                    if(findDate.Length > 1) appointmentDate = findDate[1].Substring(0, findDate[1].Length - 5);
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("Visit Reasons:"))
                {
                    string[] captureProc = printWord.Split(new string[] { "Visit Reasons: " }, StringSplitOptions.None);
                    if(captureProc.Length > 1) procedureDesc = captureProc[1];
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void Traverse_Ortho(string [] textArray)
        {

        }

    }
}
