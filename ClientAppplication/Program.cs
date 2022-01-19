using ClientAppplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientAppplication
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAllStudents().Wait();
            GetAllStudentById().Wait();
            InsertRecord().Wait();
            EditRecord(5).Wait();
            DeleteRecord(1).Wait();
         }
        static async Task GetAllStudents()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Students1");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var studentList = JsonConvert.DeserializeObject<List<Student>>(jsonString.Result);
                foreach (var student in studentList)
                {
                    Console.WriteLine(student.Id + " " + student.Name + " " + student.Batch + " " + student.Marks); ;
                }

            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Some Error occurred");
            }
        }

        static async Task GetAllStudentById()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Students1/1");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var student  = JsonConvert.DeserializeObject<Student>(jsonString.Result);
                
                    Console.WriteLine(student.Id + " " + student.Name + " " + student.Batch + " " + student.Marks); ;
                

            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Some Error occurred");
            }
        }



        static async Task InsertRecord()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var student = new Student() { Name = "Madhav", Batch = "B003", Marks = 89 };

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Students1", student);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Record inserted");

            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Some Error occurred");
            }
        }

        static async Task EditRecord(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var student = new Student() { Batch = "New Batch", Marks = 100 };

            HttpResponseMessage response = await client.PutAsJsonAsync("api/Students1/" + id, student);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Record edited");

            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Some Error occurred");
            }
        }


        static async Task DeleteRecord(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync("api/Students1/" + id);
              if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Record deleted");

            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Some Error occurred");
            }
        }
    }
}
