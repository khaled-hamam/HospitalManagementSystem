using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class AppointmentPatient : Patient
    {
        // member variables
        private List<Appointment> appointments;
        public List<Appointment> Appointments { get { return this.appointments; } set { this.appointments = value; } }
        //constructors
        public AppointmentPatient() : base()
        {
            this.Appointments = new List<Appointment>();
        }
        public AppointmentPatient(string name, DateTime birthDate, string address, string diagnosis) : base( name, birthDate, address, diagnosis)
        {
            this.Appointments = new List<Appointment>();
        }
        // member methods
        public void addAppointment(Appointment appointment)
        {
            this.Appointments.Add(appointment);
        }
        public void removeAppointment(string id)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (this.Appointments[i].Id == id)
                {
                    this.Appointments.Remove(appointments[i]);
                    return;
                }
            }
        }
        public override double getBill()
        {
            int duration = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                duration += this.Appointments[i].Duration;
            }
            float bill = duration + (float)(duration * 0.5);
            return bill;
        }
    }
}