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
    public class EmployeeDetailsVeiwModel : BaseViewModel
    {
        /// <summary>
        /// Details View Properites
        /// </summary>
       
            // Main Page
        public String EmployeeName { get; set; }
        public String EmployeeAddress { get; set; }
        public String EmployeeBirthDate { get; set; }
        public String EmployeeDepartment { get; set; }
        public String EmployeeEmploymentDate { get; set; }
        public String EmployeeSalary { get; set; }
            // Main Lists
        public ObservableCollection<ComboBoxPairs> PatientsList { get; set; }
        public ObservableCollection<ComboBoxPairs> AppointmentsList { get; set; }
        public ObservableCollection<ComboBoxPairs> RoomsList { get; set; }
        public String PatientsNumber { get; set; }
        public String AppointmentsNumber { get; set; }
        public String RoomsNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> PatientsComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> RoomsComboBox { get; set; }
        // Edit Content
        public String EditEmployeeNameTextBox { get; set; }
        public String EditEmployeeAddressTextBox { get; set; }
        public String EditEmployeeSalaryTextBox { get; set; }
        public ComboBoxPairs EditEmployeeDepartment { get; set; }
        public String EditEmployeeRole { get; set; }
        public DateTime EditEmployeeDatePicker { get; set; }
        public ObservableCollection<ComboBoxPairs> EditDepartmentComboBox { get; set; }
            // Haget el condition
        private String employeeRole { get; set; }
        public Visibility IsDoctor { get; set; }
        public Visibility IsNurse { get; set; }
        public String EmployeeRole {
            get => employeeRole;
            set {
                if (value == "Doctor")
                {
                    IsDoctor = Visibility.Visible;
                    IsNurse = Visibility.Collapsed;
                }
                else
                {
                    IsNurse = Visibility.Visible;
                    IsDoctor = Visibility.Collapsed;
                }
                employeeRole = value;
            }
        }
        public EmployeeDetailsVeiwModel()
        {
            
        }
        public EmployeeDetailsVeiwModel(String id)
        {
            EditEmployeeDatePicker = DateTime.Today;

            // set Main Page Information
            EmployeeAddress = Hospital.Employees[id].Address;
            EmployeeBirthDate = Hospital.Employees[id].BirthDate.ToShortDateString();
            EmployeeEmploymentDate = Hospital.Employees[id].EmploymentDate.ToShortDateString();
            EmployeeRole = (Hospital.Employees[id].GetType() == typeof(Doctor)) ? "Doctor" : "Nurse";
            
            // Lists
            PatientsList = new ObservableCollection<ComboBoxPairs>();
            RoomsList = new ObservableCollection<ComboBoxPairs>();
            AppointmentsList = new ObservableCollection<ComboBoxPairs>();
            if (Hospital.Employees[id].GetType() == typeof(Doctor))
            {
                foreach(Patient patient in ((Doctor)Hospital.Employees[id]).Patients.Values)
                {
                    PatientsList.Add(new ComboBoxPairs(patient.ID, patient.Name));
                }
                PatientsNumber = "Patients: " + ((Doctor)Hospital.Employees[id]).Patients.Count.ToString();
                foreach (Appointment appointment in ((Doctor)Hospital.Employees[id]).Appointments.Values)
                {
                    AppointmentsList.Add(new ComboBoxPairs(appointment.ID, ("Date: " + appointment.Date.ToString() + " | " + appointment.Patient.Name)));
                }
                AppointmentsNumber = "Appointmens: " + ((Doctor)Hospital.Employees[id]).Appointments.Count().ToString();
            }
            else
            {
                foreach (Patient patient in ((Nurse)Hospital.Employees[id]).Patients.Values)
                {
                    PatientsList.Add(new ComboBoxPairs(patient.ID, patient.Name));
                }
                PatientsNumber = "Patients: " + ((Nurse)Hospital.Employees[id]).Patients.Count.ToString();

                foreach(Room room in ((Nurse)Hospital.Employees[id]).Rooms.Values)
                {
                    RoomsList.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
                }
                RoomsNumber = "Rooms: " + ((Nurse)Hospital.Employees[id]).Rooms.Count().ToString();
            }
                // Lists content
            PatientsComboBox = new ObservableCollection<ComboBoxPairs>();
            RoomsComboBox = new ObservableCollection<ComboBoxPairs>();
            EditDepartmentComboBox = new ObservableCollection<ComboBoxPairs>();
            foreach (Patient patient in Hospital.Patients.Values)
            {
                PatientsComboBox.Add(new ComboBoxPairs(patient.ID, patient.Name));
            }

            foreach(Room room in Hospital.Rooms.Values)
            {
                RoomsComboBox.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }

            foreach (Department department in Hospital.Departments.Values)
            {
                EditDepartmentComboBox.Add(new ComboBoxPairs(department.ID, department.Name));
            }
        }
        public void EditEmployee()
        {

        }
        public void DeleteEmployee()
        {

        }

    }
}
