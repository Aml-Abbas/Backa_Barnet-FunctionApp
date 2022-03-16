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
    class CreateSubstrate
    {
        [FunctionName("CreateSubstrate")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "substrate/create")] HttpRequest req, ILogger log)
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
                    var query = @"exec bb.usp_CreateSubstrate " + body.UserID + ", " + body.PersonID + ", '" + body.GuardianNbr1 + "', '" + body.GuardianNbr2 + "', " + body.GradeOmsorg + ", '" + body.CommentOmsorg + "', " + body.GradeOmsorg1 + ", '" + body.CommentOmsorg1 + "', " + body.GradeOmsorg2 + ", '" + body.CommentOmsorg2 + "', " + body.GradeTrygghet + ", '" + body.CommentTrygghet + "', " + body.GradeTrygghet1 + ", '" + body.CommentTrygghet1 + "', " + body.GradeTrygghet2 + ", '" + body.CommentTrygghet2 + "', " + body.GradeMarBra + ", '" + body.CommentMarBra + "', " + body.GradeMarBra1 + ", '" + body.CommentMarBra1 + "', " + body.GradeMarBra2 + ", '" + body.CommentMarBra2 + "', " + body.GradeFritid + ", '" + body.CommentFritid + "', " + body.GradeFritid1 + ", '" + body.CommentFritid1 + "', " + body.GradeFritid2 + ", '" + body.CommentFritid2 + "', " + body.GradeTillhorighet + ", '" + body.CommentTillhorighet + "', " + body.GradeTillhorighet1 + ", '" + body.CommentTillhorighet1 + "', " + body.GradeTillhorighet2 + ", '" + body.CommentTillhorighet2 + "', " + body.GradeAnsvarstagande + ", '" + body.CommentAnsvarstagande + "', " + body.GradeAnsvarstagande1 + ", '" + body.CommentAnsvarstagande1 + "', " + body.GradeAnsvarstagande2 + ", '" + body.CommentAnsvarstagande2 + "', " + body.GradeRespekteras + ", '" + body.CommentRespekteras + "', " + body.GradeRespekteras1 + ", '" + body.CommentRespekteras1 + "', " + body.GradeRespekteras2 + ", '" + body.CommentRespekteras2 + "', " + body.GradeUtvecklas + ", '" + body.CommentUtvecklas + "', " + body.GradeUtvecklas1 + ", '" + body.CommentUtvecklas1 + "', " + body.GradeUtvecklas2 + ", '" + body.CommentUtvecklas2 + "', " + body.Status;
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
