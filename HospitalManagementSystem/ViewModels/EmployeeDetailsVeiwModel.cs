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
        public String EmployeeName { get; set; }
        public String EmployeeAddress { get; set; }
        public String EmployeeBirthDate { get; set; }
        public String EmployeeDepartment { get; set; }
        public String EmployeeEmploymentDate { get; set; }
        public String EmployeeSalary { get; set; }
        public ObservableCollection<String> PatientsList { get; set; }
        public ObservableCollection<String> AppointmentsList { get; set; }
        public ObservableCollection<String> RoomsList { get; set; }
        public ObservableCollection<String> PatientsNumber { get; set; }
        public ObservableCollection<String> AppointmentsNumber { get; set; }

        public String EditEmployeeNameTextBox { get; set; }
        public String EditEmployeeAddressTextBox { get; set; }
        public String EditEmployeeSalaryTextBox { get; set; }
        public ComboBoxPairs EditEmployeeDepartment { get; set; }
        public String EditEmployeeRole { get; set; }
        public DateTime EditEmployeeDatePicker { get; set; }

        public List<ComboBoxPairs> EditDepartmentComboBoxItems;

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
            EditEmployeeDatePicker = DateTime.Today;
            EditDepartmentComboBoxItems = new List<ComboBoxPairs>();
            foreach (Department department in Hospital.Departments.Values)
            {
                EditDepartmentComboBoxItems.Add(new ComboBoxPairs(department.ID, department.Name));
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
