using System;

namespace MyApp // Note: actual namespace depends on the project name.
{

    public class Computer
    {
        public string Motherboard { get; set; } = "";

        public int CPUCores { get; set; }

        public bool HasWifi { get; set; }

        public bool HasLTE { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public string VideoCard { get; set; } = "";

    }
    internal class Program
    {
        static void Main(string[] args)
        {

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            Console.WriteLine(myComputer.Motherboard);
            Console.WriteLine(myComputer.HasWifi);
            Console.WriteLine(myComputer.HasLTE);
            Console.WriteLine(myComputer.ReleaseDate);
            Console.WriteLine(myComputer.Price);
            Console.WriteLine(myComputer.VideoCard);

        }
    }
}