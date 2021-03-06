using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShipManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Port workPort;
            // List<Port> = new List<Port>();
            for(;;)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-_-_-_-_-_-_-_-");
                Console.ResetColor();
                Console.WriteLine("Select the activity(by entering the number from the menu): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1.Enter another port.");
                Console.WriteLine("2.Show port register.");
                Console.WriteLine("3.Clear the register.");
                Console.WriteLine("4.Exit.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Available port numbers: 1, 3 - 20231 (not all numbers are real ports)");
                Console.WriteLine("Some of the Ship IMOs are 0, so some Ship might have IMO as 0");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-_-_-_-_-_-_-_-");
                Console.ResetColor();
                Console.ResetColor();
                string nextAc = Console.ReadLine();
                switch(nextAc.Trim())
                {
                    case "1": 
                        Scrape();
                            break;
                    case "2": 
                        PrintRegister();
                            break;
                    case "3": 
                        ClearRegister();
                            break;
                    default: 
                        Environment.Exit(0); 
                            break;
                }
            }
        }

        public static void PrintRegister()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var register = File.ReadAllText("pD.json");
            Root json = JsonConvert.DeserializeObject<Root>(register);
            foreach (var data in json.data)
            {
                Console.WriteLine("Port Name: {0}, Port Number: {1}, Country: {2}, Coordinates: {3}, ShipsOnPort: {4}, UnLocode: {5}, shipName: {6}, shipType: {7}, IMO: {8}", data.PortName, data.PortNumber, data.Country, data.Coordinates, data.Shipsonport, data.UnLocode, data.shipName, data.shipType, data.IMO);
            }
            Console.ResetColor();
        }
        public static void ClearRegister()
        {
            using (StreamWriter writer = new StreamWriter("pD.json"))
            {
                writer.WriteLine("~~~~");
            }
        }
        public static void Scrape()
        {
            Console.Write("Enter port number: ");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("Scraper.py cmd.exe");
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using(Process process = Process.Start(start))
            {
                using(StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                }
            }
        }
    }
}