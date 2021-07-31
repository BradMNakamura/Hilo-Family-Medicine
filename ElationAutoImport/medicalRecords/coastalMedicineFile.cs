﻿using System;
using System.Reflection;
using System.IO;
using System.Xml;
namespace ElationAutoImport.medicalRecords
{
    class coastalMedicalFile : GeneralForm
    {
        const int CPAP_SUPPLIES = 1;
        public coastalMedicalFile(string[] textArray, int fileType)
        {
            if (fileType == CPAP_SUPPLIES)
            {
                traverse_CpapAndSupplies(textArray);
            }
            //WARNING!
            //IF ADDING MORE FUNCTIONS. DO NOT PUT fileName = patientNAme ect. HERE. NEEDS TO GO INSIDE THE FUNCTION.
            printInfo();
        }

        private void traverse_CpapAndSupplies(string[] textArray)
        {
            reviewerName = "COASTAL MEDICAL SUPPLY CPAP"; //If changed dont forget about form2.
            string userData = "";
            bool foundName = false;
            foreach (string readLine in textArray)
            {
                string printWord = readLine.ToString();
                if (ccName.Length == 0 && printWord.Contains("NAKAMURA"))
                {
                    ccName = DOCTOR_NAKAMURA;
                }
                if (ccName.Length == 0 && printWord.Contains("ARAKAKI"))
                {
                    ccName = DOCTOR_ARAKAKI;
                }
                if (patientName.Length == 0 && printWord.Contains("PATIENT:"))
                {
                    string[] patName = printWord.Split(new string[] { "PATIENT:" }, StringSplitOptions.None);
                    string[] lastFirst = patName[1].Split(new string[] { "," }, StringSplitOptions.None);
                    patientName = lastFirst[1] + "," + lastFirst[0];
                }
                if (appointmentDate.Length == 0 && printWord.IndexOf("Date:", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    for (int i = 5; i < 15; i++)
                    {
                        appointmentDate += printWord[i];
                    }
                }
                if (procedureDesc.Length == 0 && printWord.Contains("Initial Date"))
                {
                    string[] getDate = printWord.Split(new string[] { "Initial Date" }, StringSplitOptions.None);
                    procedureDesc = getDate[1];
                }
            }
            RenameFile();
            //Save into xml file.
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var loadNames = Path.Combine(outPutDirectory, "ResourceFiles\\savedFolder.xml");
            loadNames = loadNames.Replace(@"file:\", "");
            XmlDocument doc = new XmlDocument();
            doc.Load(loadNames);
            XmlNode testPatient = doc.CreateElement("patient");
            userData = patientName + procedureDesc;
            userData = userData.Replace(" ", "");
            testPatient.InnerText = userData;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                
                if (node.InnerText == userData) //Found Duplicate
                {
                    Console.WriteLine("Found duplicate: " + userData);
                    foundName = true;
                    break;
                }
            }
            if (foundName == true)
            {
                fileName = "DUPLICATE " + fileName;
                return;
            }
            else
            {
                doc.DocumentElement.AppendChild(testPatient);
            }
            doc.Save(loadNames);
        }
    }
}