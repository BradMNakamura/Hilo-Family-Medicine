using System;
using System.Linq;

namespace ElationAutoImport.medicalRecords
{
    public class hraFile : GeneralForm
    {
        private bool isMammogram = false;
        public hraFile(string[] textArray)
        {
            traverseResult(textArray);
            printInfo();
        }
        private void traverseResult(string[] textArray)
        {
            bool procedureFound = false;
            bool foundName = false;
            docType = "Imaging";
            docLocation = "HRA";
            foreach (string nextLine in textArray)
            {
                string printWord = nextLine.ToString(); //Line needed because of weird bug.
                //Console.WriteLine(printWord);
                if (printWord.Contains(DOCTOR_NAKAMURA))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                if (printWord.Contains(DOCTOR_ARAKAKI))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                string test = nextLine.ToString();
                if (printWord.Contains("DATE OF SERVICE: "))
                {
                    string[] servDate = printWord.Split(new string[] { "DATE OF SERVICE: " }, StringSplitOptions.None);
                    appointmentDate = servDate[1];
                }
                if (printWord.Contains("REFERRING PHYSICIAN:"))
                {
                    string[] refDoctor = printWord.Split(new string[] { "REFERRING PHYSICIAN: " }, StringSplitOptions.None);

                    reviewerName = refDoctor[1]; //Represents only the doctors name after split.
                }
                if (printWord.Contains(",") && !foundName)
                {
                    bool hasDigits = false;
                    for (int i = 0; i < printWord.Length; i++)
                    {
                        if (Char.IsDigit(printWord[i]))
                        {
                            hasDigits = true;
                        }
                    }
                    if (!hasDigits)
                    {
                        foundName = true;
                        patientName = printWord;
                    }
                }
                if (printWord.Contains("PROCEDURE") || procedureFound)
                {
                    if (procedureFound)
                    {
                        if (printWord.Contains('.')) //Used to only get first sentence of procedure.
                        {
                            string[] temp = printWord.Split('.');
                            procedureDesc = temp[0].Replace("was performed", "");
                        }
                        else
                        {
                            procedureDesc = printWord;
                        }
                        procedureFound = false;
                    }
                    else
                    {
                        procedureFound = true;
                    }
                }

                if (printWord.Contains("ACR BI-RADS Category")) //Refer to BI-RADS online
                {
                    isMammogram = true;
                    if (printWord.Contains("0")) //Additional Scanning
                    {
                        reviewerName += " Needs Additional Screening Now ";
                    }
                    else if (printWord.Contains("1") || printWord.Contains("2")) //Benign 1yr recall

                    {
                        string[] splitDate = appointmentDate.Split(new string[] { @"/" }, StringSplitOptions.None);
                        if (splitDate.Length == 3)
                        {
                            DateTime date = new DateTime(Int32.Parse(splitDate[2]), Int32.Parse(splitDate[0]), Int32.Parse(splitDate[1]));
                            reviewerName += " 1 yr recall due " + date.AddYears(1).ToShortDateString();
                        }
                    }
                    else if (printWord.Contains("3")) //6month recall
                    {
                        string[] splitDate = appointmentDate.Split(new string[] { @"/" }, StringSplitOptions.None);
                        if (splitDate.Length == 3)
                        {
                            DateTime date = new DateTime(Int32.Parse(splitDate[2]), Int32.Parse(splitDate[0]), Int32.Parse(splitDate[1]));
                            reviewerName += " 6 months recall due " + date.AddMonths(6).ToShortDateString();
                        }
                    }
                    else if (printWord.Contains("4") || printWord.Contains("5")) //NEEDS BIOPSY
                    {
                        reviewerName += " NEEDS BIOPSY ";
                    }
                    else if (printWord.Contains("6")) //KNOWN CANCER
                    {
                        reviewerName += " KNOWN CANCER ";
                    }

                    docType = "Quality";
                    reviewerName += " WIC ";
                }


            }
            if (docType == "")
            {
                docLocation = "HRA";
                docType = "Imaging";
            }

            //Once traversal is done. Use vars to create the correct filename format.
            RenameFile();
        }

        //Function is needed because weird bug occurs when doing string concationation in printInfo function
        public string reformatWord(string reWord)
        {
            string returnString = "";
            for (int i = 0; i < reWord.Length - 1; i++)
            {
                returnString += reWord[i];
            }
            return returnString;
        }
    }
}
