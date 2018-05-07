using System;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentCardViewModel : BaseViewModel
    {
        public String PatientName { get; set; }
        public String DoctorName { get; set; }
        public String AppointmentDate { get; set; }
        public String Duration { get; set; }
        public String appointmentBill { get; set; }

        public ICommand deleteAppointment { get; set; }

        public AppointmentCardViewModel()
        {
            deleteAppointment = new RelayCommand(DeleteAppointment);
        }

        public void DeleteAppointment()
        {
            // Delete from List ( appointmet Page )

            // Delete from FilteredList

            // delete from hospital

            // deleter from hospital DB
        }
    }
}
