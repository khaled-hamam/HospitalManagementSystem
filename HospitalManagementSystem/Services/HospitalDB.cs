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

        #region Fetching Operations
        public static List<Department> FetchDepartments()
        {
            MySqlConnection con = InitConnection();
            List<Department> departments = new List<Department>();
            try
            {
                con.Open();
                String query = "SELECT * FROM department";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(
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

        public static List<Room> FetchRooms()
        {
            MySqlConnection con = InitConnection();
            List<Room> rooms = new List<Room>();
            try
            {
                con.Open();
                String query = "SELECT * FROM room";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String room_id = reader.GetString("room_id");
                    int number = reader.GetInt32("room_number");
                    String type = reader.GetString("type");
                    Room newRoom = null;
                    if (type == typeof(PrivateRoom).ToString())
                            newRoom = new PrivateRoom { ID = room_id, RoomNumber = number };
                    else if (type == typeof(SemiPrivateRoom).ToString())
                            newRoom = new SemiPrivateRoom { ID = room_id, RoomNumber = number };
                    else if (type == typeof(StandardWard).ToString())
                            newRoom = new StandardWard { ID = room_id, RoomNumber = number };

                    rooms.Add(newRoom);
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
                        BirthDate = reader.GetDateTime("birth_date"),
                        Address = reader.GetString("address"),
                        EmploymentDate = reader.GetDateTime("employment_date"),
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
                        BirthDate = reader.GetDateTime("birth_date"),
                        Address = reader.GetString("address"),
                        EmploymentDate = reader.GetDateTime("employment_date"),
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

        public static List<Employee> FetchEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.AddRange(FetchDoctors());
            employees.AddRange(FetchNurses());

            return employees;
        }

        public static String FetchPersoneDepartment(String personID)
        {
            MySqlConnection con = InitConnection();
            String departmentID = "";
            try
            {
                con.Open();
                String query = $"SELECT department_id FROM person_department WHERE person_id = '{personID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                departmentID = (String) command.ExecuteScalar();
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Department.");
            }
            finally
            {
                con.Close();
            }

            return departmentID;
        }

        public static List<String> FetchNurseRooms(String nurseID)
        {
            List<String> rooms = new List<String>();
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"SELECT room_id FROM nurse_room WHERE nurse_id = {nurseID}";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    rooms.Add(reader.GetString("room_id"));
                } 
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Department.");
            }
            finally
            {
                con.Close();
            }
            return rooms;
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
                        BirthDate = reader.GetDateTime("birth_date"),
                        Address = reader.GetString("address"),
                        Diagnosis = reader.GetString("diagnosis"),
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

        public static String FetchPatientRoom(String patientID)
        {
            MySqlConnection con = InitConnection();
            String roomID = "";
            try
            {
                con.Open();
                String query = $"SELECT room_id from resident_patient WHERE patient_id = {patientID}";
                MySqlCommand command = new MySqlCommand(query, con);
                roomID = (String)command.ExecuteScalar();
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Patient Room.");
            }
            finally
            {
                con.Close();
            }

            return roomID;
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
                        BirthDate = reader.GetDateTime("birth_date"),
                        Address = reader.GetString("address"),
                        Diagnosis = reader.GetString("diagnosis"),
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
                        StartingDate = reader.GetDateTime("starting_date"),
                        EndingDate = reader.GetDateTime("ending_date")
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

        public static List<Medicine> FetchMedicine(String patientID)
        {
            MySqlConnection con = InitConnection();
            List<Medicine> medicine = new List<Medicine>();
            try
            {
                con.Open();
                String query = $"SELECT * from medicine WHERE patient_id = {patientID}";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    medicine.Add(new Medicine
                    {
                        ID = reader.GetString("medicine_id"),
                        Name = reader.GetString("name"),
                        StartingDate = reader.GetDateTime("starting_date"),
                        EndingDate = reader.GetDateTime("ending_date")
                    });
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Patient Medicine.");
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
                        // Date = reader.GetDateTime("date"),
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

        public static String FetchAppointmentDoctor(String appointmentID)
        {
            MySqlConnection con = InitConnection();
            String doctorID = "";
            try
            {
                con.Open();
                String query = $"SELECT doctor_id from appointment WHERE appointment_id = {appointmentID}";
                MySqlCommand command = new MySqlCommand(query, con);
                doctorID = (String)command.ExecuteScalar();
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Appointment Doctor.");
            }
            finally
            {
                con.Close();
            }

            return doctorID;
        }

        public static String FetchAppointmentPatient(String appointmentID)
        {
            MySqlConnection con = InitConnection();
            String patientID = "";
            try
            {
                con.Open();
                String query = $"SELECT patient_id from appointment WHERE appointment_id = {appointmentID}";
                MySqlCommand command = new MySqlCommand(query, con);
                patientID = (String)command.ExecuteScalar();
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Appointment Doctor.");
            }
            finally
            {
                con.Close();
            }

            return patientID;
        }

        public static List<String> FetchPatientDoctors(String patientID)
        {
            MySqlConnection con = InitConnection();
            List<String> doctorsID = new List<String>();

            try
            {
                con.Open();
                String query = $"SELECT doctor_id from doctor_patient WHERE patient_id = {patientID}";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    doctorsID.Add(reader.GetString("doctor_id"));
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Patient Doctors.");
            }
            finally
            {
                con.Close();
            }

            return doctorsID;
        }
        #endregion

        #region Inserting Operations
        public async static void InsertDepartment(Department department)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO department VALUES('{department.ID}', '{department.Name}')";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Department.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertDoctor(Doctor doctor)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO doctor VALUES('{doctor.ID}', '{doctor.Name}', " +
                    $"'{doctor.BirthDate}', '{doctor.Address}', '{doctor.EmploymentDate}', '{doctor.Department.ID}'," +
                    $"{doctor.Salary}, {doctor.IsHead})";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Doctor.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertNurse(Nurse nurse)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO nurse VALUES('{nurse.ID}', '{nurse.Name}', " +
                    $"'{nurse.BirthDate}', '{nurse.Address}', '{nurse.EmploymentDate}', '{nurse.Department.ID}'," +
                    $"{nurse.Salary})";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Nurse.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertPatient(Patient patient)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO patient VALUES('{patient.ID}', '{patient.Name}', " +
                    $"'{patient.BirthDate}', '{patient.Address}', '{patient.Diagnosis}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();

                // TODO: inserting Special data if (Resident / Appointment)
            }
            catch
            {
                Console.WriteLine("Error Inserting Patient.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertRoom(Room room)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO room VALUES('{room.ID}', {room.RoomNumber}, '{room.GetType()}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Room.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertAppointment(Appointment appointment)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO appointment VALUES('{appointment.ID}', '{appointment.Patient.ID}', " +
                    $"'{appointment.Doctor.ID}', '{appointment.Date}', {appointment.Duration}";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Appointment.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertMedicine(Medicine medicine, Patient patient)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO medicine VALUES('{medicine.ID}', '{medicine.Name}', " +
                    $"'{medicine.StartingDate}', '{medicine.EndingDate}', '{patient.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Medicine.");
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}
