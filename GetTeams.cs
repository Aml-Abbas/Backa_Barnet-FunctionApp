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
    class GetTeams
    {
        [FunctionName("GetTeams")]
        public static async Task<IActionResult>
        Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "teams")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Team> teamList = new List<Team>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"exec bb.usp_GetTeams";
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Team team = new Team()
                        {
                            TeamID = (int)reader[0],
                            TeamName = reader[1].ToString(),
                            UnitID = (int)reader[2],
                            UnitName = reader[3].ToString(),
                            CreatedBy = reader[4].ToString(),
                            CreatedOn = (DateTime)reader[5],
                            ChangedBy = reader[6].ToString(),
                            ChangedOn = (DateTime)reader[7]
                        };
                        teamList.Add(team);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
            }
            
            return (ActionResult)new OkObjectResult(teamList);
            
        }

    }
}
