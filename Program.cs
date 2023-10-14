﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            DataContextEF entityFramework = new DataContextEF(config);

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

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

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

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
             + "')";
            // Console.WriteLine(sql);
            //  int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);


            // Console.WriteLine(result);

            string sqlSelect = @"SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId
             + "','" + singleComputer.Motherboard
             + "','" + singleComputer.HasWifi
             + "','" + singleComputer.HasLTE
             + "','" + singleComputer.ReleaseDate
             + "','" + singleComputer.Price
             + "','" + singleComputer.VideoCard
             + "'");
            }

            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            if (computersEF != null)
            {

                Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine("'" + singleComputer.ComputerId
             + "','" + singleComputer.Motherboard
             + "','" + singleComputer.HasWifi
             + "','" + singleComputer.HasLTE
             + "','" + singleComputer.ReleaseDate
             + "','" + singleComputer.Price
             + "','" + singleComputer.VideoCard
             + "'");
                }

            }

            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLTE);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);

        }
    }
}