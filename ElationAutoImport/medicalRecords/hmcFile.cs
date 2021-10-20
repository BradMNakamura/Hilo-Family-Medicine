using System;
using System.Linq;

namespace ElationAutoImport
{
    public class hmcFile : GeneralForm
    {
        const int XRAY_NUM = 1;
        const int ER_NUM = 2;
        public hmcFile(string[] textArray, int fileType)
        {
            if (fileType == XRAY_NUM)
            {
                TraverseXray(textArray);
            }
            else if (fileType == ER_NUM)
            {
                TraverseER(textArray);
            }

            printInfo();
        }

        private void TraverseXray(string[] textArray)
        {
            bool isDiagnosticReport = false;
            bool procedureFound = false;
            foreach (string nextLine in textArray)
            {
                string printWord = nextLine.ToString();
                if (docType.Length == 0 && printWord.Contains("Diagnostic Imaging Report"))
                {
                    isDiagnosticReport = true;
                    docType = "Imaging";
                    docLocation = "HMC";
                }
                if (isDiagnosticReport)
                {  
                    if (ccName.Length == 0 && printWord.Contains("Nakamura"))
                    {
                        ccName = DOCTOR_NAKAMURA;
                    }
                    if (ccName.Length == 0 && printWord.Contains("Arakaki"))
                    {
                        ccName = DOCTOR_ARAKAKI;
                    }

                    if (patientName.Length == 0 && printWord.Contains("Patient:"))
                    {
                        printWord = printWord.Remove(0, 9);
                        string[] getName = printWord.Split(new string[] { "DOB" }, StringSplitOptions.None);
                        patientName = fixRomanNumbers(getName[0]);
                        if (patientName.Contains(','))
                        {
                            string[] lastFirst = patientName.Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                        }
                    }
                    if (appointmentDate.Length == 0 && printWord.Contains("Service Date"))
                    {
                        string[] servDate = printWord.Split(new string[] { "Service Date:" }, StringSplitOptions.None);
                        if(servDate.Length > 1) appointmentDate = servDate[1];
                    }
                    if (reviewerName.Length ==0 && printWord.Contains("Ordering Physician: "))
                    {
                        string[] orderPhys = printWord.Split(new string[] { "Ordering Physician:" }, StringSplitOptions.None);
                        if(orderPhys.Length > 1) reviewerName = orderPhys[1];
                    }
                    if (procedureDesc.Length == 0 && printWord.Contains("PROCEDURE") || procedureFound)
                    {
                        if (procedureFound) //This is needed to capture next line since PROCEDURE is not the same line as the desc.
                        {
                            if (printWord.Contains('.')) //Used to only get first sentence of procedure.
                            {
                                string[] temp = printWord.Split('.');
                                procedureDesc = temp[0];
                            }
                            else
                            {
                                procedureDesc = printWord.Replace("\n", "");
                            }
                        }
                        procedureFound = !procedureFound;
                    }
                }
            }
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc + "_" + reviewerName + "_" + docLocation +"_" + ccName;
        }

        private void TraverseER(string[] textArray)
        {

            docType = "hospital";
            reviewerName = "ER Report HMC";
            bool foundProcedure = false;
            foreach (string nextLine in textArray)
            {
                string printWord = nextLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("Nakamura,David"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                else if (ccName.Length == 0 && printWord.Contains("Arakaki,Melanie"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                else if (patientName.Length == 0 && printWord.Contains("Patient:"))
                {
                    string[] patName = printWord.Split(new string[] { "Patient:" }, StringSplitOptions.None);
                    if (patName.Length > 1)
                    {
                        string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                        if (lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Service Date:"))
                {
                    string[] getDate = printWord.Split(new string[] { "Service Date:" }, StringSplitOptions.None);
                    if(getDate.Length > 1) appointmentDate = getDate[1];
                }
                else if (procedureDesc.Length == 0 && printWord.Contains("DX::"))
                {
                    foundProcedure = true;
                    continue;
                }
                else if (foundProcedure == true)
                {
                    if (printWord.Contains("Pg"))
                        continue;
                    else if (printWord.Contains("Physician Documentation"))
                        continue;
                    else if (printWord.Contains("Hilo Medical Center"))
                        continue;
                    else if (printWord.Contains("Name:"))
                        continue;
                    else if (printWord.Contains("MR #"))
                        continue;
                    else if (printWord.Contains("DOB:"))
                        continue;
                    else
                    {
                        procedureDesc = printWord;
                        foundProcedure = false;
                    }
                }
            }

            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + procedureDesc + "_" + ccName;
        }
    }
}
