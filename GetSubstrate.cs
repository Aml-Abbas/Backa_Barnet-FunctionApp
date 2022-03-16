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
    class GetSubstrate
    {
        [FunctionName("GetSubstrate")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "substrate/{personid}")] HttpRequest req, ILogger log, [FromRoute]int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Substrate> substrateList = new List<Substrate>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetSubstrate " + personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Substrate substrate = new Substrate()
                        {
                            QuestionID = (int)reader[0],
                            PersonID = (int)reader[1],
                            Grade = (int)reader[2],
                            Comment = reader[3].ToString(),
                            GradeType = reader[4].ToString(),
                            GradedOn = (DateTime)reader[5],
                            Status = reader[6].ToString(),
                            UserName = reader[7].ToString(),
                            UserRole = reader[8].ToString()
                        };
                        substrateList.Add(substrate);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(substrateList);
            
        }

    }
}
