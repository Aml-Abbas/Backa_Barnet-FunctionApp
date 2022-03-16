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
    class GetBarnKontakt
    {
        [FunctionName("GetBarnKontakt")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "barnkontakt/{personid}")] HttpRequest req, ILogger log, [FromRoute]int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<User> userList = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetBarnKontakt " + personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            Name = reader[0].ToString(),
                            Email = reader[1].ToString(),
                            Organisaton = reader[2].ToString()
                        };
                        userList.Add(user);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(userList);
            
        }

    }
}
