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
        protected string diagnosis;
        protected Dictionary<string,Doctor> doctors;
        public string Diagnosis { get { return this.diagnosis; } set { this.diagnosis = value; } }
        public Dictionary<string, Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        // constructor
        public Patient() : base()
        {
            this.Diagnosis = "";
            this.Doctors = new Dictionary<string, Doctor>();
        }
        public Patient( string name, DateTime birthDate, string address, string diagnosis) : base(name, birthDate, address)
        {
            this.Diagnosis = diagnosis;
            this.Doctors = new Dictionary<string, Doctor>();
        }
        // member methods
        public void assignDoctor(string id,Doctor doctor)
        {
            this.Doctors.Add(id,doctor);
        }
        public void removeDoctor(string id)
        {
            this.Doctors.Remove(id);
        }
        public abstract double getBill();
    }
}

