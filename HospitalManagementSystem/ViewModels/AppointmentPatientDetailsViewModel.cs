using HospitalManagementSystem.Models;
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
        public String PatientBill { get; set; }

        public String DoctorsNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsList { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> AppointmentsList { get; set; }
        public String AppointmentsNumber { get; set; }

        public AppointmentPatientDetailsViewModel(String id)
        {
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

        }
        public void EditAppointmentPatient()
        {

        }

        public void DeleteAppointmentPatient()
        {

        }
    }
}
