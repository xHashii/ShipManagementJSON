using System;
using MySql.Data.MySqlClient;

class SQL {

    public static void AddSQL() 
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
            Console.WriteLine($"{rdr.GetString(0)} {rdr.GetInt32(1)} {rdr.GetString(2)} {rdr.GetString(3)} {rdr.GetInt32(4)} {rdr.GetString(5)} {rdr.GetString(6)} {rdr.GetString(7)} {rdr.GetInt32(8)}");
        }
    }

    public static void ClearSQL()
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
    }
}