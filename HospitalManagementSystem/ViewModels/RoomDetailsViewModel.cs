using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using HospitalManagementSystem.Views.Components;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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

        public ObservableCollection<ComboBoxPairs> PatientsList { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesList { get; set; }

        public ObservableCollection<ComboBoxPairs> NursesComboBoxItems { get; set; }

        public ICommand EditRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand assignNurse { get; set; }

        public ComboBoxPairs NurseSelectedItem { get; set; }
        public ComboBoxPairs ListSelectedNurse { get; set; }


        public RoomDetailsViewModel(String ID)
        {
            RoomID = ID;
            assignNurse = new RelayCommand(AssignNurse);
            EditRoom = new RelayCommand(EditRooms);
            DeleteRoom = new RelayCommand(DeleteRooms);
            NursesList = new ObservableCollection<ComboBoxPairs>();
            PatientsList = new ObservableCollection<ComboBoxPairs>();
            NurseSelectedItem = new ComboBoxPairs("Key", "Value");
            ListSelectedNurse = new ComboBoxPairs("Key", "Value");
            RoomNumber = Hospital.Rooms[ID].RoomNumber.ToString();
            editedRoomNumber = RoomNumber;


            foreach (Patient patient in Hospital.Rooms[ID].Patients.Values)
            {
                PatientsList.Add(new ComboBoxPairs(patient.ID, patient.Name));
            }

            NursesComboBoxItems = new ObservableCollection<ComboBoxPairs>();

            foreach (Nurse nurse in Hospital.Rooms[ID].Nurses.Values)
            {
                NursesList.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
            }

            foreach (Employee nurse in Hospital.Employees.Values)
            {
                Boolean valid = true;
                foreach (Nurse invalidNurse in Hospital.Rooms[ID].Nurses.Values)
                {
                    if (nurse.ID == invalidNurse.ID && nurse.GetType() == typeof(Nurse))
                    {
                        valid = false;
                    }
                }
                if (valid && nurse.GetType() == typeof(Nurse))
                {
                    NursesComboBoxItems.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
                }
            }
            PatientsNumber = $"Patients : {PatientsList.Count}";
            NursesNumber = $"Nurses : {NursesList.Count}";
        }

        public void EditRooms()
        {
            if (String.IsNullOrEmpty(editedRoomNumber))
            {
                System.Windows.Forms.MessageBox.Show("Room Number can't be Empty");
                return;
            }
            bool ValideRoom = true;
            foreach(Room room in Hospital.Rooms.Values)
            {
                if(int.Parse(editedRoomNumber) == room.RoomNumber)
                {
                    ValideRoom = false;
                    break;
                }
            }
            if(!ValideRoom && editedRoomNumber != RoomNumber)
            {
                System.Windows.Forms.MessageBox.Show("ROOM NUMBER IS ALREADY EXIST");
            }
            else
            {    
                RoomNumber = editedRoomNumber;
                Hospital.Rooms[RoomID].RoomNumber = int.Parse(editedRoomNumber);
                HospitalDB.UpdateRoom(Hospital.Rooms[RoomID]);
                Home.ViewModel.CloseRootDialog();
            }
        }
        public async void DeleteRooms()
        {

            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {
                Hospital.Rooms.Remove(RoomID);
                Home.ViewModel.Content = new RoomsViewModel();
                HospitalDB.DeleteRoom(RoomID);
            }
        }

        public async void RemoveNurse()
        {
            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {

                NursesComboBoxItems.Add(new ComboBoxPairs(ListSelectedNurse.Key, ListSelectedNurse.Value));
                Hospital.Rooms[RoomID].removeNurse(ListSelectedNurse.Key);
                ((Nurse)Hospital.Employees[ListSelectedNurse.Key]).removeRoom(RoomID);
                HospitalDB.DeleteNurseRoom(ListSelectedNurse.Key, RoomID);
                NursesList.Remove(ListSelectedNurse);
                NursesNumber = $"Nurses : {NursesList.Count}";

            }
        }
        public void AssignNurse()
        {
            Home.ViewModel.CloseRootDialog();

            NursesList.Add(new ComboBoxPairs(NurseSelectedItem.Key, NurseSelectedItem.Value));

            HospitalDB.InsertNurseRoom(NurseSelectedItem.Key, RoomID);

            Hospital.Rooms[RoomID].addNurse((Nurse)(Hospital.Employees[NurseSelectedItem.Key]));
            ((Nurse)Hospital.Employees[NurseSelectedItem.Key]).addRoom(Hospital.Rooms[RoomID]);
           
            NursesComboBoxItems.Remove(NurseSelectedItem);
            NursesNumber = $"Nurses : {NursesList.Count}";

        }
    }
}
