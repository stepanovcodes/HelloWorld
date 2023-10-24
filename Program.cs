using System;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard
             + "','" + myComputer.HasWifi
             + "','" + myComputer.HasLTE
             + "','" + myComputer.ReleaseDate
             + "','" + myComputer.Price
             + "','" + myComputer.VideoCard
             + "')\n";

            // File.WriteAllText("log.txt", sql);

            using StreamWriter openFile = new("log.txt", append: true);

            openFile.WriteLine(sql);

            openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            Console.WriteLine(fileText);

        }
    }
}