using System;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Text.Json;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;
using System.Xml;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // File.WriteAllText("log.txt", sql);

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine(sql);

            // openFile.Close();

            string computersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(computersJson);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            if (computersNewtonSoft != null)
            {
                foreach (Computer computer in computersNewtonSoft)
                {
                    // Console.WriteLine(computer.Motherboard);
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                     + "','" + computer.HasWifi
                     + "','" + computer.HasLTE
                     + "','" + computer.ReleaseDate
                     + "','" + computer.Price
                     + "','" + EscapeSingleQuote(computer.VideoCard)
                     + "')\n";

                     dapper.ExecuteSql(sql);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);
            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
            File.WriteAllText("computersCopySystem.txt", computersCopySystem);
        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }
    }
}