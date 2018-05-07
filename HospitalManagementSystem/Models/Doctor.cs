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
        private Dictionary<String, Patient> patients;
        private Dictionary<String, Appointment> appointments;

        public bool IsHead { get { return this.isHead; } set { this.isHead = value;} }
        public Dictionary<String, Patient> Patients { get { return this.patients; } set { this.patients = value;} }
        public Dictionary<String, Appointment> Appointments { get { return this.appointments; } set { this.appointments = value;} }

        public Doctor()
        {
            this.IsHead = false;
            this.Patients = new Dictionary<String, Patient>();
            this.Appointments = new Dictionary<String, Appointment>();
        }

        public Doctor(bool ishead, double salary, Department department) : base(salary, department)
        {
            this.IsHead = ishead;
            this.Patients = new Dictionary<String, Patient>();
            this.Appointments = new Dictionary<String, Appointment>();
        }

        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient.ID, patient);
        }

        public void removePatient(String id)
        {
            this.Patients.Remove(id);
        }

        public void addAppointment(Appointment appointment)
        {
            this.Appointments.Add(appointment.ID, appointment);
        }

        public void removeAppointment(String id)
        {
            this.Appointments.Remove(id);
        }

        public bool isFree(Appointment newAppointment)
        {
            List<Appointment> apps = new List<Appointment>(Appointments.Values);
            apps.Add(newAppointment);

            DateTime curEnd = DateTime.MinValue;
            foreach (Appointment app in apps.OrderBy(app => app.Date))
            {
                if (app.Date < curEnd)
                    return false;

                curEnd = app.Date;
                curEnd.AddMinutes(app.Duration);
            }

            return true;
        }
    }
}
