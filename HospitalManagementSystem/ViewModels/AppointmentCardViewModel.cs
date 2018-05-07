using System;


namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentCardViewModel : BaseViewModel
    {
        public String PatientName { get; set; }
        public String DoctorName { get; set; }
        public String AppointmentDate { get; set; }
        public String Duration { get; set; }
        public String appointmentBill { get; set; }
    }
}
