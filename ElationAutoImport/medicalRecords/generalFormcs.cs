using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElationAutoImport.medicalRecords
{
    public abstract class GeneralForm
    {
        public string patientName { get;  set; } //Location of patient document.
        public string reviewerName { get;  set; } //Doctor Name
        public string ccName { get;  set; }
        public string appointmentDate { get; set; } //When appointment happened.
        public string procedureDesc { get;  set; } //What procedure was done.
        public string docType { get; set; } //Type of document.
        public string docLocation { get; set; } //Location of building.
        public string docComments { get; set; } //Used for comment box on Elation.
        public string fileName { get; protected set; } //Used to capture the correct format of the file based on declared vars above.
        protected List<string> docTags;  //Tags to put on document
        protected const string SERVICE_DATE = "SERVICE: ";
        protected const string DOCTOR_NAKAMURA = "DAVID NAKAMURA, MD";
        protected const string DOCTOR_ARAKAKI = "MELANIE ARAKAKI, MD";

        protected List<(string, string)> captureWords; //Keyword , Captured Word I.E Patient Name: , Brad Nakamura
        public GeneralForm()
        {
            docTags = new List<string>();
            patientName = "";
            reviewerName = "";
            docLocation = "";
            ccName = "";
            appointmentDate = "";
            procedureDesc = "";
            docType = "";
            docComments = "";
            fileName = "";
        }

        protected void addCaptureWords(string[] keyWords)
        {
            foreach (string word in keyWords)
            {
                captureWords.Add((word, ""));
            }
        }
        protected void fileRename(string sourceFile, string renameVar)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
            if (fi.Exists)
            {
                fi.MoveTo(@"C:\Users\bradm\source\repos\Mamo_AutoImport\Mamo_AutoImport\" + renameVar + ".pdf");
                Console.WriteLine("Worked");
            }
            else
            {
                Console.WriteLine("Nope");
            }
        }

        protected void printInfo()
        {
            /*Console.WriteLine("Patient Name: " + patientName);
            Console.WriteLine("Ordering Doctor: " + reviewerName);
            Console.WriteLine("Date: " + appointmentDate);
            Console.WriteLine("Procedure: " + procedureDesc);
            Console.WriteLine("Doc Type: " + docType);
            Console.WriteLine("Reviewing Doctor: " + ccName);*/
            Console.WriteLine("Filename: " + fileName);
            Console.WriteLine();
        }


        protected string fixRomanNumbers(string patientName)
        {
            string fixedName = "";
            for (int i = 0; i < patientName.Length; i++)
            {
                if (patientName[i] == '|')
                    fixedName += "I";
                else
                    fixedName += patientName[i];
            }
            return fixedName;
        }

        public void RenameFile()
        {
            fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + procedureDesc + "_" + reviewerName + "_" + docLocation + "_" + ccName;
        }
    }
}
