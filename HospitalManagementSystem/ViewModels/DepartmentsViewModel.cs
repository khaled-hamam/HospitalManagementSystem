using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {

        /// <summary>
        /// Items Properties
        /// </summary>
        public ObservableCollection<DepartmentCardViewModel> Departments { get; set; }
        public ObservableCollection<DepartmentCardViewModel> FilteredDepartments { get; set; }

        /// <summary>
        /// Search Bar Properties
        /// </summary>
        public String SearchQuery { get; set; }

        /// <summary>
        /// Add Dialog Properites
        /// </summary>
        public String DepartmentName { get; set; }
        public ComboBoxPairs EmployeeHead { get; set; }

        public List<ComboBoxPairs> ComboBoxItems;

        public ICommand SearchAction { get; set; }

        public string DepartmentNameTextBox { get; set; }
        public string DepartmentHeadIDTextBox { get; set; }
        public bool ValidateNameTextBox()
        {
            DepartmentNameTextBox = (DepartmentNameTextBox != null) ? DepartmentNameTextBox.Trim() : "";
            return !(DepartmentNameTextBox == "");
        }
        public bool ValidateHeadIDTextBox()
        {
            DepartmentHeadIDTextBox = (DepartmentHeadIDTextBox != null) ? DepartmentHeadIDTextBox.Trim() : "";
            return !(DepartmentHeadIDTextBox == "");
        }

        public DepartmentsViewModel()
        {
            ComboBoxItems = new List<ComboBoxPairs>();
            Departments = new ObservableCollection<DepartmentCardViewModel>();
            SearchAction = new RelayCommand(Search);
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.GetType() == typeof(Doctor))
                {
                    ComboBoxItems.Add(new ComboBoxPairs(employee.ID, employee.Name));
                }
            }

            foreach (Department department in Hospital.Departments.Values)
            {
                Departments.Add(
                    new DepartmentCardViewModel
                    {
                        ID = department.ID,
                        Name = department.Name,
                        EmployeesNumber = department.Nurse.Count + department.Doctors.Count,
                        PatientsNumber = department.Patients.Count
                    }
                );
            }
            FilteredDepartments = new ObservableCollection<DepartmentCardViewModel>(Departments);
        }


        private void Search()
        {
            if (String.IsNullOrEmpty(SearchQuery))
            {
                FilteredDepartments = new ObservableCollection<DepartmentCardViewModel>(Departments); return;
            }

            FilteredDepartments = new ObservableCollection<DepartmentCardViewModel>(
                Departments.Where(department => department.Name.ToLower().Contains(SearchQuery))
            );
        }


        public void addDepartment()
        {
            Department newDepartment = new Department
            {
                Name = DepartmentName,
                HeadID = EmployeeHead.Key
            };
            Departments.Add(
                new DepartmentCardViewModel
                {
                    ID = newDepartment.ID,
                    Name = newDepartment.Name,
                    PatientsNumber = newDepartment.Patients.Count,
                    EmployeesNumber = newDepartment.Nurse.Count + newDepartment.Doctors.Count
                }
                );
            FilteredDepartments.Add(
               new DepartmentCardViewModel
               {
                   ID = newDepartment.ID,
                   Name = newDepartment.Name,
                   PatientsNumber = newDepartment.Patients.Count,
                   EmployeesNumber = newDepartment.Nurse.Count + newDepartment.Doctors.Count
               }
               );
            Hospital.Departments.Add(newDepartment.ID, newDepartment);
            HospitalDB.InsertDepartment(newDepartment);

        }

        public bool ValidateDepartment()
        {
            DepartmentName = (DepartmentName != null) ? DepartmentName.Trim() : "";
            if (DepartmentName == "") return false;
            return true;
        }
    }
}
