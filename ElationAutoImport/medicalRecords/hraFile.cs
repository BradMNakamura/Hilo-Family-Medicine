using System;
using System.Linq;

namespace ElationAutoImport
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
                if (ccName.Length == 0 && printWord.Contains("NAKAMURA"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                if (ccName.Length == 0 && printWord.Contains("ARAKAKI"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                string test = nextLine.ToString();
                if (appointmentDate.Length == 0 && printWord.Contains("DATE OF SERVICE:"))
                {
                    string[] servDate = printWord.Split(new string[] { "DATE OF SERVICE:" }, StringSplitOptions.None);
                    if(servDate.Length > 1) appointmentDate = servDate[1];
                }
                if (reviewerName.Length == 0 && printWord.Contains("REFERRING PHYSICIAN:"))
                {
                    string[] refDoctor = printWord.Split(new string[] { "REFERRING PHYSICIAN:"}, StringSplitOptions.None);
                    if(refDoctor.Length > 1) reviewerName = refDoctor[1]; //Represents only the doctors name after split.
                }
                if (patientName.Length == 0 && printWord.Contains(",") && !foundName)
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
                        if (patientName.Contains(','))
                        {
                            string[] lastFirst = patientName.Split(new string[] { "," }, StringSplitOptions.None);
                            if(lastFirst.Length > 1) patientName = lastFirst[1] + "," + lastFirst[0];
                        }
                    }
                }
                if (procedureDesc.Length == 0 && printWord.Contains("PROCEDURE") || procedureFound)
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
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc + "_" + reviewerName + "_" + docLocation + "_" + ccName;
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
