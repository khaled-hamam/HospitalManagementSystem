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
        private Room room { get; set; }
        private List<Medicine> history { get; set; }
        public int duration { get; set; }
        //constructors
        public ResidentPatient() : base()
        {
            this.room = new Room();
            this.history = new List<Medicine>();
        }
        public ResidentPatient(string id, string name, DateTime dt, string address, string diagnosis, Room room, int duration) : base(id, name, dt, address, diagnosis)
        {

            this.room = room;
            this.duration = duration;
            this.history = new List<Medicine>();
        }
        // member methods
        public void addMedicine(Medicine M)
        {
            history.Add(M);
        }
        public override float getBill()
        {
            float bill = this.duration + (float)(this.duration * 0.5);
            return bill;
        }
    }
}