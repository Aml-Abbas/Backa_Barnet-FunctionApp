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
    class GetPerson
    {
        [FunctionName("GetPerson")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "person/{userid}")] HttpRequest req, ILogger log, [FromRoute]string userid)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Person> personList = new List<Person>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetPerson " + userid;
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Person person = new Person()
                        {
                            PersonNbr = reader[0].ToString(),
                            LastName = reader[1].ToString(),
                            FirstName = reader[2].ToString(),
                            Guardian1 = reader[3].ToString(),
                            GuardianPersonNbr1 = reader[4].ToString(),
                            Guardian2 = reader[5].ToString(),
                            GuardianPersonNbr2 = reader[6].ToString(),
                            ChangedBy = reader[7].ToString(),
                            ChangedOn = (DateTime)reader[8],
                            Status = reader[9].ToString(),
                            PersonID = (int)reader[10]
                        };
                        personList.Add(person);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(personList);
            
        }

    }
}
