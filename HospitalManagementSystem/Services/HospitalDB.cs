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
                String query = "SELECT * FROM room";
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

        public static List<Doctor> FetchDoctors()
        {
            MySqlConnection con = InitConnection();
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                con.Open();
                String query = "SELECT * FROM doctor";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    doctors.Add(new Doctor
                    {
                        ID = reader.GetString("doctor_id"),
                        Name = reader.GetString("name"),
                        // TODO: add BirthDate
                        Address = reader.GetString("address"),
                        // TODO: add EmploymentDate
                        // TODO: add Department
                        Salary = reader.GetFloat("salary"),
                        IsHead = reader.GetBoolean("is_head")
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Doctors.");
            }
            finally
            {
                con.Close();
            }

            return doctors;
        }

        public static List<Nurse> FetchNurses()
        {
            MySqlConnection con = InitConnection();
            List<Nurse> nurses = new List<Nurse>();
            try
            {
                con.Open();
                String query = "SELECT * FROM nurse";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nurses.Add(new Nurse
                    {
                        ID = reader.GetString("nurse_id"),
                        Name = reader.GetString("name"),
                        // TODO: add BirthDate
                        Address = reader.GetString("address"),
                        // TODO: add EmploymentDate
                        // TODO: add Department
                        Salary = reader.GetFloat("salary")
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Nurses.");
            }
            finally
            {
                con.Close();
            }

            return nurses;
        }

        public static Dictionary<String, Employee> FetchEmployees()
        {
            Dictionary<String, Employee> employees = new Dictionary<string, Employee>();

            List<Doctor> doctors = FetchDoctors();
            foreach (Doctor doctor in doctors)
            {
                employees.Add(doctor.ID, doctor);
            }

            List<Nurse> nurses = FetchNurses();
            foreach (Nurse nurse in nurses)
            {
                employees.Add(nurse.ID, nurse);
            }

            return employees;
        }

        public static List<ResidentPatient> FetchResidentPatients()
        {
            List<ResidentPatient> patients = new List<ResidentPatient>();
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = "SELECT * FROM residentPatients";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new ResidentPatient
                    {
                        ID = reader.GetString("patient_id"),
                        Name = reader.GetString("name"),
                        // TODO: add BirthDate
                        Address = reader.GetString("address"),
                        Diagnosis = reader.GetString("diagnosis"),
                        // TODO: add Room
                        // TODO: add Department
                        Duration = reader.GetInt32("duration")
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Resident Patients.");
            }
            finally
            {
                con.Close();
            }

            return patients;
        }

        public static List<AppointmentPatient> FetchAppointmentPatients()
        {
            List<AppointmentPatient> patients = new List<AppointmentPatient>();
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = "SELECT * FROM patient join appointment USING(patient_id)";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new AppointmentPatient
                    {
                        ID = reader.GetString("patient_id"),
                        Name = reader.GetString("name"),
                        // TODO: add BirthDate
                        Address = reader.GetString("address"),
                        Diagnosis = reader.GetString("diagnosis"),
                        // TODO: add Appointment
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Appointment Patients.");
            }
            finally
            {
                con.Close();
            }

            return patients;
        }

        public static List<Patient> FetchPatients()
        {
            List<Patient> patients = new List<Patient>();
            patients.AddRange(FetchResidentPatients());
            patients.AddRange(FetchAppointmentPatients());

            return patients;
        }

        public static List<Medicine> FetchMedicine()
        {
            MySqlConnection con = InitConnection();
            List<Medicine> medicine = new List<Medicine>();
            try
            {
                con.Open();
                String query = "SELECT * from medicine";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    medicine.Add(new Medicine
                    {
                        ID = reader.GetString("medicine_id"),
                        Name = reader.GetString("name"),
                        // TODO: add Starting Date
                        // TODO: add Ending Date
                        // TODO: add Patient
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Medicine.");
            }
            finally
            {
                con.Close();
            }

            return medicine;
        }

        public static List<Appointment> FetchAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = "SELECT * FROM patient join appointment USING(patient_id)";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    appointments.Add(new Appointment
                    {
                        ID = reader.GetString("appointment_id"),
                        // TODO: add Date
                        // TODO: add Patient
                        // TODO: add Doctor
                        Duration = reader.GetInt32("duration")
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Appointments.");
            }
            finally
            {
                con.Close();
            }

            return appointments;
        }
    }
}
