using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Nurse : Employee
    {
        private Dictionary<String, Room> rooms;
        private Dictionary<String, Patient> patients;
        private Department department;
        public Dictionary<String, Room> Rooms { get { return this.rooms; } set { this.rooms = value; } }
        public Dictionary<String, Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public Nurse()
        {
            Rooms = new Dictionary<String, Room>();
            Patients = new Dictionary<String, Patient>();
            Department = new Department();
        }

        public void addRoom(Room room)
        {
            this.Rooms.Add(room.ID, room);
        }

        public void removeRoom(String id)
        {
            this.Rooms.Remove(id);
        }

        public void addPatient(Patient patient)
        {
            this.Patients.Add(patient.ID, patient);
        }

        public void removePatient(String id)
        {
            this.Patients.Remove(id);
        }
    }
}
