using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementSystem.ViewModels
{
    public class ResidentPatientDetailsViewModel : BaseViewModel
    {
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String PatientRoomNumber { get; set; }
        public String PatientBill { get; set; }
        public ObservableCollection<String> DoctorsNumber { get; set; }
        public ObservableCollection<String> NursesNumber { get; set; }
        public ObservableCollection<String> DoctorsList { get; set; }
        public ObservableCollection<String> NursessList { get; set; }
        public ObservableCollection<String> MedicalHistoryList { get; set; }
        public String EditPatientNameTextBox { get; set; }
        public String EditPatientAddressTextBox { get; set; }
        public DateTime EditPatientBirthDatePicker { get; set; }
        public ComboBoxPairs EditRoomNumberComboBox { get; set; }
        public List<ComboBoxPairs> RoomNumberComboBoxItems;
        public ComboBoxPairs DoctorComboBoxPair { get; set; }
        public String DoctorComboBox { get; set; }
        public List<ComboBoxPairs> DoctorsComboBoxItems;
        public ComboBoxPairs NurseComboBoxPair { get; set; }
        public String NurseComboBox { get; set; }
        public List<ComboBoxPairs> NursesComboBoxItems;

        public Visibility IsResident { get; set; }


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
            IsResident = Visibility.Collapsed;
            EditPatientBirthDatePicker = DateTime.Today;
            RoomNumberComboBoxItems = new List<ComboBoxPairs>();
            foreach (Room room in Hospital.Rooms.Values)
            {
                RoomNumberComboBoxItems.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }
            //TODO : DoctorsComboBoxItems 

            // TODO : NursesComboBoxItems

        }

        public void EditResidentPatient()
        {

        }

        public void DeleteResidentPatient()
        {

        }




    }
}
