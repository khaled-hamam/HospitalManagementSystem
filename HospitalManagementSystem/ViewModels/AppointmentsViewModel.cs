using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        public ObservableCollection<AppointmentCardViewModel> Appointments { get; set; }

        public AppointmentsViewModel()
        {
            Appointments = new ObservableCollection<AppointmentCardViewModel>();
            foreach (Appointment appointment in Hospital.Appointments)
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
