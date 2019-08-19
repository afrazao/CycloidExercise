using Cycloid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public class ProgramsRestService : IProgramsService
    {
        public Program getProgram;

        public List<Program> GetProgramsMethod()
        {

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://tomahawk.cycloid.pt/ott.programs/");
                ////HTTP GET
                //var responseTask = client.GetAsync("programs");
                //responseTask.Wait();

                //var resultToRead = responseTask.Result;

                //var result = await resultToRead.Content.ReadAsStringAsync();

                //var programGot = JsonConvert.DeserializeObject<List<Program>>(result);

                //return programGot;

                //var response = client.GetAsync("http://google.com").Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    var responseContent = response.Content;

                //    // by calling .Result you are synchronously reading the result
                //    string responseString = responseContent.ReadAsStringAsync().Result;

                //    Console.WriteLine(responseString);
                //}

                client.BaseAddress = new Uri("http://tomahawk.cycloid.pt/ott.programs/");
                //HTTP GET
                var responseTask = client.GetAsync("programs").Result;

                var resultToRead = responseTask.Content;

                var result = resultToRead.ReadAsStringAsync().Result;

                var programGot = JsonConvert.DeserializeObject<List<Program>>(result);

                return programGot;
            }
        }

    }
}
