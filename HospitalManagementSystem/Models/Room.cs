using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    abstract class Room
    {
        protected String id;
        protected int roomNumber;
        protected Dictionary<String, Patient> patients;
        protected Dictionary<String, Nurse> nurses;
        protected int capacity;
        protected double price;

        public String ID { get { return this.id; } set { this.id = value; } }
        public int RoomNumber { get { return this.roomNumber; } set { this.roomNumber = value; } }
        public Dictionary<String, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public  Dictionary<String, Nurse> Nurses { get { return this.nurses; } set { this.nurses = value; } }
        public int Capacity { get { return this.capacity; } set { this.capacity = value; } }
        public double Price { get { return this.price; } set { this.price = value; } }

        // constructors
        public Room()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Capacity = 0;
            this.Price = 0;
            this.Patients = new Dictionary<String, Patient>();
            this.Nurses = new Dictionary<String, Nurse>();
        }

        public Room(int capacity, double price)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Capacity = capacity;
            this.Price = price;
            this.Patients = new Dictionary<String, Patient>();
            this.Nurses = new Dictionary<String, Nurse>();
        }

        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient.ID, patient);
        }
        public void removePatient(String patientID)
        {
            this.Patients.Remove(patientID);   
        }
        public void addNurse(Nurse nurse)
        {
            this.Nurses.Add(nurse.ID, nurse);
        }
        public void removeNurse(String nurseID)
        {
            this.Nurses.Remove(nurseID);
        }
        public bool hasAvailableBed()
        {
            return (Capacity - Patients.Count) > 0;
        }

    }
}
