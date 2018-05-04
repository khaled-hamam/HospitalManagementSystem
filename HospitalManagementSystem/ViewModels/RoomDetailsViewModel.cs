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

        public ObservableCollection<ComboBoxPairs> PatientsList { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesList { get; set; }

        public ObservableCollection<ComboBoxPairs> PatientsComboBoxItems { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesComboBoxItems { get; set; }

        public ICommand EditRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand assignPatient { get; set; }
        public ICommand assignNurse { get; set; }

        public ComboBoxPairs PatientSelectedItem { get; set; }
        public ComboBoxPairs NurseSelectedItem { get; set; }


        public RoomDetailsViewModel(String ID)
        {
            RoomID = ID;
            assignNurse = new RelayCommand(AssignNurse);
            assignPatient = new RelayCommand(AssignPatient);
            EditRoom = new RelayCommand(EditRooms);
            DeleteRoom = new RelayCommand(DeleteRooms);
            NursesList = new ObservableCollection<ComboBoxPairs>();
            PatientsList = new ObservableCollection<ComboBoxPairs>();
            PatientSelectedItem = new ComboBoxPairs("Key", "Value");
            NurseSelectedItem = new ComboBoxPairs("Key", "Value");

            PatientsComboBoxItems = new ObservableCollection<ComboBoxPairs>();

            foreach (Patient patient in Hospital.Rooms[ID].Patients.Values)
            {
                PatientsList.Add(new ComboBoxPairs(patient.ID, patient.Name));
            }

            foreach (Patient patient in Hospital.Patients.Values)
            {
                Boolean valid = true;
                foreach (Patient invalidPatient in Hospital.Rooms[ID].Patients.Values)
                {
                    if (patient.ID == invalidPatient.ID)
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
            RoomNumber = editedRoomNumber;
            Hospital.Rooms[RoomID].RoomNumber = int.Parse(editedRoomNumber);
            if (RoomType == "Private")
            {
                Hospital.Rooms[RoomID].Capacity = 1;
                Hospital.Rooms[RoomID].Price = 100;
            }
            else if (RoomType == "Semi Private")
            {
                Hospital.Rooms[RoomID].Capacity = 2;
                Hospital.Rooms[RoomID].Price = 50;
            }
            else if (RoomType == "Standard Ward")
            {
                Hospital.Rooms[RoomID].Capacity = 4;
                Hospital.Rooms[RoomID].Price = 25;
            }
            PatientsList.Clear();
            NursesList.Clear();

            NursesNumber = $"Nurses : {NursesList.Count}";
            PatientsNumber = $"Patients : {PatientsList.Count}";

            roomCapacity = $"{PatientsList.Count} / {Hospital.Rooms[RoomID].Capacity}";
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
            Home.ViewModel.CloseRootDialog();

            if (PatientsList.Count > 0 && RoomType == "Private Room")
            {
                MessageBox.Show("No Enough Beds In Room");
            }
            else if (PatientsList.Count > 1 && RoomType == "Semi Private")
            {
                MessageBox.Show("No Enough Beds In Room");
            }
            else if (PatientsList.Count > 3 && RoomType == "Standard Ward")
            {
                MessageBox.Show("No Enough Beds In Room");
            }
            else
            {
                PatientsList.Add(new ComboBoxPairs(PatientSelectedItem.Key, PatientSelectedItem.Value));
                foreach (Patient patient in Hospital.Patients.Values)
                {
                    if (patient.ID == PatientSelectedItem.Key && patient.Name == PatientSelectedItem.Value)
                    {
                        Hospital.Rooms[RoomID].addPatient(patient);
                        break;
                    }
                }
                PatientsNumber = $"Patients : {PatientsList.Count}";
                roomCapacity = $"{PatientsList.Count} / {Hospital.Rooms[RoomID].Capacity}";
            }

        }
        public void AssignNurse()
        {
            Home.ViewModel.CloseRootDialog();

            NursesList.Add(new ComboBoxPairs(NurseSelectedItem.Key, NurseSelectedItem.Value));
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.ID == NurseSelectedItem.Key &&
                   employee.Name == NurseSelectedItem.Value &&
                   employee.GetType() == typeof(Nurse))
                {
                    Hospital.Rooms[RoomID].addNurse((Nurse)employee);
                    break;
                }
            }
            NursesComboBoxItems.Remove(NurseSelectedItem);
            NursesNumber = $"Nurses : {NursesList.Count}";

        }
    }
}
