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
        private static Dictionary<String, Employee> employees;
        private static Dictionary<String, Patient> patients;
        private static Dictionary<String, Appointment> appointments;
        private static Dictionary<String, Department> departments;
        private static Dictionary<String, Room> rooms;

        public static Dictionary<String, Employee> Employees { get { return employees; } }
        public static Dictionary<String, Patient> Patients { get { return patients; } }
        public static Dictionary<String, Appointment> Appointments { get { return appointments; } }
        public static Dictionary<String, Department> Departments { get { return departments; } }
        public static Dictionary<String, Room> Rooms { get { return rooms; } }

        public Hospital()
        {
            employees = new Dictionary<String, Employee>();
            patients = new Dictionary<String, Patient>();
            appointments = new Dictionary<String, Appointment>();
            departments = new Dictionary<String, Department>();
            rooms = new Dictionary<String, Room>();
        }
    }
}
