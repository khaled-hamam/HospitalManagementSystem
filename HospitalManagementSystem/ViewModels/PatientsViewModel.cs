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
    public class PatientsViewModel : BaseViewModel
    {
        public String PatientNameTextBox { get; set; }
        public String PatientAddressTextBox { get; set; }
        public ObservableCollection<PatientCardViewModel> Patients { get; set; }
        public bool ValidateName()
        {
            PatientNameTextBox = (PatientNameTextBox != null)? PatientNameTextBox.Trim() : "";
            return !(PatientNameTextBox == "");
        }
        public bool ValidateAddress()
        {
            PatientAddressTextBox = (PatientAddressTextBox != null) ? PatientAddressTextBox.Trim() : "";
            return !(PatientAddressTextBox == "");
        }
        public PatientsViewModel()
        {
            Patients = new ObservableCollection<PatientCardViewModel>();
            foreach (Patient patient in Hospital.Patients)
            {
                Patients.Add(
                    new PatientCardViewModel
                    {
                        Name = patient.Name,
                        Type = (patient.GetType() == typeof(Patient)) ? "Resident" : "Appointment",
                        ShortDiagnosis = patient.Diagnosis
                    }
                );
            }
        }
    }
}
