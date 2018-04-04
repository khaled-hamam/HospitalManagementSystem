using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Room
    {
        protected string _id { set; get; }
        protected List<Patient> patients { set; get; }
        protected List<Nurse> nurses { set; get; }
        protected int capacity { set; get; }
        protected float price { set; get; }

        public Room()
        {
            this._id = "";
            this.capacity = 0;
            this.price = 0;
            this.patients = new List<Patient>();
            this.nurses = new List<Nurse>();
        }
        public Room(int capacity, float price)
        {
            this.capacity = capacity;
            this.price = price;
        }
        public void addPatient(Patient patient)
        {
            this.patients.Add(patient);
        }
        public void removePatient(string patientID)
        {
            for (int i=0; i<patients.Count; i++)
            {
                if (patients[i]._id == patientID)
                {
                    patients.Remove(patients[i]);
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
            for(int i=0; i<nurses.Count; i++)
            {
                if (nurses[i]._id == nurseID)
                {
                    nurses.Remove(nurses[i]);
                    return;
                }
            }
        }
        public bool hasAvailableBed()
        {
            return (capacity-patients.Count)>0;
        }

    }
}
