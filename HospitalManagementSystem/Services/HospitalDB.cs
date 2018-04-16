using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace HospitalManagementSystem.Services
{
    public static class HospitalDB
    {
        public static MySqlConnection initConnection()
        {
            String connectionString = $"server={Credentials.Server};" +
                $"user id={Credentials.UserID};" +
                $"password={Credentials.Password};" +
                $"database={Credentials.Database}";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                return con;
            }
        }
    }
}
