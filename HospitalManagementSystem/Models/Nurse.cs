using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Nurse : Employee
    {
        private Dictionary<string, Room> rooms;
        private Dictionary<string, Patient> patients;
        private Department department;
        public Dictionary<string, Room> Rooms { get { return this.rooms; } set { this.rooms = value; } }
        public Dictionary<string, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public void addRoom(string id, Room room)
        {
            this.Rooms.Add(id, room);
        }

        public void removeRoom(string id)
        {
            this.Rooms.Remove(id);
        }

        public void addPatient(string id, Patient patient)
        {
            this.Patients.Add(id, patient);
        }

        public void removePatient(string id)
        {
            this.Patients.Remove(id);
        }
    }
}
