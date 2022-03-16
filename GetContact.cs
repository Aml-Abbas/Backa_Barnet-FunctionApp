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
    class GetContact
    {
        [FunctionName("GetContact")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contact/{personnbr}")] HttpRequest req, ILogger log, [FromRoute]string personnbr)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Contact> contactList = new List<Contact>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetContact " + personnbr;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Contact contact = new Contact()
                        {
                            ContactPersonNbr = reader[0].ToString(),
                            LastName = reader[1].ToString(),
                            FirstName = reader[2].ToString(),
                            PhoneNbr = reader[3].ToString(),
                            Employer = reader[4].ToString()
                        };
                        contactList.Add(contact);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(contactList);
            
        }

    }
}
