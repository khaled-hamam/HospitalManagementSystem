using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public static class Hospital
    {
        private static Dictionary<string, Employee> employees;
        private static Dictionary<string, Patient> patients;
        private static Dictionary<string, Appointment> appointments;
        private static Dictionary<string, Department> departments;
        private static Dictionary<string, Room> rooms;
        // setters & getters
        static Dictionary<string, Employee> Employees { get { return employees; } }
        static Dictionary<string, Patient> Patients { get { return patients; } }
        static Dictionary<string, Appointment> Appointments { get { return appointments; } }
        static Dictionary<string, Department> Departments { get { return departments; } }
        static Dictionary<string, Room> Rooms { get { return rooms; } }

        // constructors
        static Hospital()
        {
            employees = new Dictionary<string, Employee>();
            patients = new Dictionary<string, Patient>();
            appointments = new Dictionary<string, Appointment>();
            departments = new Dictionary<string, Department>();
            rooms = new Dictionary<string, Room>();
        }

    }
}
