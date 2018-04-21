﻿using HospitalManagementSystem.Services;
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
            Console.WriteLine(typeof(PrivateRoom).ToString());
            Employees = new Dictionary<String, Employee>();
            Patients = new Dictionary<String, Patient>();
            Appointments = new Dictionary<String, Appointment>();
            Departments = new Dictionary<String, Department>();
            Rooms = new Dictionary<String, Room>();
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
                String departmentID = HospitalDB.FetchEmployeeDepartment(doctor.ID);
                doctor.Department = Departments[departmentID];
                Departments[departmentID].addDoctor(doctor);
                if (doctor.IsHead)
                    Departments[departmentID].HeadID = doctor.ID;

                Employees.Add(doctor.ID, doctor);
            }

            List<Nurse> nurseList = HospitalDB.FetchNurses();
            foreach (Nurse nurse in nurseList)
            {
                String departmentID = HospitalDB.FetchEmployeeDepartment(nurse.ID);
                List<String> roomsID = HospitalDB.FetchNurseRooms(nurse.ID);
                nurse.Department = Departments[departmentID];
                Departments[departmentID].addNurse(nurse);
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
                if (patient.GetType() == typeof(ResidentPatient))
                {
                    // Room

                    // Nurses
                }
                else if (patient.GetType() == typeof(AppointmentPatient))
                {
                    // Appointment
                }
                Patients.Add(patient.ID, patient);
            }
        }
    }
}
