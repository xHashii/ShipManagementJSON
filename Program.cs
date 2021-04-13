using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

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
            string cs = @"server=localhost;userid=root;password=shipmng;database=shipmngt";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM data";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetString(0)} {rdr.GetInt32(1)}  {rdr.GetString(2)} {rdr.GetString(3)} {rdr.GetInt32(4)} {rdr.GetString(5)} {rdr.GetString(6)} {rdr.GetString(7)} {rdr.GetInt32(8)}");
            }
            Console.ResetColor();
        }

        public static void ClearRegister()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string cs = @"server=localhost;userid=root;password=shipmng;database=shipmngt";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "TRUNCATE TABLE data";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
            }
            Console.WriteLine("All data has been cleared!");
            Console.ResetColor();
        }

        public static void Scrape()
        {
            Console.Write("Enter port number: ");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("scrapersql.py cmd.exe");
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