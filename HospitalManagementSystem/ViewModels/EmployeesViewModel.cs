using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Linq;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        /// <summary>
        /// Items Properties
        /// </summary>
        public ObservableCollection<EmployeeCardViewModel> Employees { get; set; }
        public ObservableCollection<EmployeeCardViewModel> FilteredEmployees { get; set; }

        /// <summary>
        /// Search Bar Properties
        /// </summary>
        public String SearchQuery { get; set; }
        
        /// <summary>
        /// Add Dialog Properites
        /// </summary>
        public String EmployeeNameTextBox { get; set;  }
        public String EmployeeAddressTextBox { get; set; }
        public String EmployeeSalaryTextBox { get; set; }
        public String EmployeeDepartment { get; set; }
        public String EmployeeRole { get; set; }

        public ICommand SearchAction { get; set; }

        public EmployeesViewModel()
        {
            SearchAction = new RelayCommand(Search);
            Employees = new ObservableCollection<EmployeeCardViewModel>();
            foreach (Employee employee in Hospital.Employees.Values)
            {
                Employees.Add(
                    new EmployeeCardViewModel
                    {
                        Name = employee.Name,
                        Role = (employee.GetType() == typeof(Doctor))? "Doctor" : "Nurse",
                        Department = employee.Department.Name,
                        Salary = employee.Salary.ToString() + '$'
                    }
                );
            }
            FilteredEmployees = new ObservableCollection<EmployeeCardViewModel>(Employees);
        }

        private void Search()
        {
            if (String.IsNullOrEmpty(SearchQuery))
            {
                FilteredEmployees = new ObservableCollection<EmployeeCardViewModel>(Employees);
                return;
            }

            FilteredEmployees = new ObservableCollection<EmployeeCardViewModel>(
                Employees.Where(employee => employee.Name.ToLower().Contains(SearchQuery))
            );
        }

        public bool ValidateName()
        {
            EmployeeNameTextBox = (EmployeeNameTextBox != null) ? EmployeeNameTextBox.Trim() : "";
            EmployeeAddressTextBox = (EmployeeAddressTextBox != null) ? EmployeeAddressTextBox.Trim() : "";
            EmployeeSalaryTextBox = (EmployeeSalaryTextBox != null) ? EmployeeSalaryTextBox.Trim() : "";
            EmployeeDepartment = (EmployeeDepartment != null) ? EmployeeDepartment.Trim() : "";
            EmployeeRole = (EmployeeRole != null) ? EmployeeRole.Trim() : "";
            if (EmployeeNameTextBox == "") return false;
            if (EmployeeAddressTextBox == "") return false;
            if (EmployeeSalaryTextBox == "") return false;
            if (EmployeeDepartment == "") return false;
            if (EmployeeRole == "") return false;
            return true;
        }
    }
}
