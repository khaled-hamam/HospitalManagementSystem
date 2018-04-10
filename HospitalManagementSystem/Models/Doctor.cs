using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Doctor : Employee
    {
        private bool isHead;
        private Dictionary<string, Patient> patients;
        private Dictionary<string, Appointment> appointments;

        public bool IsHead { get { return this.isHead; } set { this.isHead = value;} }
        public Dictionary<string, Patient> Patients { get { return this.patients; } set { this.patients = value;} }
        public Dictionary<string, Appointment> Appointments { get { return this.appointments; } set { this.appointments = value;} }

        public Doctor()
        {
            this.IsHead = false;
            this.Patients = new Dictionary<string, Patient>();
            this.Appointments = new Dictionary<string, Appointment>();
        }

        public Doctor(bool ishead, double salary, Department department) : base(salary, department)
        {
            this.IsHead = ishead;
            this.Patients = new Dictionary<string, Patient>();
            this.Appointments = new Dictionary<string, Appointment>();
        }

        public void addPatient(string id, Patient patient)
        {
            this.Patients.Add(id, patient);
        }

        public void removePatient(string id)
        {
            this.Patients.Remove(id);
        }

        public void addAppointment(string id, Appointment appointment)
        {
            this.Appointments.Add(id, appointment);
        }

        public void removeAppointment(string id)
        {
            this.Appointments.Remove(id);
        }
    }
}
