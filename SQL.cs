using System;
using MySql.Data.MySqlClient;

class SQL {

    static string cs = @"server=localhost;userid=root;password=shipmng;database=shipmngt";
    public static void AddSQL() 
    {
        try 
        {
            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM data";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine($"Port Name: {rdr.GetString(0)}\nPort Number: {rdr.GetInt32(1)}\nCountry:{rdr.GetString(2)}\nCoordinates: {rdr.GetString(3)}\nShips on port: {rdr.GetInt32(4)}\nUnlocode: {rdr.GetString(5)}\nShip Name: {rdr.GetString(6)}\nShip Type: {rdr.GetString(7)}\nShip IMO: {rdr.GetInt32(8)}");
                Console.WriteLine("---------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while selecting data from database!\nDetails: " + ex);
        }
    }

    public static void ClearSQL()
    {
        try
        {
            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "TRUNCATE TABLE data";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
            }
        }
        catch (Exception dx)
        {
            Console.WriteLine("Error while trying to clean data table!\nDetails: " + dx);
        }
    }
}