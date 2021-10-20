using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Threading;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace ElationAutoImport
{
    class ElationAPI
    {
        List<string> apiCred;
        string auth_header;
        public ElationAPI()
        {
            apiCred = new List<string>();
            XmlDocument doc = new XmlDocument();
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var savedPath = Path.Combine(outPutDirectory, @"apicred.xml");
            savedPath = savedPath.Replace(@"file:\", "");
            Console.WriteLine(savedPath);
            doc.Load(savedPath);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                apiCred.Add(node.InnerText);
            }
            string cred = apiCred[0] + ":" + apiCred[1];
            byte[] utf8 = Encoding.Default.GetBytes(cred);
            cred = Encoding.UTF8.GetString(utf8);

            var auth_token = Convert.ToBase64String(utf8);
            auth_header = "Basic " + auth_token;
            var client = new RestClient("https://sandbox.elationemr.com/api/2.0/oauth2/token/");
            var request = new RestRequest(Method.POST);
            request.Parameters.Clear();
            request.AddHeader("Authorization", auth_header);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", apiCred[2]);
            request.AddParameter("password", apiCred[3]);
            IRestResponse response = client.Execute(request);

            dynamic resp = JObject.Parse(response.Content);
            Console.WriteLine(resp);
            string access_token = (string)resp["access_token"];

            //Console.WriteLine("Token: " + access_token);
            auth_header = "Bearer " + access_token;

            //var req = PostRequest("https://sandbox.elationemr.com/api/2.0/patients/140808726970369/", auth_header);
            //Console.WriteLine(CreateReferral(req));
        }

        public JObject FindPatient(string firstName)
        {
            return PostRequest("https://sandbox.elationemr.com/api/2.0/patients/?first_name="+firstName, auth_header);
        }

        public JObject ExactPatient(string patientID)
        {
            return PostRequest("https://sandbox.elationemr.com/api/2.0/patients/" + patientID + "/", auth_header);
        }
        public string UploadReport(Dictionary<string, string> generalForm, JObject patientFile)
        {
            Console.WriteLine(patientFile);
            Console.WriteLine("Report");
            Byte[] bytes = File.ReadAllBytes(generalForm["filename"]);
            string base64Content = Convert.ToBase64String(bytes);
            Dictionary<string, dynamic> patientReport = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> fileContent = new Dictionary<string, dynamic>();
            List<dynamic> files = new List<dynamic>();

            string appDay;
            string appMonth;
            string appYear;
            string[] getInfo = generalForm["date"].Split(new string[] { "." }, StringSplitOptions.None);
            string fileName = Path.GetFileNameWithoutExtension(generalForm["filename"]);
            appDay = getInfo[0];
            appMonth = getInfo[1];
            appYear = getInfo[2];
            fileContent.Add("content_type", "application/octet-stream");
            fileContent.Add("original_filename", "combo.png");
            fileContent.Add("base64_content", base64Content);
            files.Add(fileContent);
            patientReport.Add("custom_title", fileName);
            patientReport.Add("report_type", generalForm["doc_type"]);
            patientReport.Add("chart_date", appYear+"-"+appDay+"-"+appMonth+ "T22:01:08Z");
            patientReport.Add("document_date", appYear + "-" + appDay + "-" + appMonth + "T22:01:08Z");
            patientReport.Add("patient", patientFile["id"]);
            patientReport.Add("physician", patientFile["primary_physician"]);
            patientReport.Add("practice", patientFile["caregiver_practice"]);
            patientReport.Add("files", files);
            var test = JsonConvert.SerializeObject(patientReport);
            //auth_header = "Bearer " + access_token;
            var client = new RestClient("https://sandbox.elationemr.com/api/2.0/reports/");
            var request = new RestRequest(Method.POST);
            request.Parameters.Clear();
            request.AddHeader("Authorization", auth_header);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(test);
            var response = client.Execute(request);
            var resp = JObject.Parse(response.Content);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(resp);
            return response.StatusCode.ToString();
        }

        public string CreateReferral(JObject patientFile)
        {
            string patientRef = "";
            Console.WriteLine((string)patientFile["id"]);
            //Get Past Medical History and Family History
            var patientHistory = PostRequest("https://sandbox.elationemr.com/api/2.0/histories/", auth_header);
            patientRef += "Past Medical History:\n";
            foreach (dynamic medItem in patientHistory["results"])
            {
                Console.WriteLine(medItem);
                if ((string)medItem["type"] == "Past")
                {
                    patientRef += (string)medItem["text"] + ". ";
                }
            }
            //Get Medication
            var patientMedication = PostRequest("https://sandbox.elationemr.com/api/2.0/medications/", auth_header);
            Console.WriteLine(patientMedication);
            patientRef += "\n\nMedications:\n";
            foreach (dynamic medItem in patientMedication["results"])
            {
                Console.WriteLine(medItem);
                var medName = medItem["medication"];
                patientRef += (string)medName["name"];
                if ((string)medItem["directions"] == "") patientRef += ", ";
                else patientRef += " : " + (string)medItem["directions"] + ", ";
            }
            Thread.Sleep(1000);
            //Get Allergies
            var patientAllergies = PostRequest("https://sandbox.elationemr.com/api/2.0/allergies/", auth_header);
            Console.WriteLine(patientAllergies);
            patientRef += "\n\nAllergies:\n";
            foreach (dynamic medItem in patientAllergies["results"])
            {
                patientRef += (string)medItem["name"];
            }

            //Get Family History
            var familyHistory = PostRequest("https://sandbox.elationemr.com/api/2.0/family_histories/", auth_header);
            Console.WriteLine(familyHistory);
            patientRef += "\n\nFamily History:\n";
            string prevWord = "";
            foreach (dynamic medItem in familyHistory["results"])
            {
                if (prevWord == "")
                {
                    prevWord = (string)medItem["relationship"];
                    patientRef += (string)medItem["relationship"] + ": ";
                    patientRef += (string)medItem["text"] + ". ";
                }
                else if (prevWord != (string)medItem["relationship"])
                {
                    prevWord = (string)medItem["relationship"];
                    patientRef += "\n" + (string)medItem["relationship"] + ": ";
                    patientRef += (string)medItem["text"] + ". ";
                }
                else
                    patientRef += (string)medItem["text"] + ". ";
            }
            patientRef += "\n";
            foreach (dynamic medItem in patientHistory["results"]) //Get "Family Text" (Text not in categories.) I.e Mother, Father, ect.
            {
                if ((string)medItem["type"] == "Family")
                {
                    patientRef += (string)medItem["text"] + ". ";
                }
            }

            //Get Social History from patientHistory.hgg
            patientRef += "\n\nSocial History:\n";
            foreach (dynamic medItem in patientHistory["results"])
            {
                Console.WriteLine(medItem);
                if ((string)medItem["type"] == "Social")
                {
                    patientRef += (string)medItem["text"] + ". ";
                }
            }

            return patientRef.Replace("\n", "\r\n");
        }
        public JObject PostRequest(string url, string auth_header)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.Parameters.Clear();
            request.AddHeader("Authorization", auth_header);
            IRestResponse response = client.Execute(request);
            response = client.Execute(request);
            Console.WriteLine(response.StatusCode);
            dynamic resp = JObject.Parse(response.Content);
            return resp;
        }
    }
}
