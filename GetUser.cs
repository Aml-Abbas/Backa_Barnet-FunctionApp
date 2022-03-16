﻿using System;
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
    class GetUser
    {
        [FunctionName("GetUser")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{email}")] HttpRequest req, ILogger log, [FromRoute]string email)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<User> userList = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetUser '" + email + "'";
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
                            Description = reader[5].ToString()
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
