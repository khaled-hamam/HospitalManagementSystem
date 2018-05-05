using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Edit
        public ICommand editAppointmentPatient { get; set; }
        public String EditPatientNameTextBox { get; set; }
        public String EditPatientAddressTextBox { get; set; }
        public DateTime EditPatientBirthDatePicker { get; set; }
        public String EditPatientTypeComboBox { get; set; }
        public String EditPatientDiagnosisTextBox { get; set; }
        public String EditPatientBillTextBox { get; set; }

        // lists
        public String DoctorsNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsList { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> AppointmentsList { get; set; }
        public String AppointmentsNumber { get; set; }
        public ICommand assignDoctor { get; set; }
        public ComboBoxPairs DoctorComboBox { get; set; }


        public AppointmentPatientDetailsViewModel(String id)
        {
            PatientID = id;
            DoctorsList = new ObservableCollection<ComboBoxPairs>();
            AppointmentsList = new ObservableCollection<ComboBoxPairs>();
            DoctorsComboBox = new ObservableCollection<ComboBoxPairs>();
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

            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.GetType() == typeof(Doctor))
                    DoctorsComboBox.Add(new ComboBoxPairs(employee.ID, employee.Name));
            }
            // list Content
            assignDoctor = new RelayCommand(AssignDoctor);
            DoctorComboBox = new ComboBoxPairs("Key", "Value");
            // Edit Content
            editAppointmentPatient = new RelayCommand(EditAppointmentPatient);
            EditPatientNameTextBox = PatientName;
            EditPatientAddressTextBox = PatientAddress;
            EditPatientBillTextBox = PatientBill;
            EditPatientBirthDatePicker = Hospital.Patients[id].BirthDate;
            EditPatientDiagnosisTextBox = PatientDiagnosis;
            EditPatientTypeComboBox = PatientType;
        }
        public void EditAppointmentPatient()
        {
           Hospital.Patients[PatientID].Name = PatientName = EditPatientNameTextBox;
            Hospital.Patients[PatientID].Address = PatientAddress = EditPatientAddressTextBox;
            // ((AppointmentPatient)Hospital.Patients[PatientID]).getBill() = PatientBill = EditPatientBillTextBox;
            PatientBirthDate = EditPatientBirthDatePicker.ToShortDateString();
            Hospital.Patients[PatientID].BirthDate = EditPatientBirthDatePicker;
            Hospital.Patients[PatientID].Diagnosis = PatientDiagnosis = EditPatientDiagnosisTextBox;
            //Hospital.Patients[PatientID].
            HospitalDB.UpdatePatient(Hospital.Patients[PatientID]);
        }

        public void DeleteAppointmentPatient()
        {

        }

        public void AssignDoctor()
        {
            DoctorsList.Add(new ComboBoxPairs(DoctorComboBox.Key, DoctorComboBox.Value));
            foreach (Doctor doctor in Hospital.Employees.Values)
            {
                if (doctor.ID == DoctorComboBox.Key)
                {
                    Hospital.Patients[PatientID].Doctors.Add(doctor.ID, doctor);
                    DoctorsNumber = "Doctors: " + Hospital.Patients[PatientID].Doctors.Count().ToString();
                    break;
                }
            }
            Home.ViewModel.CloseRootDialog();
        }
    }
}
