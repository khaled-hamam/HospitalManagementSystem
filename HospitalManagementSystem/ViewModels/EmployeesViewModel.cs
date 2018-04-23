using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        public String EmployeeNameTextBox { get; set;  }
        public String EmployeeAddressTextBox { get; set; }
        public String EmployeeSalaryTextBox { get; set; }
        public ComboBoxPairs EmployeeDepartment { get; set; }
        public String EmployeeRole { get; set; }
        public DateTime EmployeeDatePicker { get; set; }
        public ObservableCollection<EmployeeCardViewModel> Employees { get; set; }

        public List<ComboBoxPairs> ComboBoxItems;

        public ICommand SearchAction { get; set; }

        public EmployeesViewModel()
        {
            EmployeeDatePicker = DateTime.Today;
            ComboBoxItems = new List<ComboBoxPairs>();
            SearchAction = new RelayCommand(Search);
            foreach(Department department in Hospital.Departments.Values)
            {
                ComboBoxItems.Add(new ComboBoxPairs(department.ID, department.Name));
            }

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
        }

        private void Search()
        {
            Console.WriteLine("Searching Not Yet Implemented...");
        }

        public void addEmployee()
        {
            if (EmployeeRole == "Doctor") {

                Doctor newDoctor = new Doctor
                {
                    Name = EmployeeNameTextBox,
                    Salary = Double.Parse(EmployeeSalaryTextBox),
                    Department = Hospital.Departments[EmployeeDepartment.Key],
                    Address = EmployeeAddressTextBox, 
         
                };

                Employees.Add(
                    new EmployeeCardViewModel
                    {
                        Name = newDoctor.Name,
                        Salary = $"{newDoctor.Salary}$",
                        Department = newDoctor.Department.ToString(),
                        Role = "Doctor"
                    }
                   );

                Hospital.Employees.Add(newDoctor.ID, newDoctor);
                HospitalDB.InsertDoctor(newDoctor);
            }
            else if(EmployeeRole=="Nurse")
            {
                Nurse newNurse = new Nurse
                {
                    Name = EmployeeNameTextBox,
                    Salary = Double.Parse(EmployeeSalaryTextBox),
                    Department = Hospital.Departments[EmployeeDepartment.Key],
                    Address = EmployeeAddressTextBox,
                };


                Employees.Add(
                    new EmployeeCardViewModel
                    {
                        Name = newNurse.Name,
                        Salary = $"{newNurse.Salary}$",
                        Department = newNurse.Department.ToString(),
                        Role = "Nurse"
                    }
                   );
                Hospital.Employees.Add(newNurse.ID, newNurse);
                HospitalDB.InsertNurse(newNurse);

            }

        }
        

        public bool ValidateName()
        {
            EmployeeNameTextBox = (EmployeeNameTextBox != null) ? EmployeeNameTextBox.Trim() : "";
            EmployeeAddressTextBox = (EmployeeAddressTextBox != null) ? EmployeeAddressTextBox.Trim() : "";
            EmployeeSalaryTextBox = (EmployeeSalaryTextBox != null) ? EmployeeSalaryTextBox.Trim() : "";
            EmployeeDepartment.Value = (EmployeeDepartment.Value != null) ? EmployeeDepartment.Value.Trim() : "";
            EmployeeRole = (EmployeeRole != null) ? EmployeeRole.Trim() : "";
            if (EmployeeNameTextBox == "") return false;
            if (EmployeeAddressTextBox == "") return false;
            if (EmployeeSalaryTextBox == "") return false;
            if (EmployeeDepartment.Value == "") return false;
            if (EmployeeRole == "") return false;
            return true;
        }
    }
}
