using HospitalManagementSystem.Views;
using System;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeeCardViewModel : BaseViewModel
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Role { get; set; }
        public String Department { get; set; }
        public String Salary { get; set; }

        public ICommand NavigateToDetailsAction { get; set; }

        public EmployeeCardViewModel()
        {
            NavigateToDetailsAction = new RelayCommand(navigateToDetails);
        }

        public void navigateToDetails()
        {
            Home.ViewModel.Content = new EmployeeDetailsVeiwModel(ID) {
                EmployeeName = Name,
                EmployeeRole = Role,
                EmployeeSalary = Salary,
                EmployeeDepartment = Department,
            };
        }
    }
}
