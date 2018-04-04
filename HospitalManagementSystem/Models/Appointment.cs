using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Appointment
    {
        private string id { get; set; }
        private Doctor doctor { get; set; }
        private AppointmentPatient patient { get; set; }
        private DateTime date { get; set; }
        private int duration { get; set; }

        public Appointment()
        {
            this.id = Guid.NewGuid().ToString();
            this.doctor = new Doctor();
            this.patient = new AppointmentPatient();
            this.date = new DateTime();
            this.duration = 0;
        }

        public Appointment(Doctor doctor, AppointmentPatient patient, DateTime date, int duration)
        {
            this.id = Guid.NewGuid().ToString();
            this.doctor = doctor;
            this.patient = patient;
            this.date = date;
            this.duration = duration;
        }

        public void cancel()
        {
            this.doctor.removeAppointment(this.id);
            this.patient.removeAppointment(this.id);
        }
    }
}
