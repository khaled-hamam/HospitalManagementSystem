using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Room
    {
        protected String _id;
        protected String roomNumber;
        protected Dictionary<String, Patient> patients;
        protected Dictionary<String, Nurse> nurses;
        protected int capacity;
        protected double price;
        // getters & setters
        public String Id { get { return this._id; } set { this._id = value; } }
        public String RoomNumber { get { return this.roomNumber; } set { this.roomNumber = value; } }
        public Dictionary<String, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public  Dictionary<String, Nurse> Nurses { get { return this.nurses; } set { this.nurses = value; } }
        public int Capacity { get { return this.capacity; } set { this.capacity = value; } }
        public double Price { get { return this.price; } set { this.price = value; } }
        // constructors
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = 0;
            this.Price = 0;
            this.Patients = new Dictionary<String, Patient>();
            this.Nurses = new Dictionary<String, Nurse>();
        }
        public Room(int capacity, double price)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = capacity;
            this.Price = price;
        }
        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient.Id, patient);
        }
        public void removePatient(String patientID)
        {
            this.Patients.Remove(patientID);   
        }
        public void addNurse(Nurse nurse)
        {
            this.Nurses.Add(nurse.Id, nurse);
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
