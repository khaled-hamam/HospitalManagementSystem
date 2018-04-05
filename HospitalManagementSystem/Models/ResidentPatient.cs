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
        private List<Medicine> history;
        private int duration;
        public Room Room { get { return this.room; } set { this.room = value; } }
        public List<Medicine> History { get { return this.history; } set { this.history = value; } }
        public int Duration { get { return this.duration; } set { this.duration = value; } }
        //constructors
        public ResidentPatient() : base()
        {
            this.room = new Room();
            this.history = new List<Medicine>();
        }
        public ResidentPatient(string id, string name, DateTime birthDate, string address, string diagnosis, Room room, int duration) : base(id, name, birthDate, address, diagnosis)
        {

            this.room = room;
            this.duration = duration;
            this.history = new List<Medicine>();
        }
        // member methods
        public void addMedicine(Medicine M)
        {
            this.history.Add(M);
        }
        public override double getBill()
        {
            float bill = this.duration + (this.duration * room.Price);
            return bill;
        }
    }
}