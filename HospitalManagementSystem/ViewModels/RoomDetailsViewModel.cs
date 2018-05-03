using HospitalManagementSystem.Models;
using System;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class RoomDetailsViewModel : BaseViewModel
    {
        public String RoomID { get; set; }  
        public String RoomNumber { get; set; }
        public String RoomType { get; set; }
        public String roomPrice { get; set; }
        public String roomCapacity { get; set; }

        public ObservableCollection<String> PatientsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }

        public ObservableCollection<ComboBoxPairs> PatientsComboBoxItems { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesComboBoxItems { get; set; }

        public RoomDetailsViewModel(String ID)
        {
            /*PatientsComboBoxItems = new ObservableCollection<ComboBoxPairs>();
            foreach (Patient patient in Hospital.Rooms[ID].Patients.Values)
            {
                PatientsComboBoxItems.Add(new ComboBoxPairs(patient.ID, patient.Name));
            }

            NursesComboBoxItems = new ObservableCollection<ComboBoxPairs>();
            foreach(Nurse nurse in Hospital.Rooms[ID].Nurses.Values)
            {
                NursesComboBoxItems.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
            }*/
        }

    }
}
