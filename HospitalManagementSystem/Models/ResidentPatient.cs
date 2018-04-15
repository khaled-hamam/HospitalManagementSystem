using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class ResidentPatient : Patient
    {
        // member variables
        private Room room;
        private Dictionary<string, Medicine> history;
        private int duration;
        public Room Room { get { return this.room;} set { this.room = value;} }
        public Dictionary<string,Medicine> History { get { return this.history; } set { this.history = value;} }
        public int Duration { get { return this.duration;} set { this.duration = value;} }
        //constructors
        public ResidentPatient() : base()
        {
            this.Room = new Room();
            this.History = new Dictionary<string, Medicine>();
        }
        public ResidentPatient(string name, DateTime birthDate, string address, string diagnosis, Room room, int duration) : base(name, birthDate, address, diagnosis)
        {

            this.Room = room;
            this.Duration = duration;
            this.History = new Dictionary<string, Medicine>();
        }
        // member methods
        public void addMedicine(string id,Medicine medicine)
        {
            this.history.Add(id,medicine);
        }
        public override double getBill()
        {
            double bill = this.duration + (this.duration * Room.Price);
            return bill;
        }
    }
}