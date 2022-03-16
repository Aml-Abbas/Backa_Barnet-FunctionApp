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
    class GetUsers
    {
        [FunctionName("GetUsers")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<User> userList = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetUsers";
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            UserID = (int)reader[0],
                            LastName = reader[1].ToString(),
                            FirstName = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            RoleID = (int)reader[4],
                            Description = reader[5].ToString(),
                            Organisaton = reader[6].ToString(),
                            UnitID = (int)reader[7],
                            UnitName = reader[8].ToString()
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
