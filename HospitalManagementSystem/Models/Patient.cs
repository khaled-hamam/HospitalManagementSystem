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
        public void setDiagnosis(string diagnosis)
        {
            this.diagnosis = diagnosis;
        }
        public string getDiagnosis()
        {
            return this.diagnosis;
        }
        public void setDoctors(List<Doctor> docs)
        {
            for(int i = 0; i < docs.Count; i++)
            {
                doctors.Add(docs[i]);
            }
        }
        public List<Doctor> getDoctors()
        {
            return this.doctors;
        }
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

