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
    class CreateEstimate
    {
        [FunctionName("CreateEstimate")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "estimate/create")] HttpRequest req, ILogger log)
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
                    var query = @"exec bb.usp_CreateEstimate " + body.PersonID + ", " + body.UserID + ", " + body.GradeOmsorg1 + ", " + body.GradeOmsorg2 + ", " + body.GradeOmsorg3 + ", " + body.GradeOmsorg4 + ", " + body.GradeOmsorg5 + ", " + body.GradeOmsorg6 + ", '" + body.CommentOmsorg + "', " + body.GradeTrygghet1 + ", " + body.GradeTrygghet2 + ", " + body.GradeTrygghet3 + ", " + body.GradeTrygghet4 + ", " + body.GradeTrygghet5 + ", " + body.GradeTrygghet6 + ", " + body.GradeTrygghet7 + ", " + body.GradeTrygghet8 + ", '" + body.CommentTrygghet + "', " + body.GradeMarBra1 + ", " + body.GradeMarBra2 + ", " + body.GradeMarBra3 + ", " + body.GradeMarBra4 + ", " + body.GradeMarBra5 + ", " + body.GradeMarBra6 + ", '" + body.CommentMarBra + "', " + body.GradeFritid1 + ", " + body.GradeFritid2 + ", " + body.GradeFritid3 + ", '" + body.CommentFritid + "', " + body.GradeTillhorighet1 + ", " + body.GradeTillhorighet2 + ", " + body.GradeTillhorighet3 + ", '" + body.CommentTillhorighet + "', " + body.GradeAnsvarstagande1 + ", " + body.GradeAnsvarstagande2 + ", " + body.GradeAnsvarstagande3 + ", " + body.GradeAnsvarstagande4 + ", " + body.GradeAnsvarstagande5 + ", " + body.GradeAnsvarstagande6 + ", " + body.GradeAnsvarstagande7 + ", '" + body.CommentAnsvarstagande + "', " + body.GradeRespekteras1 + ", " + body.GradeRespekteras2 + ", " + body.GradeRespekteras3 + ", " + body.GradeRespekteras4 + ", '" + body.CommentRespekteras + "', " + body.GradeUtvecklas1 + ", " + body.GradeUtvecklas2 + ", " + body.GradeUtvecklas3 + ", " + body.GradeUtvecklas4 + ", " + body.GradeUtvecklas5 + ", '" + body.CommentUtvecklas + "'";
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
