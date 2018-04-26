using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentPatientDetailsViewModel : BaseViewModel
    {
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String AppointmentDate { get; set; }
        public String AppointmentDoctor { get; set; }
        public String PatientBill { get; set; }
        public ObservableCollection<String> DoctorsNumber { get; set; }
        public ObservableCollection<String> DoctorsList { get; set; }

        public void EditAppointmentPatient()
        {

        }

        public void DeleteAppointmentPatient()
        {

        }
    }
}
