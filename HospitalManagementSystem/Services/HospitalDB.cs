using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    static class HospitalDB
    {
        public static MySqlConnection InitConnection()
        {
            String connectionString = $"server={Credentials.Server};" +
                $"user id={Credentials.UserID};" +
                $"password={Credentials.Password};" +
                $"database={Credentials.Database}";

            return new MySqlConnection(connectionString);
        }

        public static Dictionary<String, Department> FetchDepartments()
        {
            MySqlConnection con = InitConnection();
            Dictionary<String, Department> departments = new Dictionary<String, Department>();
            try
            {
                con.Open();
                String query = "SELECT * FROM department";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(reader.GetString("department_id"), 
                        new Department
                        {
                            ID = reader.GetString("department_id"),
                            Name = reader.GetString("name")
                        }
                    );
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Departments.");
            }
            finally
            {
                con.Close();
            }

            return departments;
        }

        public static Dictionary<String, Room> FetchRooms()
        {
            MySqlConnection con = InitConnection();
            Dictionary<String, Room> rooms = new Dictionary<String, Room>();
            try
            {
                con.Open();
                String query = "SELECT * FROM department";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String room_id = reader.GetString("room_id");
                    int capacity = reader.GetInt16("capacity");
                    float price = reader.GetFloat("price");
                    Room newRoom = null;
                    switch (capacity)
                    {
                        case 1:
                            newRoom = new PrivateRoom { ID = room_id, Capacity = capacity, Price = price };
                            break;
                        case 2:
                            newRoom = new SemiPrivateRoom { ID = room_id, Capacity = capacity, Price = price };
                            break;
                        case 4:
                            newRoom = new StandardWard { ID = room_id, Capacity = capacity, Price = price };
                            break;
                    }

                    rooms.Add(newRoom.ID, newRoom);
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Rooms.");
            }
            finally
            {
                con.Close();
            }

            return rooms;
        }
    }
}
