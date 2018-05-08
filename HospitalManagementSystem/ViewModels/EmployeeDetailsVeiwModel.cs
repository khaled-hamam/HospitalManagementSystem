using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using HospitalManagementSystem.Views.Components;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
        public ComboBoxPairs ListSelectedPatient {get; set;}
        public ComboBoxPairs PatientComboBox { get; set; }
        public ICommand assignRoom { get; set; }
        public ComboBoxPairs ListSelectedRoom { get; set; }
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
        public String textValidation { get; set; }
        public bool isHeadCheck { get; set; }
        //Visibility conditions
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
            Department tempDep = Hospital.Employees[id].Department;
            if(tempDep != null)
            EmployeeDepartment = Hospital.Employees[id].Department.Name;
            else
                EmployeeDepartment = "N/A";
            EmployeeID = id;
            if(Hospital.Employees[id].GetType() == typeof(Doctor) && tempDep != null)
            {
                isHeadCheck = ((Doctor)Hospital.Employees[id]).IsHead;
                if(((Doctor)Hospital.Employees[id]).IsHead == true)
                {
                    
                    EmployeeDepartment += " (Head)";
                }
            }
            // set Main Page & Edit Content Information
            if(Hospital.Employees[EmployeeID].Department != null)
            EditEmployeeDepartment = new ComboBoxPairs(Hospital.Employees[EmployeeID].Department.ID, Hospital.Employees[EmployeeID].Department.Name);
            EditEmployeeNameTextBox = Hospital.Employees[id].Name;
            EditEmployeeAddressTextBox = EmployeeAddress = Hospital.Employees[id].Address;
            EmployeeBirthDate = Hospital.Employees[id].BirthDate.ToShortDateString();
            EmployeeEmploymentDate = Hospital.Employees[id].EmploymentDate.ToShortDateString();
            EditEmployeeSalaryTextBox = Hospital.Employees[id].Salary.ToString();
            if (!(Hospital.Employees[id].Department == null))
                SetEditDepartmentComboBox = Hospital.Employees[id].Department.Name;
            else
                EditEmployeeDepartment = null;
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
                Boolean valid = true;
                if (Hospital.Employees[EmployeeID].GetType() == typeof(Doctor))
                    foreach (Patient invalidPatient in ((Doctor)Hospital.Employees[id]).Patients.Values)
                    {
                        if (patient.ID == invalidPatient.ID)
                        {
                            valid = false;
                        }
                    }
                if (valid)
                   PatientsComboBox.Add(new ComboBoxPairs(patient.ID, patient.Name));
            }

            foreach(Room room in Hospital.Rooms.Values)
            {
                Boolean valid = true;
                if (Hospital.Employees[EmployeeID].GetType() == typeof(Nurse))
                    foreach (Room invalidRoom in ((Nurse)Hospital.Employees[id]).Rooms.Values)
                    {
                        if (room.ID == invalidRoom.ID)
                        {
                            valid = false;
                        }
                    }
                    if(valid)
                     RoomsComboBox.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }

            foreach (Department department in Hospital.Departments.Values)
            {
                if(department != null)
                EditDepartmentComboBox.Add(new ComboBoxPairs(department.ID, department.Name));
            }


        }
        public void EditEmployee()
        {
            if (EditEmployeeDepartment == null)
            {
                textValidation = "Department is not Assigned";
                return;
            }
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
            {
                
               //remove old data
                ((Doctor)Hospital.Employees[EmployeeID]).IsHead = false;
                if (Hospital.Employees[EmployeeID].Department != null)
                {
                    Hospital.Employees[EmployeeID].Department.HeadID = null;
                    Hospital.Employees[EmployeeID].Department.Doctors.Remove(EmployeeID);
                }
                //assign new department to the employee
                Hospital.Employees[EmployeeID].Department = Hospital.Departments[EditEmployeeDepartment.Key];
                // add the employee to the selected department
                Hospital.Departments[EditEmployeeDepartment.Key].Doctors.Add(EmployeeID, (Doctor)Hospital.Employees[EmployeeID]);
                
                //add head to doctor and department
                ((Doctor)Hospital.Employees[EmployeeID]).IsHead = isHeadCheck;
                if(isHeadCheck==false)
                {
                    Hospital.Departments[EditEmployeeDepartment.Key].HeadID = null;
        
                }
                else
                {
                    EmployeeDepartment += " (Head)";
                    Hospital.Departments[EditEmployeeDepartment.Key].HeadID = EmployeeID;

                }
                
               
                HospitalDB.UpdateDoctor((Doctor)Hospital.Employees[EmployeeID]);
            }
            else
            {
                if (Hospital.Employees[EmployeeID].Department != null)
                {
                    Hospital.Employees[EmployeeID].Department.removeNurse(EmployeeID);
                }
                Hospital.Departments[EditEmployeeDepartment.Key].addNurse((Nurse)Hospital.Employees[EmployeeID]);
                Hospital.Employees[EmployeeID].Department = Hospital.Departments[EditEmployeeDepartment.Key];
                HospitalDB.UpdateNurse((Nurse)Hospital.Employees[EmployeeID]);
            }
            Home.ViewModel.CloseRootDialog();

        }
        public async void DeleteEmployee()
        {
            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {
                if (Hospital.Employees[EmployeeID].GetType() == typeof(Doctor))
                {
                    if(((Doctor)Hospital.Employees[EmployeeID]).IsHead == true)
                    {
                        ((Doctor)Hospital.Employees[EmployeeID]).IsHead = false;
                    }
                    HospitalDB.DeleteDoctor(EmployeeID);
                    Hospital.DeleteDoctor(EmployeeID);
                    Home.ViewModel.CloseRootDialog();
                    Home.ViewModel.Content = new EmployeesViewModel();
                }
                else
                {
                    HospitalDB.DeleteNurse(EmployeeID);
                    Hospital.DeleteNurse(EmployeeID);
                    Home.ViewModel.CloseRootDialog();
                    Home.ViewModel.Content = new EmployeesViewModel();
                }
            }
        }
        
        public async void RemovePatient()
        {
            if (Hospital.Employees[EmployeeID].GetType() == typeof(Doctor))
            {
                object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
                if (result.Equals(true))
                {

                    PatientsComboBox.Add(new ComboBoxPairs(ListSelectedPatient.Key, ListSelectedPatient.Value));
                    Hospital.Patients[ListSelectedPatient.Key].removeDoctor(EmployeeID);
                    ((Doctor)Hospital.Employees[EmployeeID]).Patients.Remove(ListSelectedPatient.Key);
                    HospitalDB.DeleteDoctorPatient(EmployeeID, ListSelectedPatient.Key);
                    PatientsList.Remove(ListSelectedPatient);
                    PatientsNumber = $"Patients : {PatientsList.Count}";
                }
            }
        }
        
        public async void RemoveRoom()
        {
            if (Hospital.Employees[EmployeeID].GetType() == typeof(Nurse))
            {
                object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
                if (result.Equals(true))
                {

                    RoomsComboBox.Add(new ComboBoxPairs(ListSelectedRoom.Key, ListSelectedRoom.Value));
                    Hospital.Rooms[ListSelectedRoom.Key].removeNurse(EmployeeID);
                    ((Nurse)Hospital.Employees[EmployeeID]).Rooms.Remove(ListSelectedRoom.Key);
                    foreach(Patient patient in Hospital.Rooms[ListSelectedRoom.Key].Patients.Values)
                    {
                        ((Nurse)Hospital.Employees[EmployeeID]).Patients.Remove(patient.ID);
                        for(int i=0; i<PatientsList.Count; i++)
                        {
                            if(PatientsList[i].Key==patient.ID)
                            {
                                PatientsList.Remove(PatientsList[i]);
                            }
                        }
                    }
                    HospitalDB.DeleteNurseRoom(EmployeeID, ListSelectedRoom.Key);
                    RoomsList.Remove(ListSelectedRoom);
                    PatientsList.Remove(ListSelectedPatient);
                    RoomsNumber = $"Rooms : {RoomsList.Count}";
                }
            }
        }
        
        public void AssignPatient()
        {
            if (PatientComboBox.Key == "Key" && PatientComboBox.Value == "Value")
            {
                System.Windows.MessageBox.Show("Invalid Input");
            }
            else
            {
                PatientsList.Add(new ComboBoxPairs(PatientComboBox.Key, PatientComboBox.Value));

                foreach (Patient patient in Hospital.Patients.Values)
                {
                    if (patient.ID == PatientComboBox.Key)
                    {
                        if (EmployeeRole == "Doctor")
                        {
                            ((Doctor)Hospital.Employees[EmployeeID]).Patients.Add(patient.ID, patient);
                            Hospital.Patients[patient.ID].assignDoctor(((Doctor)Hospital.Employees[EmployeeID]));
                            HospitalDB.InsertDoctorPatient(EmployeeID, patient.ID);
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
                PatientsComboBox.Remove(PatientComboBox);
                Home.ViewModel.CloseRootDialog();
            }
        }

        public void AssignRoom()
        {
            
            RoomsList.Add(new ComboBoxPairs(RoomComboBox.Key, RoomComboBox.Value));
            foreach(Room room in Hospital.Rooms.Values)
            {
                if(room.ID == RoomComboBox.Key)
                {
                    Hospital.Rooms[RoomComboBox.Key].addNurse(((Nurse)Hospital.Employees[EmployeeID]));
                    ((Nurse)Hospital.Employees[EmployeeID]).Rooms.Add(room.ID, room);
                    HospitalDB.InsertNurseRoom(EmployeeID, room.ID);
                    RoomsNumber = "Rooms: " + ((Nurse)Hospital.Employees[EmployeeID]).Rooms.Count().ToString();
                    break;

                }
            }
            RoomsComboBox.Remove(RoomComboBox);
            Home.ViewModel.CloseRootDialog();
        }
    }
}
