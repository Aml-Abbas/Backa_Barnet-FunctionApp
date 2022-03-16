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
    class EditCard
    {
        [FunctionName("EditCard")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "card/edit")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            Body body = JsonConvert.DeserializeObject<Body>(content);

            List<Error> errorList = new List<Error>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_EditCard " + body.UserID + ", '" + body.PersonNbr + "', '" + body.Unit + "', '" + body.Situation + "', '" + body.GradedOn + "', " + body.GradeActions + ", '" + body.CommentActions + "', " + body.GradeOmsorg + ", '" + body.CommentOmsorg + "', " + body.GradeTrygghet + ", '" + body.CommentTrygghet + "', " + body.GradeMarBra + ", '" + body.CommentMarBra + "', " + body.GradeFritid + ", '" + body.CommentFritid + "', " + body.GradeTillhorighet + ", '" + body.CommentTillhorighet + "', " + body.GradeAnsvarstagande + ", '" + body.CommentAnsvarstagande + "', " + body.GradeRespekteras + ", '" + body.CommentRespekteras + "', " + body.GradeUtvecklas + ", '" + body.CommentUtvecklas + "', " + body.GradeUpprattats1 + ", " + body.GradeUpprattats2 + ", " + body.GradeSamtycke1 + ", " + body.GradeSamtycke2 + ", " + body.Status + ", " + body.PersonID;
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
                            ErrorMessage = reader[3].ToString(),

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
