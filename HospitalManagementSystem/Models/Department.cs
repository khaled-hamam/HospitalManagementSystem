using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Department
    {
        private string name { set; get; }
        private string headID { set; get; } 
        private List<Doctors> doctors { set; get; }
        private List<Nurse> nurses { set; get; }
        private List<Patient> patients { set; get; }

        public Department()
        {
            this.name = "";
            this.headID = "";
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
                if (nurses[i]._id == nurseID)
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
                if (patients[i]._id == patientID)
                {
                    patients.Remove(patients[i]);
                    return;
                }
            }
        }
           

    }
}
