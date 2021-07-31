using System;
using System.Linq;

namespace ElationAutoImport.medicalRecords
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
                if (printWord.Contains("Diagnostic Imaging Report"))
                {
                    isDiagnosticReport = true;
                    docType = "Imaging";
                    docLocation = "HMC";
                }
                if (isDiagnosticReport)
                {
                    //Console.WriteLine(printWord);   
                    if (printWord.Contains("Nakamura,David"))
                    {
                        ccName = DOCTOR_NAKAMURA;
                    }
                    if (printWord.Contains("Arakaki,Melanie"))
                    {
                        ccName = DOCTOR_ARAKAKI;
                    }

                    if (printWord.Contains("Patient:"))
                    {
                        printWord = printWord.Remove(0, 9);
                        string[] patName = printWord.Split(new string[] { "DOB" }, StringSplitOptions.None);
                        patientName = fixRomanNumbers(patName[0]);
                    }
                    if (printWord.Contains("Service Date"))
                    {
                        string test = "Service Date: ";
                        string[] servDate = printWord.Split(new string[] { "Service Date: " }, StringSplitOptions.None);
                        appointmentDate = servDate[1];
                    }
                    if (printWord.Contains("Ordering Physician: "))
                    {
                        string[] orderPhys = printWord.Split(new string[] { "Ordering Physician: " }, StringSplitOptions.None);
                        reviewerName = orderPhys[1];
                    }
                    if (printWord.Contains("PROCEDURE") || procedureFound)
                    {
                        if (procedureFound) //This is needed to capture next line since PROCEDURE is not the same line as the desc.
                        {
                            Console.WriteLine(printWord);
                            if (printWord.Contains('.')) //Used to only get first sentence of procedure.
                            {
                                string[] temp = printWord.Split('.');
                                procedureDesc = temp[0];
                            }
                            else
                            {
                                procedureDesc = printWord;
                            }
                            procedureFound = false;
                        }
                        else
                        {
                            procedureFound = true; //This will capture the next line in the foreach loop. Which is the procedure.
                        }
                    }
                }
            }

            //Once traversal is done. Use vars to create the correct filename format.
            RenameFile();
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
                    string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Service Date:"))
                {
                    string[] getDate = printWord.Split(new string[] { "Service Date:" }, StringSplitOptions.None);
                    appointmentDate = getDate[1];
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
