using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    class ResidentPatient : Patient
    {
        // member variables
        private Room room;
        private Dictionary<String, Medicine> history;
        private int duration;
        private Department department;

        public Room Room { get { return this.room;} set { this.room = value;} }
        public Dictionary<String, Medicine> History { get { return this.history; } set { this.history = value;} }
        public int Duration { get { return this.duration;} set { this.duration = value;} }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public ResidentPatient() : base()
        {
            //this.Room = new Room();
            this.History = new Dictionary<String, Medicine>();
        }
        public ResidentPatient(String name, DateTime birthDate, String address, String diagnosis, Room room, int duration, Department department) : base(name, birthDate, address, diagnosis)
        {

            this.Room = room;
            this.Duration = duration;
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
            double bill = this.duration + (this.duration * Room.Price);
            return bill;
        }
    }
}