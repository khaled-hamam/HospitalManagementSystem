using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;
using HospitalManagementSystem.Services;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using HospitalManagementSystem.Views;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {

        public String SearchQuery { get; set; }

        public ComboBoxPairs PatientNameComboBox { get; set; }
        public ComboBoxPairs DoctorNameComboBox { get; set; }
        public String AppointmentDuration { get; set; }
        public DateTime AppointmentDatePicker { get; set; }
        public DateTime AppointmentTimePicker { get; set; }
        public String datePickerString { get; set; }
        public String timePickerString { get; set; }
        public String textValidation { get; set; }
        public List<ComboBoxPairs> patientsComboBoxItems;
        public List<ComboBoxPairs> doctorsComboBoxItems;

        public ICommand SearchAction { get; set; }

        public static ObservableCollection<AppointmentCardViewModel> FilteredAppointments { get; set; }
        public static ObservableCollection<AppointmentCardViewModel> Appointments { get; set; }

        public bool Validate()
        {

            AppointmentDuration = (AppointmentDuration != null) ? AppointmentDuration.Trim() : "";

            if (AppointmentDuration == "")
            {
                textValidation = "Can't Have empty Values";
                return false;
            }
            if (PatientNameComboBox == null)
            {
                textValidation = "Can't Have empty Values";

                return false;
            }
            if (DoctorNameComboBox == null)
            {
                textValidation = "Can't Have empty Values";

                return false;
            }
            for (int i = 0; i < AppointmentDuration.Length; i++)
            {
                if (AppointmentDuration[i] >= 'a' && AppointmentDuration[i] <= 'z')
                {
                    textValidation = "Can't Have empty Values";
                    return false;
                }
            }
            bool foundPatient = false, foundDoctor = false;
            foreach (Patient patient in Hospital.Patients.Values)
            {
                if (patient.GetType() == typeof(AppointmentPatient) && PatientNameComboBox.Value == patient.Name)
                {
                    foundPatient = true;
                }
            }
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.GetType() == typeof(Doctor) && DoctorNameComboBox.Value == employee.Name)
                {
                    foundDoctor = true;
                }
            }
            if (!foundPatient || !foundDoctor)
            {
                return false;
            }
            return true;
        }
        public AppointmentsViewModel()
        {
            AppointmentDatePicker = DateTime.Today;


            patientsComboBoxItems = new List<ComboBoxPairs>();
            doctorsComboBoxItems = new List<ComboBoxPairs>();

            SearchAction = new RelayCommand(Search);

            foreach (Patient patient in Hospital.Patients.Values)
            {
                if (patient.GetType() == typeof(AppointmentPatient))
                {
                    patientsComboBoxItems.Add(new ComboBoxPairs(patient.ID, patient.Name));
                }
            }

            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.GetType() == typeof(Doctor))
                {
                    doctorsComboBoxItems.Add(new ComboBoxPairs(employee.ID, employee.Name));
                }
            }
            Appointments = new ObservableCollection<AppointmentCardViewModel>();

            foreach (Appointment appointment in Hospital.Appointments.Values)
            {
                Appointments.Add(
                    new AppointmentCardViewModel
                    {
                        ID = appointment.ID,
                        PatientName = appointment.Patient.Name,
                        DoctorName = appointment.Doctor.Name,
                        Duration = appointment.Duration.ToString() + " mins",
                        AppointmentDate = appointment.Date.ToString(),
                        appointmentBill = appointment.Bill.ToString("#0.00") + '$'
                    }
                );
            }
            FilteredAppointments = new ObservableCollection<AppointmentCardViewModel>(Appointments);

        }

        public void DeleteAppointment(String ID)
        {
            Hospital.Appointments[ID].Doctor.removePatient(Hospital.Appointments[ID].Patient.ID);
            Hospital.DeleteAppointment(ID);
            for (int i = 0; i < FilteredAppointments.Count; ++i)
            {
                if (FilteredAppointments[i].ID == ID)
                {
                    FilteredAppointments.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < Appointments.Count; ++i)
            {
                if (Appointments[i].ID == ID)
                {
                    Appointments.RemoveAt(i);
                    break;
                }
            }
            HospitalDB.DeleteAppointment(ID);
        }
        
        public void Search()
        {
            if (String.IsNullOrEmpty(SearchQuery))
            {
                FilteredAppointments = new ObservableCollection<AppointmentCardViewModel>(Appointments);
                return;
            }

            FilteredAppointments = new ObservableCollection<AppointmentCardViewModel>(
                Appointments.Where(Appointment => Appointment.AppointmentDate.ToString().Contains(SearchQuery))
            );
        }
        public void addAppointment()
        {
            datePickerString = AppointmentDatePicker.ToShortDateString();
            datePickerString += " ";
            datePickerString += AppointmentTimePicker.ToShortTimeString();
            Appointment newAppointment = new Appointment
            {
            
                Patient = (AppointmentPatient)Hospital.Patients[PatientNameComboBox.Key],
                Doctor = (Doctor)Hospital.Employees[DoctorNameComboBox.Key],
                Duration = Int32.Parse(AppointmentDuration),
                Date = DateTime.Parse(datePickerString),
                Bill =((double.Parse(AppointmentDuration) / 60.0)) * Hospital.Config.AppointmentHourPrice
            };
            if (!(((Doctor)Hospital.Employees[DoctorNameComboBox.Key]).isAvailable(newAppointment)))
            {
                textValidation = "Doctor is not available at this time";
                return;
            }
            Appointments.Add(
                new AppointmentCardViewModel
                {
                    ID = newAppointment.ID,
                    PatientName = newAppointment.Patient.Name,
                    DoctorName = newAppointment.Doctor.Name,
                    Duration = newAppointment.Duration.ToString(),
                    AppointmentDate = newAppointment.Date.ToString(),
                    appointmentBill = newAppointment.Bill.ToString("0.00") + '$'
                });
            FilteredAppointments.Add(
                new AppointmentCardViewModel
                {
                    ID = newAppointment.ID,
                    PatientName = newAppointment.Patient.Name,
                    DoctorName = newAppointment.Doctor.Name,
                    Duration = newAppointment.Duration.ToString(),
                    AppointmentDate = newAppointment.Date.ToString(),
                    appointmentBill = newAppointment.Bill.ToString("0.00") + '$'
                });
            Hospital.Appointments.Add(newAppointment.ID, newAppointment);
            Hospital.Appointments[newAppointment.ID].Patient.addAppointment(newAppointment);
            Hospital.Appointments[newAppointment.ID].Doctor.addAppointment(newAppointment);
            Hospital.Appointments[newAppointment.ID].Doctor.addPatient(newAppointment.Patient);
            HospitalDB.InsertAppointment(newAppointment);
            Home.ViewModel.CloseRootDialog();



        }
    }
}
