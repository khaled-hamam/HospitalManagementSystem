using HospitalManagementSystem.Models;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class PatientCardViewModel : BaseViewModel
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String ShortDiagnosis { get; set; }

        public ICommand NavigateToDetailsAction { get; set; }

        public PatientCardViewModel()
        {
            NavigateToDetailsAction = new RelayCommand(navigateToDetails);
        }

        public void navigateToDetails()
        {
            if (Hospital.Patients[ID].GetType() == typeof(ResidentPatient))
                Home.ViewModel.Content = new ResidentPatientDetailsViewModel(ID)
                {
                    PatientName = Hospital.Patients[ID].Name,
                    PatientAddress = Hospital.Patients[ID].Address,
                    PatientBirthDate = Hospital.Patients[ID].BirthDate.ToShortDateString(),
                    PatientBill = Hospital.Patients[ID].getBill().ToString(),
                    PatientDiagnosis = Hospital.Patients[ID].Diagnosis,
                    PatientRoomNumber = ((ResidentPatient)(Hospital.Patients[ID])).Room.RoomNumber.ToString(),
                    PatientType = "Resdident Patient",
                };
            else
            {
                Home.ViewModel.Content = new AppointmentPatientDetailsViewModel(ID)
                {
                    PatientName = Hospital.Patients[ID].Name,
                    PatientAddress = Hospital.Patients[ID].Address,
                    PatientBirthDate = Hospital.Patients[ID].BirthDate.ToShortDateString(),
                    PatientBill = Hospital.Patients[ID].getBill().ToString(),
                    PatientDiagnosis = Hospital.Patients[ID].Diagnosis,
                    PatientType = "Appointment Patient",
                };
            }
        }
    }
}
