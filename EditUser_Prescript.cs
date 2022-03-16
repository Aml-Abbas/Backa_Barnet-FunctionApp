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
    class EditUser_Prescript
    {
        [FunctionName("EditUser_Prescript")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user/edit/{userid}")] HttpRequest req, ILogger log, [FromRoute]int userid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Error> errorList = new List<Error>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_EditUser_Prescript " + userid;
                    log.LogInformation(query);
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Error error = new Error()
                        {
                            ErrorNumber = (int)reader[0],
                            ErrorSeverity = (int)reader[1],
                            ErrorState = (int)reader[2],
                            ErrorMessage = reader[3].ToString()

                        };
                        errorList.Add(error);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(errorList);
            
        }

    }
}
