using System;

namespace ElationAutoImport
{
    class QueensMedicalFile : GeneralForm
    {
        const int H_AND_P = 1;
        const int NORTH_HI = 2;
        const int RANDALL_NUM = 3;
        const int PULMONARY_NUM = 4;
        const int HEAD_NUM = 5;
        const int ENT_NUM = 6;
        const int SPINE_NUM = 7;
        const int INTERVENTIONAL_NUM = 8;
        public QueensMedicalFile(string[] textArray, int fileType)
        {
            if (fileType == H_AND_P) Traverse_HandP(textArray);
            else if (fileType == NORTH_HI) Traverse_NorthHI(textArray);
            else if (fileType == RANDALL_NUM) Travese_Randall(textArray);
            else if (fileType == PULMONARY_NUM) Traverse_Pulmonary(textArray);
            else if (fileType == HEAD_NUM) Traverse_Head(textArray);
            else if (fileType == ENT_NUM) Traverse_ENT(textArray);
            else if (fileType == SPINE_NUM) Traverse_Spine(textArray);
            else if (fileType == INTERVENTIONAL_NUM) Traverse_Interventional(textArray);

            printInfo();
        }


        private void Traverse_HandP(string[] textArray)
        {
            docType = "Hospital";
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
                    if (patName.Length > 1)
                    {
                        string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                }
                else if (reviewerName.Length == 0 && printWord.Contains("ATTENDING:"))
                {
                    string[] getDoc = printWord.Split(new string[] { "ATTENDING:" }, StringSplitOptions.None);
                    reviewerName += "H&P QMC - " + getDoc[1];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("DATE OF ADMISSION:"))
                {
                    string[] getDate = printWord.Split(new string[] { "DATE OF ADMISSION:" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1];
                }
            }

            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
        private void Traverse_NorthHI(string[] textArray)
        {
            docType = "Hospital";
            reviewerName = "ER Report QMC North Hawaii";
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
                    if (getDate.Length > 1) appointmentDate = getDate[1];
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
                    if(lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    foundName = false;
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void Travese_Randall(string[] textArray)
        {
            docType = "Consult";
            reviewerName = "Randall Blake MD";
            string docSpecialty = "Urology";

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
                if (patientName.Length == 0 && printWord.Contains("MRN")) //needs to be a if and not else if to make sure name can be captured.
                {
                    string[] patientArray = printWord.Split(new string[] { "MRN" }, StringSplitOptions.None);
                    string[] lastFirst = patientArray[0].Split(new string[] { "," }, StringSplitOptions.None);
                    if(lastFirst.Length > 1) patientName = lastFirst[1].Trim('(') + "," + lastFirst[0];
                }
                if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1].Trim(";:".ToCharArray());
                }

            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + docSpecialty + "_" + ccName;
        }
        private void Traverse_Pulmonary(string[] textArray)
        {
            docType = "Consult";
            reviewerName = "Pulmonary Critical Care Medicine";

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
                else if (patientName.Length == 0 && printWord.Contains("Name")) //needs to be a if and not else if to make sure name can be captured.
                {
                    printWord = printWord.TrimStart("Name".ToCharArray());
                    string[] patientArray = printWord.Split(new string[] { "MRN" }, StringSplitOptions.None);
                    patientName = patientArray[0];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1].Trim(";:".ToCharArray());
                }

                else if (procedureDesc.Length == 0 && printWord.Contains("Reason for Appointment"))
                {
                    string[] getDesc = printWord.Split(new string[] { "Reason for Appointment" }, StringSplitOptions.None);
                    procedureDesc = printWord.Trim("Reason for Appointment:; ".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
        private void Traverse_Head(string[] textArray)
        {
            docType = "Consult";
            string docSpecialty = "";
            bool foundDoc = false;
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
                if (patientName.Length == 0 && printWord.Contains("MRN")) //needs to be a if and not else if to make sure name can be captured.
                {
                    string[] patientArray = printWord.Split(new string[] { "MRN" }, StringSplitOptions.None);
                    string[] lastFirst = patientArray[0].Split(new string[] { "," }, StringSplitOptions.None);
                    if(lastFirst.Length > 1) patientName = lastFirst[1].Trim('(') + "," + lastFirst[0];
                }
                if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1].Trim(";:".ToCharArray());
                }

                else if (procedureDesc.Length == 0 && printWord.Contains("Reason for Appointment"))
                {
                    string[] getDesc = printWord.Split(new string[] { "Reason for Appointment" }, StringSplitOptions.None);
                    procedureDesc = printWord.Trim("Reason for Appointment:; ".ToCharArray());
                }
                else if (reviewerName.Length == 0 && printWord.Contains("MRN") || foundDoc)
                {
                    if (foundDoc)
                    {
                        if (printWord.Contains("Encounter Date"))
                        {
                            string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                            reviewerName = getDate[0];
                        }
                        else
                        {
                            reviewerName = printWord;
                        }
                    }
                    foundDoc = !foundDoc;
                }
                else if (docSpecialty.Length == 0 && printWord.Contains("Specialty"))
                {
                    string[] getSpec = printWord.Split(new string[] { "Specialty" }, StringSplitOptions.None);
                    if(getSpec.Length > 1) docSpecialty = getSpec[1].Trim(";:".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + docSpecialty + "_" + ccName;
        }
        private void Traverse_ENT(string[] textArray)
        {
            docType = "Consult";
            reviewerName = "Alfred Liu MD";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient"))
                {
                    patientName = printWord.TrimStart("Patient: ".ToCharArray());
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date of Visit"))
                {
                    appointmentDate = printWord.TrimStart("Date of Visit: ".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
        private void Traverse_Spine(string[] textArray)
        {
            docType = "Consult";
            reviewerName = "Lance Mitsunaga MD";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient"))
                {
                    patientName = printWord.TrimStart("Patient: ".ToCharArray());
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Date of Visit"))
                {
                    appointmentDate = printWord.TrimStart("Date of Visit: ".ToCharArray());
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
        private void Traverse_Interventional(string[] textArray)
        {
            reviewerName = "QMC Interventional Cardiology";
            docType = "Cardiac";
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
                else if (patientName.Length == 0 && printWord.Contains("Name"))
                {
                    string[] patientArray = printWord.Split(new string[] { "Name" }, StringSplitOptions.None);
                    if (patientArray.Length > 1)
                    {
                        patientArray[1] = patientArray[1].Trim(":; ".ToCharArray());
                        patientName = patientArray[1];
                    }
                    else if (appointmentDate.Length == 0 && printWord.Contains("Encounter Date"))
                    {
                        string[] getDate = printWord.Split(new string[] { "Encounter Date" }, StringSplitOptions.None);
                        if (getDate.Length > 1)
                        {
                            getDate[1] = getDate[1].TrimStart(":; ".ToCharArray());
                            appointmentDate = getDate[1];
                        }
                    }
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
        }
    }
}
