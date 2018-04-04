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
        private List<Appointment> appointments { get; set; }
        //constructors
        public AppointmentPatient() : base()
        {
            appointments = new List<Appointment>();
        }
        public AppointmentPatient(string id, string name, DateTime dt, string address, string diagnosis) : base(id, name, dt, address, diagnosis)
        {
            appointments = new List<Appointment>();
        }
        // member methods
        public void addAppointment(Appointment apm)
        {
            appointments.Add(apm);
        }
        public void removeAppointment(string id)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].id == id)
                {
                    appointments.Remove(appointments[i]);
                    return;
                }
            }
        }
        public override float getBill()
        {
            int duration = 0;
            for (int i = 0; i < appointments.Count; i++)
            {
                duration += appointments[i].duration;
            }
            float bill = duration + (float)(duration * 0.5);
            return bill;
        }
    }
}