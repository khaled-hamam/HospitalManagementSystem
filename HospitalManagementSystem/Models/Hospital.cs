using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Hospital
    {
        public static Dictionary<String, Employee> Employees { get; private set; }
        public static Dictionary<String, Patient> Patients { get; private set; }
        public static Dictionary<String, Appointment> Appointments { get; private set; }
        public static Dictionary<String, Department> Departments { get; private set; }
        public static Dictionary<String, Room> Rooms { get; private set; }

        public static Config Config { get; set; }

        public Hospital()
        {
            Employees = new Dictionary<String, Employee>();
            Patients = new Dictionary<String, Patient>();
            Appointments = new Dictionary<String, Appointment>();
            Departments = new Dictionary<String, Department>();
            Rooms = new Dictionary<String, Room>();
            Config = new Config
            {
                StandardWardCapacity = 4,
                StandardWardPrice = 50,
                SemiPrivateRoomCapacity = 2,
                SemiPrivateRoomPrice = 90,
                PrivateRoomCapacity = 1,
                PrivateRoomPrice = 150,
                AppointmentHourPrice = 40
            };
        }

        public static async void InitializeData()
        {
            Home.ViewModel.IsLoading = true;
            await Task.Run(() =>
            {
                Config = HospitalDB.FetchConfig();
                InitializeDepartments();
                InitializeRooms();
                InitializeEmployees();
                InitializePatients();
                InitializeAppointments();
            });
            Home.ViewModel.IsLoading = false;
        }

        public static void InitializeDepartments()
        {
            List<Department> departmentList = HospitalDB.FetchDepartments();
            foreach (Department department in departmentList)
            {
                if (department != null)
                    Departments.Add(department.ID, department);
            }
        }

        public static void InitializeRooms()
        {
            List<Room> roomList = HospitalDB.FetchRooms();
            foreach (Room room in roomList)
            {
                Rooms.Add(room.ID, room);
            }
        }

        public static void InitializeEmployees()
        {
            List<Doctor> doctorList = HospitalDB.FetchDoctors();
            foreach (Doctor doctor in doctorList)
            {
                // Fetching Doctor's Department
                String departmentID = HospitalDB.FetchPersonDepartment(doctor.ID);

                // Assigning Doctor to his Department
                if (Departments.ContainsKey(departmentID))
                {
                    doctor.Department = Departments[departmentID];
                    Departments[departmentID].addDoctor(doctor);

                    // Checking if the Doctor is the Department's Head
                    if (doctor.IsHead)
                        Departments[departmentID].HeadID = doctor.ID;
                }


                Employees.Add(doctor.ID, doctor);
            }

            List<Nurse> nurseList = HospitalDB.FetchNurses();
            foreach (Nurse nurse in nurseList)
            {
                // Fetching Nurse's Department
                String departmentID = HospitalDB.FetchPersonDepartment(nurse.ID);

                // Assigning Nurse to her Department
                if (Departments.ContainsKey(departmentID))
                {
                    nurse.Department = Departments[departmentID];
                    Departments[departmentID].addNurse(nurse);
                }

                // Fetching Nurse's Rooms
                List<String> roomsID = HospitalDB.FetchNurseRooms(nurse.ID);

                // Assigning Nurses to their Rooms
                foreach (String roomID in roomsID)
                {
                    nurse.addRoom(Rooms[roomID]);
                    Rooms[roomID].addNurse(nurse);
                }

                Employees.Add(nurse.ID, nurse);
            }
        }

        public static void InitializePatients()
        {
            List<Patient> patientList = HospitalDB.FetchPatients();
            foreach (Patient patient in patientList)
            {
                // Assigning Patient's Doctors
                List<String> doctorsIDs = HospitalDB.FetchPatientDoctors(patient.ID);
                foreach (String doctorID in doctorsIDs)
                {
                    patient.assignDoctor((Doctor)Employees[doctorID]);
                    ((Doctor)Employees[doctorID]).addPatient(patient);
                }

                if (patient.GetType() == typeof(ResidentPatient))
                {
                    // Fetching Patient's Department
                    String departmentID = HospitalDB.FetchPersonDepartment(patient.ID);
                    if (Departments.ContainsKey(departmentID))
                    {
                        ((ResidentPatient)patient).Department = Departments[departmentID];
                        Departments[departmentID].Patients.Add(patient.ID, patient);
                    }

                    // Fetching Patient's Room from Database
                    String roomID = HospitalDB.FetchPatientRoom(patient.ID);

                    if (Rooms.ContainsKey(roomID))
                    {
                        Rooms[roomID].addPatient(patient);
                        ((ResidentPatient)patient).Room = Rooms[roomID];

                        // Assigning Patients to Nurses in the Same Room
                        foreach (Nurse nurse in Rooms[roomID].Nurses.Values)
                        {
                            nurse.addPatient(patient);
                        }
                    }


                    // Fetching Patient's Medicine from Database
                    List<Medicine> medicineList = HospitalDB.FetchMedicine(patient.ID);
                    foreach (Medicine medicine in medicineList)
                    {
                        ((ResidentPatient)patient).addMedicine(new Medicine
                        {
                            ID = medicine.ID,
                            Name = medicine.Name,
                            StartingDate = medicine.StartingDate,
                            EndingDate = medicine.EndingDate
                        });
                    }
                }

                Patients.Add(patient.ID, patient);
            }
        }

        public static void InitializeAppointments()
        {
            List<Appointment> appointmentList = HospitalDB.FetchAppointments();
            foreach (Appointment appointment in appointmentList)
            {
                // Fetching Appointment Patient
                String patientID = HospitalDB.FetchAppointmentPatient(appointment.ID);
                appointment.Patient = (AppointmentPatient)Patients[patientID];
                ((AppointmentPatient)Patients[patientID]).addAppointment(appointment);

                // Fetching Appointment Doctor
                String doctorID = HospitalDB.FetchAppointmentDoctor(appointment.ID);
                appointment.Doctor = (Doctor)Employees[doctorID];
                ((Doctor)Employees[doctorID]).addAppointment(appointment);

                // Adding Doctor, Patient Relations
                if (((Doctor)Employees[doctorID]).Patients.ContainsKey(patientID) == false)
                    ((Doctor)Employees[doctorID]).addPatient(Patients[patientID]);

                if (((AppointmentPatient)Patients[patientID]).Doctors.ContainsKey(doctorID) == false)
                    Patients[patientID].assignDoctor((Doctor)Employees[doctorID]);

                Appointments.Add(appointment.ID, appointment);
            }
        }

        public static void DeleteDoctor(String DoctorId)
        {
            foreach (Appointment appointment in ((Doctor)Employees[DoctorId]).Appointments.Values)
            {
                appointment.cancel();
            }
            foreach (Patient patient in ((Doctor)Employees[DoctorId]).Patients.Values)
            {
                patient.removeDoctor(DoctorId);
            }
            Hospital.Employees[DoctorId].Department.removeDoctor(DoctorId);
            Employees.Remove(DoctorId);
        }

        public static void DeleteNurse(String NurseId)
        {
            foreach (Room room in ((Nurse)Employees[NurseId]).Rooms.Values)
            {
                room.removeNurse(NurseId);
            }
            Employees.Remove(NurseId);
        }

        public static void DeleteRoom(String RoomId)
        {
            foreach(Nurse nurse in Rooms[RoomId].Nurses.Values)
            {
                nurse.removeRoom(RoomId);
            }
            foreach (Patient patient in Rooms[RoomId].Patients.Values)
            {
                if (patient.GetType() == typeof(ResidentPatient))
                {
                    ((ResidentPatient)patient).Room = null;
                }
            }

            Rooms.Remove(RoomId);
        }

        public static void DeletePatient(String PatientId)
        {
            foreach(Doctor doctor in Patients[PatientId].Doctors.Values)
            {
                doctor.removePatient(PatientId);
            }
            if(Patients[PatientId].GetType()==typeof(AppointmentPatient))
            {
                List<Appointment> appointments = new List<Appointment>(((AppointmentPatient)Patients[PatientId]).Appointments.Values);
                foreach(Appointment appointment in appointments)
                {
                    appointment.cancel();
                }
            }
            else
            {
                ((ResidentPatient)Patients[PatientId]).Room.Patients.Remove(PatientId);                
            }

            Patients.Remove(PatientId);
        }

        public static void DeleteDepartment(String DepartmentId)
        {
            foreach (Patient patient in Departments[DepartmentId].Patients.Values)
            {
                if (patient.GetType() == typeof(ResidentPatient))
                {
                    ((ResidentPatient)patient).Department = null;
                }
            }

            foreach(Doctor doctor in Departments[DepartmentId].Doctors.Values)
            {
                doctor.Department = null;
            }

            foreach(Nurse nurse in Departments[DepartmentId].Nurse.Values)
            {
                nurse.Department = null;
            }

            Departments.Remove(DepartmentId);
        }

       public static void DeleteAppointment(String AppointmentId)
       {
            Appointments[AppointmentId].cancel();
            Appointments.Remove(AppointmentId);
       }
    }

    class Config
    {
        public double StandardWardPrice { get; set; }
        public double SemiPrivateRoomPrice { get; set; }
        public double PrivateRoomPrice { get; set; }

        public int StandardWardCapacity { get; set; }
        public int SemiPrivateRoomCapacity { get; set; }
        public int PrivateRoomCapacity { get; set; }

        public double AppointmentHourPrice { get; set; }

        public Config()
        {

        }
    }
}
