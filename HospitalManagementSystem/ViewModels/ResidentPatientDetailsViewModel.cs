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
        // Main page Data
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String PatientRoomNumber { get; set; }
        public String PatientBill { get; set; }
        // Edit Content
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
        public String MedicalHistoryTextBox { get; set; }

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
           

        }

        public ResidentPatientDetailsViewModel(String id)
        {

            DoctorsList = new ObservableCollection<ComboBoxPairs>();
            NursesList = new ObservableCollection<ComboBoxPairs>();
            MedicalHistoryList = new ObservableCollection<ComboBoxPairs>();
            PatientRoomNumberComboBox = new ObservableCollection<ComboBoxPairs>();

            IsResident = Visibility.Collapsed;
            EditPatientBirthDatePicker = DateTime.Today;

            foreach (Room room in Hospital.Rooms.Values)
            {
                PatientRoomNumberComboBox.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }

            foreach (Doctor doctor in Hospital.Patients[id].Doctors.Values)
            {
                DoctorsList.Add(new ComboBoxPairs(doctor.ID, doctor.Name));
            }
            DoctorsNumber = "Doctors: " + Hospital.Patients[id].Doctors.Count().ToString();

            //foreach (Nurse nurse in Hospital.Patients[id].Nurse.Values)
            //{
            //    NursessList.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
            //}
            //NursesNumber ="Nurses:" + Hospital.Patients[id].Nurse.Count().ToString();

            //foreach (((ResidentPatient)Hospital.Patients[id]).History.Values)
            //{
            //    MedicalHistoryList.Add(Hospital.Patients[id]).History.Values)
            //}

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
