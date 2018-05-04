using HospitalManagementSystem.Models;
using HospitalManagementSystem.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class RoomDetailsViewModel : BaseViewModel
    {
        public String RoomID { get; set; }  
        public String RoomNumber { get; set; }
        public String RoomType { get; set; }
        public String roomPrice { get; set; }
        public String roomCapacity { get; set; }
        public String editedRoomNumber { get; set; }
        public String PatientsNumber { get; set; }
        public String NursesNumber { get; set; }

        public ObservableCollection<String> PatientsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }

        public ObservableCollection<ComboBoxPairs> PatientsComboBoxItems { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesComboBoxItems { get; set; }

        public ICommand EditRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand assignPatient { get; set; }
        public ICommand assignNurse { get; set; }

        public ComboBoxPairs PatientSelectedItem;
        public ComboBoxPairs NurseSelectedItem;
        

        public RoomDetailsViewModel(String ID)
        {
            RoomID = ID;
            assignNurse = new RelayCommand(AssignNurse);
            assignPatient = new RelayCommand(AssignPatient);
            EditRoom = new RelayCommand(EditRooms);
            DeleteRoom = new RelayCommand(DeleteRooms);
            NursesList = new ObservableCollection<String>();
            PatientsList = new ObservableCollection<String>();
            PatientSelectedItem = new ComboBoxPairs("","");
            NurseSelectedItem = new ComboBoxPairs("", "");

            PatientsComboBoxItems = new ObservableCollection<ComboBoxPairs>();

           foreach (Patient patient in Hospital.Rooms[ID].Patients.Values)
            {
                PatientsList.Add(patient.Name);
            }

            foreach (Patient patient in Hospital.Patients.Values)
            {
                Boolean valid = true;
                foreach (Patient invalidPatient in Hospital.Rooms[ID].Patients.Values)
                {
                    if(patient.ID == invalidPatient.ID)
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    PatientsComboBoxItems.Add(new ComboBoxPairs(patient.ID, patient.Name));
                }
            }

            NursesComboBoxItems = new ObservableCollection<ComboBoxPairs>();

            foreach (Nurse nurse in Hospital.Rooms[ID].Nurses.Values)
            {
                NursesList.Add(nurse.Name);
            }

            foreach (Nurse nurse in Hospital.Employees.Values)
            {
                Boolean valid = true;
                foreach (Nurse invalidNurse in Hospital.Rooms[ID].Nurses.Values)
                {
                    if (nurse.ID == invalidNurse.ID && nurse.GetType() == typeof(Nurse))
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    NursesComboBoxItems.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
                }
            }
            PatientsNumber = $"Patients : {PatientsList.Count}";
            NursesNumber = $"Nurses : {NursesList.Count}";
        }

        public void EditRooms()
        {
            RoomNumber = editedRoomNumber;
            Hospital.Rooms[RoomID].RoomNumber = int.Parse(editedRoomNumber);
            roomCapacity = PatientsList.Count + '/' + Hospital.Rooms[RoomID].Capacity.ToString();
            roomPrice = $"{Hospital.Rooms[RoomID].Price}$";
            //HospitalDB.UpdateRoom(Hospital.Rooms[RoomID]);
            Home.ViewModel.CloseRootDialog();
        }
        public void DeleteRooms()
        {
            Hospital.Rooms.Remove(RoomID);
            Home.ViewModel.CloseRootDialog();
            Home.ViewModel.Content = new RoomsViewModel();
        }
        public void AssignPatient()
        {

            PatientsList.Add(PatientSelectedItem.Value);
            PatientsComboBoxItems.Remove(PatientSelectedItem);
            PatientsNumber = $"Patients : {PatientsList.Count}";

        }
        public void AssignNurse()
        {
            NursesList.Add(NurseSelectedItem.Value);
            NursesComboBoxItems.Remove(NurseSelectedItem);
            NursesNumber = $"Nurses : {NursesList.Count}";
        }
    }
}
