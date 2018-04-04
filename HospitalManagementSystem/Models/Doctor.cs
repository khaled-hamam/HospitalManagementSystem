using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Doctor : Employee
    {
        private bool isHead { get; set; }
        private List<Patient> patients { get; set; }
        private List<Appointment> appointments { get; set; }

        public Doctor()
        {
            this.isHead = false;
            patients = new List<Patient>();
            appointments = new List<Appointment>();
        }

        public Doctor(bool ishead, double salary, Department department) : base(salary, department)
        {
            this.isHead = ishead;
            patients = new List<Patient>();
            appointments = new List<Appointment>();
        }

        public void addPatient(patient)
        {
            patients.Add(patient);
        }

        public void removePatient(string id)
        {
            foreach (Patient patient in patients)
            {
                if (patient.id == id)
                {
                    patients.Remove(patient);
                    break;
                }
            }
        }

        public void addAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }

        public void removeAppointment(string id)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.id == id)
                {
                    appointments.Remove(appointment);
                    break;
                }
            }
        }
    }
