using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using HospitalManagementSystem.Views.Components;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentPatientDetailsViewModel : BaseViewModel
    {
        public String PatientID;
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String PatientBill { get; set; }
        public String textValidation { get; set; }
        // Edit
        public ICommand editAppointmentPatient { get; set; }
        public ICommand deleteAppointmentPatient { get; set; }
        public String EditPatientNameTextBox { get; set; }
        public String EditPatientAddressTextBox { get; set; }
        public DateTime EditPatientBirthDatePicker { get; set; }
        public String EditPatientTypeComboBox { get; set; }
        public String EditPatientDiagnosisTextBox { get; set; }

        // lists
        public String DoctorsNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsList { get; set; }
        public ObservableCollection<ComboBoxPairs> AppointmentsList { get; set; }
        public String AppointmentsNumber { get; set; }

        public AppointmentPatientDetailsViewModel(String id)
        {
            PatientBill = ((AppointmentPatient)(Hospital.Patients[id])).getBill().ToString("0.00") + '$';
            PatientID = id;
            DoctorsList = new ObservableCollection<ComboBoxPairs>();
            AppointmentsList = new ObservableCollection<ComboBoxPairs>();
            foreach (Doctor doctor in Hospital.Patients[id].Doctors.Values)
            {  
                DoctorsList.Add( new ComboBoxPairs(doctor.ID, doctor.Name));
            }
            DoctorsNumber = "Doctors: " + Hospital.Patients[id].Doctors.Count.ToString();
            foreach(Appointment appointment in ((AppointmentPatient)Hospital.Patients[id]).Appointments.Values)
            {
                AppointmentsList.Add(new ComboBoxPairs(appointment.ID, ("Date: " + appointment.Date.ToString() + " | " +appointment.Doctor.Name)));
            }
            AppointmentsNumber = "Appointments: " + ((AppointmentPatient)Hospital.Patients[id]).Appointments.Count().ToString();
          
            // Edit Content
            editAppointmentPatient = new RelayCommand(EditAppointmentPatient);
            EditPatientNameTextBox = Hospital.Patients[id].Name;
            EditPatientAddressTextBox = Hospital.Patients[id].Address;
            EditPatientBirthDatePicker = Hospital.Patients[id].BirthDate;
            EditPatientDiagnosisTextBox = Hospital.Patients[id].Diagnosis;
            EditPatientTypeComboBox = Hospital.Patients[id].GetType() == typeof(Patient) ? "Resident Patient" : "Appointment Patient";
            deleteAppointmentPatient = new RelayCommand(DeleteAppointmentPatient);
        }
        public void EditAppointmentPatient()
        {
            Hospital.Patients[PatientID].Name = PatientName = EditPatientNameTextBox;
            Hospital.Patients[PatientID].Address = PatientAddress = EditPatientAddressTextBox;
            PatientBirthDate = EditPatientBirthDatePicker.ToShortDateString();
            Hospital.Patients[PatientID].BirthDate = EditPatientBirthDatePicker;
            if (!String.IsNullOrEmpty(EditPatientDiagnosisTextBox))
            {
                Hospital.Patients[PatientID].Diagnosis = EditPatientDiagnosisTextBox;
                PatientDiagnosis = EditPatientDiagnosisTextBox;
            }

            HospitalDB.UpdatePatient(Hospital.Patients[PatientID]);
            Home.ViewModel.CloseRootDialog();

        }

        public async void DeleteAppointmentPatient()
        {
            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {
                // Delete Logic Here
            HospitalDB.DeletePatient(PatientID);
            Hospital.DeletePatient(PatientID);
            Home.ViewModel.Content = new PatientsViewModel();            
            }
        }
    }
}
