using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    class ResidentPatient : Patient
    {
        // member variables
        private Room room;
        private Dictionary<String, Medicine> history;
        private DateTime entryDate;
        private Department department;

        public Room Room { get { return this.room;} set { this.room = value;} }
        public Dictionary<String, Medicine> History { get { return this.history; } set { this.history = value;} }
        public DateTime EntryDate { get { return this.entryDate; } set { this.entryDate = value;} }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public ResidentPatient() : base()
        {
            //this.Room = new Room();
            this.entryDate = DateTime.Now;
            this.History = new Dictionary<String, Medicine>();
        }
        public ResidentPatient(String name, DateTime birthDate, String address, String diagnosis, Room room,  Department department) : base(name, birthDate, address, diagnosis)
        {

            this.Room = room;
            this.EntryDate = DateTime.Now;
            this.History = new Dictionary<String, Medicine>();
            this.Department = department;
        }
        // member methods
        public void addMedicine(Medicine medicine)
        {
            if(!History.ContainsKey(medicine.ID))
            this.history.Add(medicine.ID,medicine);
        }
        public override double getBill()
        {
            double bill =  ((DateTime.Now - EntryDate).Days * Room.Price);
            return bill;
        }
    }
}