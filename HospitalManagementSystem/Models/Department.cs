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
        private List<Doctor> doctors;
        private List<Nurse> nurse;
        private List<Patient> patients;
        // getters & setters
        public string Name { get { return this.name; } set { this.name = value; } }
        public string HeadId { get { return this.headId; } set { this.headId = value; } }
        public List<Doctor> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        public List<Nurse> Nurse { get { return this.nurse; } set { this.nurse = value; } }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        // constructors
        public Department()
        {
            this.Name = "";
            this.HeadId = "";
            this.Doctors = new List<Doctor>();
            this.Nurse = new List<Nurse>();
            this.Patients = new List<Patient>();
        }
        public void addDoctor(Doctor doctor)
        {
            this.Doctors.Add(doctor);
        }
        public void removeDoctor(string doctorID)
        {
            for(int i=0; i<doctors.Count; i++)
            {
                if (this.Doctors[i].Id == doctorID)
                {
                    this.Doctors.Remove(doctors[i]);
                    return;
                }
            }
        }
        public void addNurse(Nurse nurse)
        {
            this.Nurse.Add(nurse);
        }
        public void removeNurse(string nurseID)
        {
            for (int i = 0; i < nurse.Count; i++)
            {
                if (this.Nurse[i].Id == nurseID)
                {
                    this.Nurse.Remove(nurse[i]);
                    return;
                }
            }
        }
        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient);
        }
        public void removePatient(string patientID)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                if (this.Patients[i].Id == patientID)
                {
                    this.Patients.Remove(patients[i]);
                    return;
                }
            }
        }
           

    }
}
