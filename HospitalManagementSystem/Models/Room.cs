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
        protected string roomNumber;
        protected Dictionary<string, Patient> patients;
        protected Dictionary<string, Nurse> nurses;
        protected int capacity;
        protected double price;
        // getters & setters
        public string Id { get { return this._id; } set { this._id = value; } }
        public string RoomNumber { get { return this.roomNumber; } set { this.roomNumber = value; } }
        public Dictionary<string, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public  Dictionary<string,Nurse> Nurses { get { return this.nurses; } set { this.nurses = value; } }
        public int Capacity { get { return this.capacity; } set { this.capacity = value; } }
        public double Price { get { return this.price; } set { this.price = value; } }
        // constructors
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = 0;
            this.Price = 0;
            this.Patients = new Dictionary<string, Patient>();
            this.Nurses = new Dictionary< string, Nurse>();
        }
        public Room(int capacity, double price)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Capacity = capacity;
            this.Price = price;
        }
        public void addPatient(string id, Patient patient)
        {
            this.Patients.Add(id, patient);
        }
        public void removePatient(string patientID)
        {
            this.Patients.Remove(patientID);   
        }
        public void addNurse(string id, Nurse nurse)
        {
            this.Nurses.Add(id, nurse);
        }
        public void removeNurse(string nurseID)
        {
            this.Nurses.Remove(nurseID);
        }
        public bool hasAvailableBed()
        {
            return (Capacity - Patients.Count) > 0;
        }

    }
}
