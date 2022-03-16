using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BackaBarnet
{
    class GetEvent
    {
        [FunctionName("GetEvent")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "event/{personid}")] HttpRequest req, ILogger log, [FromRoute]int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Event> eventList = new List<Event>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetEvent " + personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Event events = new Event()
                        {
                            ActionID = (int)reader[0],
                            ActionDescription = reader[1].ToString(),
                            EventID = (int)reader[2],
                            EventDescription = reader[3].ToString(),
                            PersonID = (int)reader[4],
                            Responsible = reader[5].ToString(),
                            Profession = reader[6].ToString(),
                            Status = reader[7].ToString(),
                            CreatedOn = (DateTime)reader[8]
                        };
                        eventList.Add(events);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(eventList);
            
        }

    }
}
