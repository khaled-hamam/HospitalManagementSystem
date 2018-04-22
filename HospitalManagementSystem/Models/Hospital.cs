using HospitalManagementSystem.Services;
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

        public Hospital()
        {
            Employees = new Dictionary<String, Employee>();
            Patients = new Dictionary<String, Patient>();
            Appointments = new Dictionary<String, Appointment>();
            Departments = new Dictionary<String, Department>();
            Rooms = new Dictionary<String, Room>();

            InitializeData();
        }

        public async void InitializeData()
        {
            InitializeDepartments();
            InitializeRooms();
            InitializeEmployees();
            InitializePatients();
            InitializeAppointments();
        }

        public void InitializeDepartments()
        {
            List<Department> departmentList = HospitalDB.FetchDepartments();
            foreach (Department department in departmentList)
            {
                Departments.Add(department.ID, department);
            }
        }

        public void InitializeRooms()
        {
            List<Room> roomList = HospitalDB.FetchRooms();
            foreach (Room room in roomList)
            {
                Rooms.Add(room.ID, room);
            }
        }

        public void InitializeEmployees()
        {
            List<Doctor> doctorList = HospitalDB.FetchDoctors();
            foreach (Doctor doctor in doctorList)
            {
                // Fetching Doctor's Department
                String departmentID = HospitalDB.FetchEmployeeDepartment(doctor.ID);

                // Assigning Doctor to his Department
                doctor.Department = Departments[departmentID];
                Departments[departmentID].addDoctor(doctor);

                // Checking if the Doctor is the Department's Head
                if (doctor.IsHead)
                    Departments[departmentID].HeadID = doctor.ID;

                Employees.Add(doctor.ID, doctor);
            }

            List<Nurse> nurseList = HospitalDB.FetchNurses();
            foreach (Nurse nurse in nurseList)
            {
                // Fetching Nurse's Department
                String departmentID = HospitalDB.FetchEmployeeDepartment(nurse.ID);

                // Assigning Nurse to her Department
                nurse.Department = Departments[departmentID];
                Departments[departmentID].addNurse(nurse);

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

        public void InitializePatients()
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
                    // Fetching Patient's Room from Database
                    String roomID = HospitalDB.FetchPatientRoom(patient.ID);
                    Rooms[roomID].addPatient(patient);
                    ((ResidentPatient)patient).Room = Rooms[roomID];

                    // Assigning Patients to Nurses in the Same Room
                    foreach (Nurse nurse in Rooms[roomID].Nurses.Values)
                    {
                        nurse.addPatient(patient);
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

        public void InitializeAppointments()
        {
            List<Appointment> appointmentList = HospitalDB.FetchAppointments();
            foreach (Appointment appointment in appointmentList)
            {
                // Fetching Appointment Patient
                String patientID = HospitalDB.FetchAppointmentPatient(appointment.ID);
                appointment.Patient = (AppointmentPatient)Patients[patientID];

                // Fetching Appointment Doctor
                String doctorID = HospitalDB.FetchAppointmentDoctor(appointment.ID);
                appointment.Doctor = (Doctor)Employees[doctorID];

                Appointments.Add(appointment.ID, appointment);
            }
        }
    }
}
