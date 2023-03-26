using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD_Json_Operation
{
    class StudentList
    {
        private string jsonFilePath = @"D:\Projects\CRUD_Json_Operation\CRUD_Json_Operation\studentlist.json";
        
        public void DashBoard()
        {
            Console.WriteLine("Choose Your Options:\n 1 - Student List\n 2 - Add Student\n 3 - Update Student\n 4 - Delete Student\n 5 - Exit");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1": GetStudentList(); break;
                case "2": AddStudent(); break;
                case "3": UpdateStudent(); break;
                case "4": DeleteStudent(); break;
                case "5": Exitmanagement(); break;
                default: DashBoard(); break;
            }
        }
        public void GetStudentList()
        {
            string jsonFileRead = File.ReadAllText(jsonFilePath);
            try
            {
                JObject jObject = JObject.Parse(jsonFileRead);
                if (jObject != null)
                {
                    JArray studentlistArrary = jObject["studentlist"] as JArray;
                    Console.WriteLine("------------ Student List ------------\nID\t|\tName\t\t|\tStatus\n");
                    foreach (JToken item in studentlistArrary)
                    {
                        Console.WriteLine(item["studentid"] + "\t|\t"+ item["studentname"].ToString() + "\t\t|\t"+ (item["studentstatus"].ToString().ToLower() == "true" ? "Active" : "De-Active"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in student list...!" + ex.Message.ToString());
            }
            Console.WriteLine("======================================\n");
            DashBoard();
        }
        public void AddStudent()
        {
            Console.Write("Enter Student ID: ");
            var studentid = Console.ReadLine();
            Console.Write("Enter Student Name: ");
            var studentname = Console.ReadLine();
            Console.Write("Enter Student Status (Active= A / De-Active= D): ");
            var studentstatus = Console.ReadLine();
            var newStudent = "{ 'studentid': " + studentid + ", 'studentname': '" + studentname + "','studentstatus': "+ (studentstatus.ToLower() == "a" ? "true":"false") + "}";
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                JObject jsonObj = JObject.Parse(json);
                JArray studentListArrary = jsonObj.GetValue("studentlist") as JArray;
                studentListArrary.Add(JObject.Parse(newStudent));
                jsonObj["studentlist"] = studentListArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFilePath, newJsonResult);
                Console.WriteLine("$$$ Record added successfully. $$$\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
            DashBoard();
        }   
        public void DeleteStudent()
        {
            var json = File.ReadAllText(jsonFilePath);
            try
            {
                JObject jObject = JObject.Parse(json);
                JArray studentListArrary = (JArray)jObject["studentlist"];
                Console.Write("Enter Student ID to Delete Student Record: ");
                int studentId = Convert.ToInt32(Console.ReadLine());
                JToken studenttoDelete = studentListArrary.FirstOrDefault(obj => obj["studentid"].Value<int>() == studentId);

                if (studenttoDelete != null)
                {
                    studentListArrary.Remove(studenttoDelete);
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFilePath, output);
                    Console.WriteLine("$$$ Record deleted successfully. $$$\n");
                }
                else
                {
                    Console.WriteLine("Invalid Student ID, Try Again...!");
                }
                DashBoard();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error : " + ex.Message.ToString());
            }
        }
        public void UpdateStudent()
        {
            string json = File.ReadAllText(jsonFilePath);
            try
            {
                JObject jObject = JObject.Parse(json);
                JArray studentListArrary = (JArray)jObject["studentlist"];
                Console.Write("Enter Student ID to Update Details : ");
                var studentId = Convert.ToInt32(Console.ReadLine());
                JToken studenttoUpdate = studentListArrary.FirstOrDefault(obj => obj["studentid"].Value<int>() == studentId);
                if (studenttoUpdate != null)
                {
                    Console.Write("Enter New Student Name: ");
                    var studentname = Console.ReadLine();
                    Console.Write("Enter new student status (Active= A / De-Active= D): ");
                    var studentstatus = Console.ReadLine();

                    foreach (var company in studentListArrary.Where(obj => obj["studentid"].Value<int>() == studentId))
                    {
                        company["studentname"] = !string.IsNullOrEmpty(studentname) ? studentname : "";
                        company["studentstatus"] = !string.IsNullOrEmpty(studentstatus) ? (studentstatus.ToLower() == "a" ? "true" : "false") : "false";

                    }
                    jObject["studentlist"] = studentListArrary;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFilePath, output);
                    Console.WriteLine("$$$ Record updated successfully. $$$\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
            DashBoard();
        }
        public void Exitmanagement()
        {
            Console.WriteLine("Thank You, Visit Again");
            Thread.Sleep(1000);
            Environment.Exit(-1);
        }
    }
}
