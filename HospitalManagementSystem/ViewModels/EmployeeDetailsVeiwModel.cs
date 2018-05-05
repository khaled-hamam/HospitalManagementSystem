using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeeDetailsVeiwModel : BaseViewModel
    {
        /// <summary>
        /// Details View Properites
        /// </summary>

        public String EmployeeID;
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

        // List Content
        public ICommand assignPatient { get; set; }
        public ComboBoxPairs PatientComboBox { get; set; }
        public ICommand assignRoom { get; set; }
        public ComboBoxPairs RoomComboBox { get; set; }
        // Edit Content
        public ICommand editEmployee { get; set; }
        public ICommand deleteEmployee { get; set; }
        public String EditEmployeeNameTextBox { get; set; }
        public String EditEmployeeAddressTextBox { get; set; }
        public String EditEmployeeSalaryTextBox { get; set; }
        public String SetEditDepartmentComboBox { get; set; }
        public ComboBoxPairs EditEmployeeDepartment { get; set; }
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
            EmployeeID = id;
            // set Main Page & Edit Content Information
            EditEmployeeDepartment = new ComboBoxPairs("Key", "Value");
            EditEmployeeNameTextBox = Hospital.Employees[id].Name;
            EditEmployeeAddressTextBox = EmployeeAddress = Hospital.Employees[id].Address;
            EmployeeBirthDate = Hospital.Employees[id].BirthDate.ToShortDateString();
            EmployeeEmploymentDate = Hospital.Employees[id].EmploymentDate.ToShortDateString();
            EditEmployeeSalaryTextBox = Hospital.Employees[id].Salary.ToString();
            SetEditDepartmentComboBox = Hospital.Employees[id].Department.Name;
            EditEmployeeDatePicker = Hospital.Employees[id].BirthDate;

            // Edit 
            editEmployee = new RelayCommand(EditEmployee);
            deleteEmployee = new RelayCommand(DeleteEmployee);
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
            assignPatient = new RelayCommand(AssignPatient);
            assignRoom = new RelayCommand(AssignRoom);
            PatientComboBox = new ComboBoxPairs("Key", "Value");
            RoomComboBox = new ComboBoxPairs("key", "value");
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
                EmployeeName = EditEmployeeNameTextBox;
                Hospital.Employees[EmployeeID].Name = EditEmployeeNameTextBox;
                EmployeeAddress = EditEmployeeAddressTextBox;
                Hospital.Employees[EmployeeID].Address = EditEmployeeAddressTextBox;
                EmployeeBirthDate = EditEmployeeDatePicker.ToShortDateString();
                Hospital.Employees[EmployeeID].BirthDate = EditEmployeeDatePicker;
                EmployeeSalary = EditEmployeeSalaryTextBox;
                Hospital.Employees[EmployeeID].Salary = double.Parse(EditEmployeeSalaryTextBox);
                EmployeeDepartment = EditEmployeeDepartment.Value;
            if (Hospital.Employees[EmployeeID].GetType() == typeof(Doctor))
                HospitalDB.UpdateDoctor((Doctor)Hospital.Employees[EmployeeID]);
            else
                HospitalDB.UpdateNurse((Nurse)Hospital.Employees[EmployeeID]);
            Home.ViewModel.CloseRootDialog();

        }
        public void DeleteEmployee()
        {

        }

        public void AssignPatient()
        {
            PatientsList.Add(new ComboBoxPairs(PatientComboBox.Key, PatientComboBox.Value));
            
            foreach (Patient patient in Hospital.Patients.Values)
            {
                if (patient.ID == PatientComboBox.Key)
                {
                    if (EmployeeRole == "Doctor")
                    {
                        ((Doctor)Hospital.Employees[EmployeeID]).Patients.Add(patient.ID, patient);
                        PatientsNumber = "Patients: " + ((Doctor)Hospital.Employees[EmployeeID]).Patients.Count.ToString();
                    }
                    else
                    {
                        ((Nurse)Hospital.Employees[EmployeeID]).Patients.Add(patient.ID, patient);
                        PatientsNumber = "Patients: " + ((Nurse)Hospital.Employees[EmployeeID]).Patients.Count.ToString();
                    }
                    break;
                }
            }
            Home.ViewModel.CloseRootDialog();
        }

        public void AssignRoom()
        {
            RoomsList.Add(new ComboBoxPairs(RoomComboBox.Key, RoomComboBox.Value));
            foreach(Room room in Hospital.Rooms.Values)
            {
                if(room.ID == RoomComboBox.Key)
                {
                    ((Nurse)Hospital.Employees[EmployeeID]).Rooms.Add(room.ID, room);
                    RoomsNumber = "Rooms: " + ((Nurse)Hospital.Employees[EmployeeID]).Rooms.Count().ToString();
                }
                break;
            }
            Home.ViewModel.CloseRootDialog();
        }
    }
}
