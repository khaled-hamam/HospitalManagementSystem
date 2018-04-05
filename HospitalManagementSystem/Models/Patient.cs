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
        protected List<Doctor> doctors;
        public string Diagnosis { get { return this.diagnosis; } set { this.diagnosis = value; } }
        public List<Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        // constructor
        public Patient() : base()
        {
            this.Diagnosis = "";
            this.Doctors = new List<Doctor>();
        }
        public Patient( string name, DateTime birthDate, string address, string diagnosis) : base(name, birthDate, address)
        {
            this.Diagnosis = diagnosis;
            this.Doctors = new List<Doctor>();
        }
        // member methods
        public void assignDoctor(Doctor doctor)
        {
            this.Doctors.Add(doctor);
        }
        public void removeDoctor(string id)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Id == id)
                {
                    this.doctors.Remove(doctors[i]);
                    return;
                }
            }
        }
        public abstract double getBill();
    }
}

