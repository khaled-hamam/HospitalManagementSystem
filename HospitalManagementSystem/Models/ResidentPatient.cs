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
        public void setRoom(Room room)
        {
            this.room = room;
        }
        public Room getRoom()
        {
            return this.room;
        }
        public void setHistory(List<Medicine> lm)
        {
            for(int i = 0; i < lm.Count; i++)
            {
                history.Add(lm[i]);
            }
        }
        public List<Medicine> getHistory()
        {
            return this.history;
        }
        public void setDuration(int duration)
        {
            this.duration = duration;
        }
        public int getDuration()
        {
            return this.duration;
        }
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