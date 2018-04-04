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
        protected string diagnosis { get; set; }
        protected List<Doctor> doctors { get; set; }
        // constructor
        public Patient() : base()
        {
            this.diagnosis = "";
            doctors = new List<Doctor>();
        }
        public Patient(string id, string name, DateTime dt, string address, string diagnosis) : base(id, name, dt, address)
        {
            this.diagnosis = diagnosis;
            doctors = new List<Doctor>();

        }
        // member methods
        public void assignDoctor(Doctor doctor)
        {
            doctors.Add(doctor);
        }
        public void removeDoctor(string id)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].getId() == id)
                {
                    doctors.Remove(doctors[i]);
                    return;
                }
            }
        }
        public abstract float getBill();
    }
}

