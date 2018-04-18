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
        public string PatientNameTextBox { get; set; }
        public ObservableCollection<PatientCardViewModel> Patients { get; set; }
        public bool Validate()
        {
            PatientNameTextBox = (PatientNameTextBox != null)? PatientNameTextBox.Trim() : "";
            return !(PatientNameTextBox == "");
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
