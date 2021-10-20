using System;
using System.Collections.Generic;
using System.Text;

namespace ElationAutoImport
{
    class HearingDoctor : GeneralForm
    {
        const int GENERAL_NUM = 1;
        public HearingDoctor(string [] textArray, int fileType)
        {
            if(fileType == GENERAL_NUM) Traverse_General(textArray);
            printInfo();
        }

        private void Traverse_General(string[] textArray)
        {
            reviewerName = "Hearing Doctor of Hawaii LLC";
            docType = "Consult";
            string refName = "";
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
                else if (patientName.Length == 0 && printWord.Contains("Patient Name"))
                {
                    string [] getPat = printWord.Split(new string[] { "Patient Name" }, StringSplitOptions.None);
                    if (getPat.Length > 1) 
                    {
                        getPat[1] = getPat[1].TrimStart(":; ".ToCharArray());
                        string[] patName = getPat[1].Split(new string[] { "Audiologist" }, StringSplitOptions.None);
                        patientName = patName[0].TrimEnd(' ');
                    }
                }
                else if (appointmentDate.Length == 0 && printWord.Contains("Visit Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Visit Date" }, StringSplitOptions.None);
                    if (getDate.Length > 1)
                    {
                        getDate[1] = getDate[1].TrimStart(":; ".ToCharArray());
                        string[] patName = getDate[1].Split(new string[] { "Referral" }, StringSplitOptions.None);
                        appointmentDate = patName[0].TrimEnd(' ');
                        if (refName.Length == 0 && printWord.Contains("Referral"))
                        {
                            if (!printWord.Contains("Nakamura") && !printWord.Contains("Arakaki"))
                            {
                                string[] getInfo = printWord.Split(new string[] { "Referral" }, StringSplitOptions.None);
                                if(getInfo.Length > 1) refName = getInfo[1].Trim(":; ".ToCharArray());
                            }
                        }
                    }
                }
                else if (refName.Length == 0 && printWord.Contains("Referral"))
                {
                    if(!printWord.Contains("Nakamura") && !printWord.Contains("Arakaki"))
                    {
                        string[] getInfo = printWord.Split(new string[] { "Referral" }, StringSplitOptions.None);   
                        if (getInfo.Length > 1) refName = getInfo[1].Trim(":; ".ToCharArray());
                    }
                }
                
            }
            if(refName.Length == 0) fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + ccName;
            else fileName = patientName + "_" + appointmentDate + "_" + docType + "_" + reviewerName + "_" + refName + "_" + ccName;
        }

    }
}
