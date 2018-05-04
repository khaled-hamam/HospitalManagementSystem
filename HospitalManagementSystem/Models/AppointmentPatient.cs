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
        private Dictionary<String, Appointment> appointments;
        public Dictionary<String, Appointment> Appointments { get { return this.appointments; } set { this.appointments = value; } }
        //constructors
        public AppointmentPatient() : base()
        {
            this.Appointments = new Dictionary<String, Appointment>();
        }
        public AppointmentPatient(String name, DateTime birthDate, String address, String diagnosis) : base( name, birthDate, address, diagnosis)
        {
            this.Appointments = new Dictionary<String, Appointment>();
        }
        // member methods
        public void addAppointment( Appointment appointment)
        {
            this.Appointments.Add(appointment.ID, appointment);
        }
        public void removeAppointment(String id)
        {
            this.Appointments.Remove(id);
        }
        public override double getBill()
        {
            int duration = 0;
            foreach(Appointment appointment in Hospital.Appointments.Values)
            {
                duration += appointment.Duration;
            }
            float bill = duration + (float)(duration * 0.5);
            return bill;
        }
    }
}