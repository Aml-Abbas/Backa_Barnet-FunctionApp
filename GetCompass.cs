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
    class GetCompass
    {
        [FunctionName("GetCompass")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "compass/{personid}")] HttpRequest req, ILogger log, [FromRoute]int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Compass> compassList = new List<Compass>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetCompass " + personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Compass compass = new Compass()
                        {
                            QuestionID = (int)reader[0],
                            QuestionTypeID = (int)reader[1],
                            QuestionLevelID = (int)reader[2],
                            PersonID = (int)reader[3],
                            UserID = (int)reader[4],
                            Grade = (int)reader[5],
                            Comment = reader[6].ToString(),
                            GradedOn = (DateTime)reader[7],
                            UserName = reader[8].ToString()
                        };
                        compassList.Add(compass);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(compassList);
            
        }

    }
}
