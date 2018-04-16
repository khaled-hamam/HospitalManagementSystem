using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        public ObservableCollection<PatientCardViewModel> Patients { get; set; }
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
