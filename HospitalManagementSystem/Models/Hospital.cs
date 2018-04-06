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
        private static List<Medicine> medicines;
        // setters & getters
        static List<Employee> Employees { get { return employees; } }
        static List<Patient> Patients { get { return patients; } }
        static List<Appointment> Appointments { get { return appointments; } }
        static List<Medicine> Medicines { get { return medicines; } }
        // constructors
        static Hospital()
        {
            employees = new List<Employee>();
            patients = new List<Patient>();
            appointments = new List<Appointment>();
            medicines = new List<Medicine>();
        }

    }
}
