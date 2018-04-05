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
        private List<Doctors> doctors;
        private List<Nurse> nurse;
        private List<Patient> patients;
        // getters & setters
        public string Name { get { return this.name; } set { this.name = value; } }
        public string HeadId { get { return this.headId; } set { this.headId = value; } }
        public List<Doctors> Doctors { get { return this.doctors; } set { this.doctors = value; } }
        public List<Nurse> Nurse { get { return this.nurse; } set { this.nurse = value; } }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        // constructors
        public Department()
        {
            this.name = "";
            this.headId = "";
            this.doctors = new List<Doctors>();
            this.nurses = new List<Nurse>();
            this.patients = new List<Patient>();
        }
        public void addDoctor(Doctor doctor)
        {
            this.doctors.Add(doctor);
        }
        public void removeDoctor(string doctorID)
        {
            for(int i=0; i<doctors.Count; i++)
            {
                if (doctors[i]._id == doctorID)
                {
                    doctors.Remove(doctors[i]);
                    return;
                }
            }
        }
        public void addNurse(Nurse nurse)
        {
            this.nurses.Add(nurse);
        }
        public void removeNurse(string nurseID)
        {
            for (int i = 0; i < nurses.Count; i++)
            {
                if (nurses[i].Id == nurseID)
                {
                    nurses.Remove(nurses[i]);
                    return;
                }
            }
        }
        public void addPatient(Patient patient)
        {
            this.patients.Add(patient);
        }
        public void removePatient(string patientID)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id == patientID)
                {
                    patients.Remove(patients[i]);
                    return;
                }
            }
        }
           

    }
}
