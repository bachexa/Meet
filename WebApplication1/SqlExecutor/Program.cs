using System;
using System.IO;
using Microsoft.Data.SqlClient;

namespace SqlExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "workstation id=MeetDesk.mssql.somee.com;packet size=4096;user id=bachexa99_SQLLogin_1;pwd=6bwgtkgff2;data source=MeetDesk.mssql.somee.com;persist security info=False;initial catalog=MeetDesk;TrustServerCertificate=True";
            string filePath = @"d:\Meet\WebApplication1\WebApplication1\setup_database.sql";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {filePath}");
                return;
            }

            try
            {
                string script = File.ReadAllText(filePath);

                // Split script by GO command if necessary, though current script doesn't have it.
                // Safest to split by 'GO' on a new line just in case.
                string[] commands = script.Split(new string[] { "GO\r\n", "GO \r\n", "GO\n", "GO \n" }, StringSplitOptions.RemoveEmptyEntries);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to database successfully.");

                    foreach (var commandText in commands)
                    {
                        if (string.IsNullOrWhiteSpace(commandText)) continue;

                        using (SqlCommand command = new SqlCommand(commandText, connection))
                        {
                            command.CommandTimeout = 300; // 5 minutes timeout
                            try
                            {
                                int rows = command.ExecuteNonQuery();
                                Console.WriteLine("Executed command block successfully.");
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine($"SQL Error executing command: {ex.Message}");
                                Console.WriteLine($"Command: {commandText.Substring(0, Math.Min(commandText.Length, 100))}...");
                            }
                        }
                    }
                    Console.WriteLine("All commands processed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical Error: {ex.Message}");
            }
        }
    }
}
