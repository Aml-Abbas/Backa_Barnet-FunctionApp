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
    class GetEstimate
    {
        [FunctionName("GetEstimate")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "estimate/{personid}")] HttpRequest req, ILogger log, [FromRoute]int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Estimate> estimateList = new List<Estimate>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetEstimate " + personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Estimate estimate = new Estimate()
                        {
                            QuestionID = (int)reader[0],
                            PersonID = (int)reader[1],
                            UserID = (int)reader[2],
                            Grade = (int)reader[3],
                            Comment = reader[4].ToString(),
                            GradedOn = (DateTime)reader[5],
                            ChangedOn = (DateTime)reader[6],
                            Status = reader[7].ToString(),
                            Description = reader[8].ToString(),
                            QuestionLevelID = (int)reader[9],
                            UserName = reader[10].ToString(),
                            UserRole = reader[11].ToString()
                        };
                        estimateList.Add(estimate);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(estimateList);
            
        }

    }
}
