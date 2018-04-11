using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Room
    {
        protected string _id;
        protected List<Patient> patients;
        protected List<Nurse> nurses;
        protected int capacity;
        protected double price;
        // getters & setters
        public string Id { get { return this._id; } set { this._id = value; } }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public List<Nurse> Nurses { get { return this.nurses; } set { this.nurses = value; } }
        public int Capacity { get { return this.capacity; } set { this.capacity = value; } }
        public double Price { get { return this.price; } set { this.price = value; } }
        // constructors
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = 0;
            this.Price = 0;
            this.Patients = new List<Patient>();
            this.Nurses = new List<Nurse>();
        }
        public Room(int capacity, double price)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = capacity;
            this.Price = price;
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
        public void addNurse(Nurse nurse)
        {
            this.nurses.Add(nurse);
        }
        public void removeNurse(string nurseID)
        {
            for(int i = 0; i < nurses.Count; i++)
            {
                if (nurses[i].Id == nurseID)
                {
                    nurses.Remove(nurses[i]);
                    return;
                }
            }
        }
        public bool hasAvailableBed()
        {
            return (capacity - patients.Count) > 0;
        }

    }
}
