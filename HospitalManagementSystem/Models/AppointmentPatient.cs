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
        //constructors
        public AppointmentPatient() : base()
        {
            this.appointments = new List<Appointment>();
        }
        public AppointmentPatient(string id, string name, DateTime birthDate, string address, string diagnosis) : base(id, name, birthDate, address, diagnosis)
        {
            this.appointments = new List<Appointment>();
        }
        // member methods
        public void setAppointments(List<Appointment> listAppointment)
        {
            this.appointments = listAppointment;
        }
        public List<Appointment> getAppointments()
        {
            return this.appointments;
        }
        public void addAppointment(Appointment apm)
        {
            this.appointments.Add(apm);
        }
        public void removeAppointment(string id)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (this.appointments[i].getId() == id)
                {
                    this.appointments.Remove(appointments[i]);
                    return;
                }
            }
        }
        public override double getBill()
        {
            int duration = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                duration += this.appointments[i].getDuration();
            }
            float bill = duration + (float)(duration * 0.5);
            return bill;
        }
    }
}