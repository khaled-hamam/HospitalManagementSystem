using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public static class Hospital
    {
        private static List<Employee> employees;
        private static List<Patient> patients;
        private static List<Appointment> appointments;
        private static List<Department> departments;
        private static List<Room> rooms;
        // setters & getters
        static List<Employee> Employees { get { return employees; } }
        static List<Patient> Patients { get { return patients; } }
        static List<Appointment> Appointments { get { return appointments; } }
        static List<Department> Departments { get { return departments; } }
        static List<Room> Rooms { get { return rooms; } }

        // constructors
        static Hospital()
        {
            employees = new List<Employee>();
            patients = new List<Patient>();
            appointments = new List<Appointment>();
            departments = new List<Department>();
            rooms = new List<Room>();
        }

    }
}
