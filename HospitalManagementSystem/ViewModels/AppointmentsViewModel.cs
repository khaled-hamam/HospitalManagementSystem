using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        public String PatientNameTextBox { get; set; }
        public String DoctorNameTextBox { get; set; }
        public String AppointmentDuration { get; set; }

        public ObservableCollection<AppointmentCardViewModel> Appointments { get; set; }
        public bool Validate()
        {
            PatientNameTextBox = (PatientNameTextBox != null) ? PatientNameTextBox.Trim() : "";
            DoctorNameTextBox = (DoctorNameTextBox != null) ? DoctorNameTextBox.Trim() : "";
            AppointmentDuration = (AppointmentDuration != null) ? AppointmentDuration.Trim() : "";

            if (PatientNameTextBox == "")
            {
                return false;
            }
            if (DoctorNameTextBox == "")
            {
                return false;
            }
            if (AppointmentDuration == "")
            {
                return false;
            }
            return true;
        }
        public AppointmentsViewModel()
        {
            Appointments = new ObservableCollection<AppointmentCardViewModel>();
            foreach (Appointment appointment in Hospital.Appointments.Values)
            {
                Appointments.Add(
                    new AppointmentCardViewModel
                    {
                        PatientName = appointment.Patient.Name,
                        DoctorName = appointment.Doctor.Name,
                        Duration = appointment.Duration.ToString(),
                        AppointmentDate = appointment.Date.ToString()
                    }
                );
            }
        }
    }
}
