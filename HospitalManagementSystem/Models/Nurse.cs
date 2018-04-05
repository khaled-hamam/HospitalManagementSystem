using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Nurse : Employee
    {
        private List<Room> rooms;
        private List<Patient> patients;
        private Department department;
        public List<Room> Rooms { get { return this.rooms; } set { this.rooms = value; } }
        public List<Patient> Patients { get { return this.patients; } set { this.patients = value; } }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public void addRoom(Room room)
        {
            this.Rooms.Add(room);
        }

        public void removeRoom(string id)
        {
            foreach (Room room in rooms)
            {
                if (room.Id == id)
                {
                    rooms.Remove(room);
                    break;
                }
            }
        }

        public void addPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public void removePatient(string id)
        {
            foreach (Patient patient in patients)
            {
                if (patient.Id == id)
                {
                    patients.Remove(patient);
                    break;
                }
            }
        }
    }
}
