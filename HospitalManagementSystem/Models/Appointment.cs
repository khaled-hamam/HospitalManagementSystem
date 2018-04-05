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
        private Patient patient { get; set; }
        private DateTime date { get; set; }
        private int duration { get; set; }
        // getters & setters
        public string Id { get { return this.id; } set { this.id = value; } }
        public Doctor Doctor { get { return this.doctor; } set { this.doctor = value; } }
        public Patient Patient { get { return this.patient; } set { this.patient = value; } }
        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public int Duration { get { return this.duration; } set { this.duration = value;} }
        // constructors
        public Appointment()
        {
            this.id = Guid.NewGuid().ToString();
            this.doctor = new Doctor();
            this.patient = new Patient();
            this.date = new DateTime();
            this.duration = 0;
        }

        public Appointment(Doctor doctor, Patient patient, DateTime date, int duration)
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
