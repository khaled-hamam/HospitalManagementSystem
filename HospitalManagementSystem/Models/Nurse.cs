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

        public void addRoom(Room room)
        {
            rooms.Add(room);
        }

        public void removeRoom(string id)
        {
            foreach (Room room in rooms)
            {
                if (room._id == id)
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
                if (patient._id == id)
                {
                    patients.Remove(patient);
                    break;
                }
            }
        }
    }
}
