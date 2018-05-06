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
    public class PatientsViewModel : BaseViewModel
    {
        /// <summary>
        /// Add Dialog Properties
        /// </summary>
        public String PatientNameTextBox { get; set; }
        public String PatientAddressTextBox { get; set; }
        public ComboBoxPairs RoomNumber { get; set; }
        public ComboBoxPairs PatientDepartment { get; set; }
        public ObservableCollection<ComboBoxPairs> ComboBoxItems { get; set; }
        public ObservableCollection<ComboBoxPairs> PatientDepartmentItems { get; set; }
        public String textValidation { get; set; }
        public ICommand addNewPatient { get; set; }

        private String patientTypeComboBox;
        public String PatientTypeComboBox
        {
            get => patientTypeComboBox;
            set
            {
                if (value == "Resident Patient")
                    IsResident = Visibility.Visible;
                else
                    IsResident = Visibility.Collapsed;

                patientTypeComboBox = value;
            }
        }

        public DateTime PatientBirthDatePicker { get; set; }
        public Visibility IsResident { get; set; }

        public ICommand SearchAction { get; set; }
        /// <summary>
        /// Search Bar Properties
        /// </summary>
        public String SearchQuery { get; set; }
        /// <summary>
        /// Items Properties
        /// </summary>
        public ObservableCollection<PatientCardViewModel> Patients { get; set; }
        public ObservableCollection<PatientCardViewModel> FilteredPatients { get; set; }

        public PatientsViewModel()
        {
            addNewPatient = new RelayCommand(addPatient);
            IsResident = Visibility.Collapsed;
            SearchAction = new RelayCommand(Search);
            Patients = new ObservableCollection<PatientCardViewModel>();
            PatientBirthDatePicker = DateTime.Today;
            ComboBoxItems = new ObservableCollection<ComboBoxPairs>();
            PatientDepartmentItems = new ObservableCollection<ComboBoxPairs>();

            foreach (Room room in Hospital.Rooms.Values) {
                ComboBoxItems.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
             }

            foreach (Department department in Hospital.Departments.Values)
            {
                PatientDepartmentItems.Add(new ComboBoxPairs(department.ID, department.Name));
            }

            foreach (Patient patient in Hospital.Patients.Values)
            {
                Patients.Add(
                    new PatientCardViewModel
                    {
                        ID = patient.ID,
                        Name = patient.Name,
                        Type = (patient.GetType() == typeof(ResidentPatient)) ? "Resident" : "Appointment",
                        ShortDiagnosis = patient.Diagnosis
                    }
                );
            }
            FilteredPatients = new ObservableCollection<PatientCardViewModel>(Patients);
        }
        private void Search()
        {
            if (String.IsNullOrEmpty(SearchQuery))
            {
                FilteredPatients = new ObservableCollection<PatientCardViewModel>(Patients);
                return;
            }
            FilteredPatients = new ObservableCollection<PatientCardViewModel>(Patients.Where(patient => patient.Name.ToLower().Contains(SearchQuery)));
        }
        public bool ValidateInput()
        {
            PatientNameTextBox = (PatientNameTextBox != null) ? PatientNameTextBox.Trim() : "";
            PatientAddressTextBox = (PatientAddressTextBox != null) ? PatientAddressTextBox.Trim() : "";
            PatientTypeComboBox = (PatientTypeComboBox != null) ? PatientTypeComboBox.Trim() : "";
            return !(PatientNameTextBox == "" || PatientAddressTextBox == ""  || PatientTypeComboBox == "");
        }
        public void addPatient()
        {
            if (!ValidateInput())
            {
                textValidation = " Cannot have empty values";
                return;
            }
            if (PatientTypeComboBox == "Resident Patient")
            {
                ResidentPatient newPatient = new ResidentPatient
                {
                    Name = PatientNameTextBox,
                    Address = PatientAddressTextBox,
                    BirthDate = PatientBirthDatePicker,
                    Room = Hospital.Rooms[RoomNumber.Key],
                    Department = Hospital.Departments[PatientDepartment.Key]
                };
                Patients.Add(new PatientCardViewModel
                {
                    ID = newPatient.ID,
                    Name = PatientNameTextBox,
                    Type = PatientTypeComboBox,
                    ShortDiagnosis = "Not Implemented yet"
                });

                FilteredPatients.Add(new PatientCardViewModel
                {
                    ID = newPatient.ID,
                    Name = PatientNameTextBox,
                    Type = PatientTypeComboBox,
                    ShortDiagnosis = "Not Implemented yet"
                });
                Hospital.Departments[PatientDepartment.Key].Patients.Add(newPatient.ID, newPatient);
                Hospital.Patients.Add(newPatient.ID, newPatient);
                Hospital.Rooms[RoomNumber.Key].Patients.Add(newPatient.ID, newPatient);
                HospitalDB.InsertPatient(newPatient);

            } else {
                AppointmentPatient newPatient = new AppointmentPatient
                {
                    Name = PatientNameTextBox,
                    Address = PatientAddressTextBox,
                    BirthDate = PatientBirthDatePicker,
                };

                Patients.Add(new PatientCardViewModel {
                    ID = newPatient.ID,
                    Name = PatientNameTextBox,
                    Type = PatientTypeComboBox,
                    ShortDiagnosis = "Not Implemented yet"
                });

                FilteredPatients.Add(new PatientCardViewModel
                {
                    ID = newPatient.ID,
                    Name = PatientNameTextBox,
                    Type = PatientTypeComboBox,
                    ShortDiagnosis = "Not Implemented yet"
                });

                Hospital.Patients.Add(newPatient.ID, newPatient);
                HospitalDB.InsertPatient(newPatient);
            }
                Home.ViewModel.CloseRootDialog();
        }
 
    }
}
