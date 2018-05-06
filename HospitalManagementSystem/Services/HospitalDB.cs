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
        public static Config FetchConfig()
        {
            MySqlConnection con = InitConnection();
            Config Config = new Config
            {
                StandardWardCapacity = 4,
                StandardWardPrice = 50,
                SemiPrivateRoomCapacity = 2,
                SemiPrivateRoomPrice = 90,
                PrivateRoomCapacity = 1,
                PrivateRoomPrice = 150,
                AppointmentHourPrice = 40
            };

            try
            {
                con.Open();
                String query = "SELECT * FROM config";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Config = new Config
                    {
                        StandardWardPrice = reader.GetFloat("standard_price"),
                        SemiPrivateRoomPrice = reader.GetFloat("semi_price"),
                        PrivateRoomPrice = reader.GetFloat("private_price"),
                        AppointmentHourPrice = reader.GetFloat("appointment_price"),
                        StandardWardCapacity = reader.GetInt32("standard_capacity"),
                        SemiPrivateRoomCapacity = reader.GetInt32("semi_capacity"),
                        PrivateRoomCapacity = reader.GetInt32("private_capacity")
                    };
                }
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Config.");
            }
            finally
            {
                con.Close();
            }

            return Config;
        }

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

        public static String FetchPersonDepartment(String personID)
        {
            MySqlConnection con = InitConnection();
            String departmentID = "";
            try
            {
                con.Open();
                String query = $"SELECT department_id FROM person_department WHERE person_id = '{personID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                object result = command.ExecuteScalar();
                if (result is String)
                    departmentID = (String)result;
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
                String query = $"SELECT room_id FROM nurse_room WHERE nurse_id = '{nurseID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    rooms.Add(reader.GetString("room_id"));
                } 
            }
            catch
            {
                Console.WriteLine("Error Occured Fetching Nurse Room.");
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
                String query = $"SELECT room_id from resident_patient WHERE patient_id = '{patientID}'";
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
                String query = $"SELECT * from medicine WHERE patient_id = '{patientID}'";
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
                        Date = reader.GetDateTime("date"),
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
                String query = $"SELECT doctor_id from doctor_patient WHERE patient_id = '{patientID}'";
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
                    $"'{doctor.BirthDate.ToString("yyyy-MM-dd")}', '{doctor.Address}', '{doctor.EmploymentDate.ToString("yyyy-MM-dd")}', '{doctor.Department.ID}'," +
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
                    $"'{nurse.BirthDate.ToString("yyyy-MM-dd")}', '{nurse.Address}', '{nurse.EmploymentDate.ToString("yyyy-MM-dd")}', '{nurse.Department.ID}'," +
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
                    $"'{patient.BirthDate.ToString("yyyy-MM-dd")}', '{patient.Address}', '{patient.Diagnosis}')";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();

                if (patient.GetType() == typeof(ResidentPatient))
                {
                    query = $"INSERT INTO resident_patient VALUES('{patient.ID}', '{((ResidentPatient)patient).Room.ID}', " +
                        $"'{((ResidentPatient)patient).Department.ID}', 0)";
                    command = new MySqlCommand(query, con);
                    await command.ExecuteNonQueryAsync();
                }
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
                String query = $"INSERT INTO room VALUES('{room.ID}', {room.RoomNumber}, '{room.GetType()}')";
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
                    $"'{appointment.Doctor.ID}', '{appointment.Date.ToString("yyyy-MM-dd")}', {appointment.Duration}";
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
                    $"'{medicine.StartingDate.ToString("yyyy-MM-dd")}', '{medicine.EndingDate.ToString("yyyy-MM-dd")}', '{patient.ID}'";
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

        public async static void InsertNurseRoom(String NurseID, String RoomID)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO nurse_room VALUES('{NurseID}', '{RoomID}')";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Nurse Room.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void InsertDoctorPatient(String DoctorID, String PatientID)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"INSERT INTO doctor_patient VALUES('{DoctorID}', '{PatientID}')";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Inserting Doctor Patient.");
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region Updating Operations

        public async static void UpdateConfig(Config config)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE config SET standard_price = {config.StandardWardPrice}, semi_price = {config.SemiPrivateRoomPrice}, " +
                    $"private_price = {config.PrivateRoomPrice}, appointment_price = {config.AppointmentHourPrice}, " +
                    $"standard_capacity = {config.StandardWardCapacity}, semi_capacity = {config.SemiPrivateRoomCapacity}, " +
                    $"private_capacity = {config.PrivateRoomCapacity} " +
                    $"WHERE id = 1";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Updating Config.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdateDepartment(Department department)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE department SET name = '{department.Name}' WHERE department_id = '{department.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch 
            {
                Console.WriteLine("Error Updating Department.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdateDoctor(Doctor doctor)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE doctor SET name = '{doctor.Name}', birth_date = '{doctor.BirthDate.ToString("yyyy-MM-dd")}', " +
                    $"address = '{doctor.Address}', employment_date = '{doctor.EmploymentDate.ToString("yyyy-MM-dd")}', " +
                    $"department_id = '{doctor.Department.ID}', salary = {doctor.Salary}, is_head = {doctor.IsHead} " +
                    $"WHERE doctor_id = '{doctor.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Updating Doctor.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdateNurse(Nurse nurse)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE nurse SET name = '{nurse.Name}', birth_date = '{nurse.BirthDate.ToString("yyyy-MM-dd")}', " +
                    $"address = '{nurse.Address}', employment_date = '{nurse.EmploymentDate.ToString("yyyy-MM-dd")}', " +
                    $"department_id = '{nurse.Department.ID}', salary = {nurse.Salary} " +
                    $"WHERE nurse_id = '{nurse.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Updating Nurse.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdateAppointment(Appointment appointment)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE appointment SET patient_id = '{appointment.Patient.ID}', doctor_id = '{appointment.Doctor.ID}', " +
                    $"date = '{appointment.Date.ToString("yyyy-MM-dd")}', duration = {appointment.Duration}, " +
                    $"WHERE appointment_id = '{appointment.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Updating Appointment.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdatePatient(Patient patient)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE patient SET name = '{patient.Name}', birth_date = '{patient.BirthDate.ToString("yyyy-MM-dd")}', " +
                    $"address = '{patient.Address}', diagnosis = '{patient.Diagnosis}' " +
                    $"WHERE patient_id = '{patient.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();

                if (patient.GetType() == typeof(ResidentPatient))
                {
                    query = $"UPDATE resident_patient SET room_id = '{((ResidentPatient)patient).Room.ID}', " +
                        $"department_id = '{((ResidentPatient)patient).Department.ID}' " +
                        $"WHERE patient_id = '{patient.ID}'";
                    command = new MySqlCommand(query, con);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                Console.WriteLine("Error Updating Patient.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void UpdateRoom(Room room)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"UPDATE room SET room_number = {room.RoomNumber}, type = '{room.GetType()}' " +
                    $"WHERE room_id = '{room.ID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Updating Room.");
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region Deleting Operations

        public async static void DeleteDepartment(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM department WHERE department_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Department.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteDoctor(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM doctor WHERE doctor_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Doctor.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteNurse(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM nurse WHERE nurse_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Nurse.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeletePatient(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM patient WHERE patient_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Patient.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteAppointment(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM appointment WHERE appointment_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Appointment.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteRoom(String id)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM room WHERE room_id = '{id}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Room.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteNurseRoom(String NurseID, String RoomID)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM nurse_room WHERE nurse_id = '{NurseID}' AND room_id = '{RoomID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Nurse Room.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteDoctorPatient(String DoctorID, String PatientID)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM doctor_patient WHERE doctor_id = '{DoctorID}' AND patient_id = '{PatientID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Doctor Patient.");
            }
            finally
            {
                con.Close();
            }
        }

        public async static void DeleteMedicine(String MedicineID)
        {
            MySqlConnection con = InitConnection();

            try
            {
                con.Open();
                String query = $"DELETE FROM medicine WHERE medicine_id = '{MedicineID}'";
                MySqlCommand command = new MySqlCommand(query, con);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("Error Deleting Medicine.");
            }
            finally
            {
                con.Close();
            }
        }

        #endregion
    }
}
