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
    class GetRight
    {
        [FunctionName("GetRight")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/right/{userid}/{personid}")] HttpRequest req, ILogger log,
            [FromRoute] int userid, [FromRoute] int personid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            List<ReturnedUserRight> userRightList = new List<ReturnedUserRight>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    log.LogInformation("UserID is: " + userid);
                    log.LogInformation("PersonID is: " + personid);

                    connection.Open();
                    var query = @"exec bb.usp_GetRight " + userid + " ,"+ personid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();


                    while (reader.Read())
                    {
                        ReturnedUserRight userRight = new ReturnedUserRight()
                        {
                            CurrentUserID = reader[0].ToString(),
                            UserID = reader[1].ToString(),
                            UserLastName = reader[2].ToString(),
                            UserFirstName = reader[3].ToString(),
                            QuestionTypeID = reader[4].ToString(),
                            PersonID = reader[5].ToString(),
                            LastName = reader[6].ToString(),
                            FirstName = reader[7].ToString(),
                        };
                        log.LogInformation("returned right is: " + userRight);

                        userRightList.Add(userRight);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(userRightList);
            
        }

    }
}
