using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Hospital
    {
        private List<Employee> employees;
        private List<Patient> patients;
        private List<Appointment> appointments;
        private List<Medicine> medicines;
        // setters & getters
        public List<Employee> Employees { get { return this.employees; } set { this.employees = value; } }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public List<Appointment> Appointments { get { return this.appointments; } set { this.appointments = value; } }
        public List<Medicine> Medicines { get { return this.medicines; } set { this.medicines = value; } }
        // constructors
        public Hospital()
        {

        }

    }
}
