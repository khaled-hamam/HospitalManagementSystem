﻿using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
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

        public ObservableCollection<ComboBoxPairs> PatientsList { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesList { get; set; }

        public ObservableCollection<ComboBoxPairs> NursesComboBoxItems { get; set; }

        public ICommand EditRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand assignNurse { get; set; }

        public ComboBoxPairs NurseSelectedItem { get; set; }


        public RoomDetailsViewModel(String ID)
        {
            RoomID = ID;
            assignNurse = new RelayCommand(AssignNurse);
            EditRoom = new RelayCommand(EditRooms);
            DeleteRoom = new RelayCommand(DeleteRooms);
            NursesList = new ObservableCollection<ComboBoxPairs>();
            PatientsList = new ObservableCollection<ComboBoxPairs>();
            NurseSelectedItem = new ComboBoxPairs("Key", "Value");
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
                MessageBox.Show("Room Number can't be Empty");
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
                MessageBox.Show("ROOM NUMBER IS ALREADY EXIST");
            }
            else
            {    
                RoomNumber = editedRoomNumber;
                Hospital.Rooms[RoomID].RoomNumber = int.Parse(editedRoomNumber);
                HospitalDB.UpdateRoom(Hospital.Rooms[RoomID]);
                Home.ViewModel.CloseRootDialog();
            }
        }
        public void DeleteRooms()
        {
            Hospital.Rooms.Remove(RoomID);
            Home.ViewModel.CloseRootDialog();
            Home.ViewModel.Content = new RoomsViewModel();
            HospitalDB.DeleteRoom(RoomID);
        }   
       
        public void AssignNurse()
        {
            Home.ViewModel.CloseRootDialog();

            NursesList.Add(new ComboBoxPairs(NurseSelectedItem.Key, NurseSelectedItem.Value));

            Hospital.Rooms[RoomID].addNurse((Nurse)(Hospital.Employees[NurseSelectedItem.Key]));
            ((Nurse)Hospital.Employees[NurseSelectedItem.Key]).addRoom(Hospital.Rooms[RoomID]);
           
            NursesComboBoxItems.Remove(NurseSelectedItem);
            NursesNumber = $"Nurses : {NursesList.Count}";

            HospitalDB.InsertNurseRoom(NurseSelectedItem.Key, RoomID);
        }
    }
}
