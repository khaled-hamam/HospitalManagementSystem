using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class ResidentPatientDetailsViewModel : BaseViewModel
    {
        // Main page Data
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String PatientRoomNumber { get; set; }
        public String PatientBill { get; set; }
        // Edit Content
        public ICommand editResidentPatient { get; set; }
        public ICommand deleteReidentPatient { get; set; }
        public String EditPatientNameTextBox { get; set; }
        public String EditPatientAddressTextBox { get; set; }
        public DateTime EditPatientBirthDatePicker { get; set; }
        public ComboBoxPairs EditRoomNumberComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> PatientRoomNumberComboBox { get; set; }
        // Main Page Lists
        public String DoctorsNumber { get; set; }
        public String NursesNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsList { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesList { get; set; }
        public ObservableCollection<ComboBoxPairs> MedicalHistoryList { get; set; }
        // Items In Contents
        public ObservableCollection<ComboBoxPairs> DoctorsComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesComboBox { get; set; }
        public ComboBoxPairs DoctorComboBox { get; set; }
        public ComboBoxPairs NurseComboBox { get; set; }
        public String MedicineName { get; set; }
        public DateTime MedicineStartDate { get; set; }
        public DateTime MedicineEndDate { get; set; }
        public ICommand assignDoctor { get; set; }
        public ICommand addMedicine { get; set; }
        public ICommand assignNurse { get; set; }

        public Visibility IsResident { get; set; }
        public String PatientID;

        private String editPatientTypeComboBox;
        public String EditPatientTypeComboBox
        {
            get => editPatientTypeComboBox;
            set
            {
                if (value == "Resident Patient")
                    IsResident = Visibility.Visible;
                else
                    IsResident = Visibility.Collapsed;

                editPatientTypeComboBox = value;
            }
        }
        public ResidentPatientDetailsViewModel()
        {
           

        }

        public ResidentPatientDetailsViewModel(String id)
        {
            PatientID = id;

            DoctorsList = new ObservableCollection<ComboBoxPairs>();
            NursesList = new ObservableCollection<ComboBoxPairs>();
            MedicalHistoryList = new ObservableCollection<ComboBoxPairs>();
            PatientRoomNumberComboBox = new ObservableCollection<ComboBoxPairs>();
            IsResident = Visibility.Collapsed;

            EditPatientBirthDatePicker = DateTime.Today;
            EditPatientAddressTextBox = Hospital.Patients[id].Address;
            EditPatientBirthDatePicker = Hospital.Patients[id].BirthDate;
            EditPatientNameTextBox = Hospital.Patients[id].Name;
            editPatientTypeComboBox = "Resident Patient";
            foreach (Room room in Hospital.Rooms.Values)
            {
                PatientRoomNumberComboBox.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }
             // Lists
            foreach (Doctor doctor in Hospital.Patients[id].Doctors.Values)
            {
                DoctorsList.Add(new ComboBoxPairs(doctor.ID, doctor.Name));
            }
            DoctorsNumber = "Doctors: " + Hospital.Patients[id].Doctors.Count().ToString();

            foreach (Nurse nurse in ((ResidentPatient)Hospital.Patients[id]).Room.Nurses.Values)
            {
                NursesList.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
            }
            NursesNumber = "Nurses:" + ((ResidentPatient)Hospital.Patients[id]).Room.Nurses.Count().ToString();

            foreach (Medicine medicine in ((ResidentPatient)Hospital.Patients[id]).History.Values)
            {
                MedicalHistoryList.Add(new ComboBoxPairs(medicine.ID, medicine.Name + " - Starting Date: " + medicine.StartingDate.ToShortDateString() + " | " + medicine.EndingDate.ToShortDateString()));
            }

            //TODO : DoctorsComboBoxItems 
            DoctorsComboBox = new ObservableCollection<ComboBoxPairs>();
            NursesComboBox = new ObservableCollection<ComboBoxPairs>();
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if(employee.GetType() == typeof(Doctor))
                    DoctorsComboBox.Add(new ComboBoxPairs(employee.ID, employee.Name));
                else
                   NursesComboBox.Add(new ComboBoxPairs(employee.ID, employee.Name));
            }
            editResidentPatient = new RelayCommand(EditResidentPatient);
            deleteReidentPatient = new RelayCommand(DeleteResidentPatient);
            DoctorComboBox = new ComboBoxPairs("Key", "Value");
            NurseComboBox = new ComboBoxPairs("Key", "Value");
            assignDoctor = new RelayCommand(AssignDoctor);
            assignNurse = new RelayCommand(AssignNurse);
            addMedicine = new RelayCommand(AddMedicine);
            MedicineStartDate = DateTime.Today;
            MedicineEndDate = DateTime.Today;
        }

        public void EditResidentPatient()
        {
            Hospital.Patients[PatientID].Name = PatientName = EditPatientNameTextBox;
            Hospital.Patients[PatientID].Address = PatientAddress = EditPatientAddressTextBox;
            PatientBirthDate = EditPatientBirthDatePicker.ToShortDateString();
            Hospital.Patients[PatientID].BirthDate = EditPatientBirthDatePicker;
            PatientType = editPatientTypeComboBox;
            //Hospital.Patients[PatientID]
            HospitalDB.UpdatePatient(Hospital.Patients[PatientID]);
            Home.ViewModel.CloseRootDialog();
        }

        public void DeleteResidentPatient()
        {
            Hospital.Patients.Remove(PatientID);
            Home.ViewModel.Content = new ResidentPatientDetailsViewModel();
        }
        public void AssignDoctor()
        {
            DoctorsList.Add(new ComboBoxPairs(DoctorComboBox.Key, DoctorComboBox.Value));
            foreach (Doctor doctor in Hospital.Employees.Values)
            {
                if(doctor.ID == DoctorComboBox.Key)
                {
                    Hospital.Patients[PatientID].Doctors.Add(doctor.ID, doctor);
                    DoctorsNumber = "Doctors: " + Hospital.Patients[PatientID].Doctors.Count().ToString();
                    break;
                }
            }
            Home.ViewModel.CloseRootDialog();
        }

        public void AssignNurse()
        {
            NursesList.Add(new ComboBoxPairs(NurseComboBox.Key, NurseComboBox.Value));
            foreach (Nurse nurse in Hospital.Employees.Values)
            {
                if(nurse.ID == NurseComboBox.Key)
                {
                    ((ResidentPatient)Hospital.Patients[PatientID]).Room.Nurses.Add(nurse.ID, nurse);
                    NursesNumber = "Nurses:" + ((ResidentPatient)Hospital.Patients[PatientID]).Room.Nurses.Count().ToString();
                    break;
                }
            }
            Home.ViewModel.CloseRootDialog();
        }

        public void AddMedicine()
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineName;
            medicine.StartingDate = MedicineStartDate;
            medicine.EndingDate = MedicineEndDate;
            ((ResidentPatient)Hospital.Patients[PatientID]).History.Add(medicine.ID, medicine);
        }
    }
}
