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
    class GetCard
    {
        [FunctionName("GetCard")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "card/{userid}")] HttpRequest req, ILogger log, [FromRoute]int userid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Card> cardList = new List<Card>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetCard " + userid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Card card = new Card()
                        {
                            GradedOn = (DateTime)reader[0],
                            UserName = reader[1].ToString(),
                            UserOrg = reader[2].ToString(),
                            UserTitle = reader[3].ToString(),
                            PersonName = reader[4].ToString(),
                            PersonNbr = reader[5].ToString(),
                            Guardian1 = reader[6].ToString(),
                            GuardianPersonNbr1 = reader[7].ToString(),
                            Guardian2 = reader[8].ToString(),
                            GuardianPersonNbr2 = reader[9].ToString(),
                            Unit = reader[10].ToString(),
                            Situation = reader[11].ToString(),
                            QuestionID = (int)reader[12],
                            Grade = (int)reader[13],
                            Comment = reader[14].ToString(),
                            Status = reader[15].ToString(),
                            PersonID = (int)reader[16]
                        };
                        cardList.Add(card);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(cardList);
            
        }

    }
}
