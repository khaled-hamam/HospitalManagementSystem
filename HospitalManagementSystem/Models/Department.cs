using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Department
    {
        private string name;
        private string headId;
        private Dictionary<string, Doctor> doctors;
        private Dictionary<string, Nurse> nurse;
        private Dictionary<string, Patient> patients;
        // getters & setters
        public string Name { get { return this.name; } set { this.name = value; } }
        public string HeadId { get { return this.headId; } set { this.headId = value; } }
        public Dictionary<string, Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        public Dictionary<string, Nurse> Nurse { get { return this.nurse; } set { this.nurse = value; } }
        public Dictionary<string, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        // constructors
        public Department()
        {
            this.Name = "";
            this.HeadId = "";
            this.Doctors = new Dictionary<string, Doctor>();
            this.Nurse = new Dictionary<string, Nurse>();
            this.Patients = new Dictionary<string, Patient>();
        }
        public void addDoctor(string id,Doctor doctor)
        {
            this.Doctors.Add(id, doctor);
        }
        public void removeDoctor(string doctorID)
        {
            this.Doctors.Remove(doctorID);
        }
        public void addNurse(string id, Nurse nurse)
        {
            this.Nurse.Add(id, nurse);
        }
        public void removeNurse(string nurseID)
        {
            this.Nurse.Remove(nurseID);
        }
        public void addPatient(string id, Patient patient)
        {
            this.Patients.Add(id,patient);
        }
        public void removePatient(string patientID)
        {
            this.Patients.Remove(patientID);
        }
           

    }
}
