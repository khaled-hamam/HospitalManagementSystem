using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    abstract class Patient : Person
    {
        // member variables
        protected String diagnosis;
        protected Dictionary<String, Doctor> doctors;
        public String Diagnosis { get { return this.diagnosis; } set { this.diagnosis = value; } }
        public Dictionary<String, Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        // constructor
        public Patient() : base()
        {
            this.Diagnosis = "";
            this.Doctors = new Dictionary<String, Doctor>();
        }
        public Patient( string name, DateTime birthDate, String address, String diagnosis) : base(name, birthDate, address)
        {
            this.Diagnosis = diagnosis;
            this.Doctors = new Dictionary<String, Doctor>();
        }
        // member methods
        public void assignDoctor(Doctor doctor)
        {
            this.Doctors.Add(doctor.ID,doctor);
        }
        public void removeDoctor(String id)
        {
            this.Doctors.Remove(id);
        }
        public abstract double getBill();
    }
}

