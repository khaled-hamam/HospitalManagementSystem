using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        public String EmployeeNameTextBox { get; set;  }
        public String EmployeeAddressTextBox { get; set; }
        public String EmployeeSalaryTextBox { get; set; }
        public String EmployeeDepartment { get; set; }
        public String EmployeeRole { get; set; }
        public ObservableCollection<EmployeeCardViewModel> Employees { get; set; }

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
            if( EmployeeRole == "") return false;
            return true;
        }
        
        public EmployeesViewModel()
        {

            Employees = new ObservableCollection<EmployeeCardViewModel>();
            foreach (Employee employee in Hospital.Employees)
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
        }
    }
}
