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
        private List<Patient> patients;
        private List<Appointment> appointments;

        public bool IsHead { get { return this.isHead; } set { this.isHead = value;} }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value;} }
        public List<Appointment> Appointments { get { return this.appointments; } set { this.appointments = value;} }

        public Doctor()
        {
            this.IsHead = false;
            this.Patients = new List<Patient>();
            this.Appointments = new List<Appointment>();
        }

        public Doctor(bool ishead, double salary, Department department) : base(salary, department)
        {
            this.IsHead = ishead;
            this.Patients = new List<Patient>();
            this.Appointments = new List<Appointment>();
        }

        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient);
        }

        public void removePatient(string id)
        {
            foreach (Patient patient in patients)
            {
                if (patient.Id == id)
                {
                    this.Patients.Remove(patient);
                    break;
                }
            }
        }

        public void addAppointment(Appointment appointment)
        {
            this.Appointments.Add(appointment);
        }

        public void removeAppointment(string id)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Id == id)
                {
                    this.Appointments.Remove(appointment);
                    break;
                }
            }
        }
    }
}
