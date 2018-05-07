using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    class Department
    {
        private String id;
        private String name;
        private String headID;
        private Dictionary<string, Doctor> doctors;
        private Dictionary<string, Nurse> nurse;
        private Dictionary<string, Patient> patients;

        public String ID { get { return this.id; } set { this.id = value; } }
        public String Name { get { return this.name; } set { this.name = value; } }
        public String HeadID { get { return this.headID; } set { this.headID = value; } }
        public Dictionary<String, Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        public Dictionary<String, Nurse> Nurse { get { return this.nurse; } set { this.nurse = value; } }
        public Dictionary<String, Patient> Patients { get { return this.patients; } set { this.patients = value; } }

        public Department()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = "";
            this.HeadID = "";
            this.Doctors = new Dictionary<String, Doctor>();
            this.Nurse = new Dictionary<String, Nurse>();
            this.Patients = new Dictionary<String, Patient>();
        }
        public void addDoctor(Doctor doctor)
        {
            if(!Doctors.ContainsKey(doctor.ID))
            this.Doctors.Add(doctor.ID, doctor);
        }
        public void removeDoctor(String doctorID)
        {
            this.Doctors.Remove(doctorID);
        }
        public void addNurse(Nurse nurse)
        {
            if(!Nurse.ContainsKey(nurse.ID))
            this.Nurse.Add(nurse.ID, nurse);
        }
        public void removeNurse(String nurseID)
        {
            this.Nurse.Remove(nurseID);
        }
        public void addPatient(Patient patient)
        {
            if(!Patients.ContainsKey(patient.ID))
            this.Patients.Add(patient.ID,patient);
        }
        public void removePatient(String patientID)
        {
            this.Patients.Remove(patientID);
        }
    }
}
