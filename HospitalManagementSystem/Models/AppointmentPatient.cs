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
        private Dictionary<string, Appointment> appointments;
        public Dictionary<string, Appointment> Appointments { get { return this.appointments; } set { this.appointments = value; } }
        //constructors
        public AppointmentPatient() : base()
        {
            this.Appointments = new Dictionary<string, Appointment>();
        }
        public AppointmentPatient(string name, DateTime birthDate, string address, string diagnosis) : base( name, birthDate, address, diagnosis)
        {
            this.Appointments = new Dictionary<string, Appointment>();
        }
        // member methods
        public void addAppointment(string id, Appointment appointment)
        {
            this.Appointments.Add(id, appointment);
        }
        public void removeAppointment(string id)
        {
            this.Appointments.Remove(id);
        }
        public override double getBill()
        {
            int duration = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                string j = i.ToString();
                duration += this.Appointments[j].Duration;
            }
            float bill = duration + (float)(duration * 0.5);
            return bill;
        }
    }
}