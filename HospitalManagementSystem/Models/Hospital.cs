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
        private static ObservableCollection<Employee> employees;
        private static ObservableCollection<Patient> patients;
        private static ObservableCollection<Appointment> appointments;
        private static ObservableCollection<Department> departments;
        private static ObservableCollection<Room> rooms;

        public static ObservableCollection<Employee> Employees { get { return employees; } }
        public static ObservableCollection<Patient> Patients { get { return patients; } }
        public static ObservableCollection<Appointment> Appointments { get { return appointments; } }
        public static ObservableCollection<Department> Departments { get { return departments; } }
        public static ObservableCollection<Room> Rooms { get { return rooms; } }

        public Hospital()
        {
            employees = new ObservableCollection<Employee>();
            patients = new ObservableCollection<Patient>();
            appointments = new ObservableCollection<Appointment>();
            departments = new ObservableCollection<Department>();
            rooms = new ObservableCollection<Room>();
        }
    }
}
