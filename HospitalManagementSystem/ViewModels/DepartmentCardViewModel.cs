using HospitalManagementSystem.Views;
using System;
using System.Windows.Input;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentCardViewModel : BaseViewModel
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public int EmployeesNumber { get; set; }
        public int PatientsNumber { get; set; }

        public ICommand NavigateToDetailsAction { get; set; }

        public DepartmentCardViewModel()
        {
            NavigateToDetailsAction = new RelayCommand(navigateToDetails);
        }

        public void navigateToDetails()
        {
            String headname;
            if (String.IsNullOrEmpty(Hospital.Departments[ID].HeadID)) headname = "N/A";
            else headname = Hospital.Employees[Hospital.Departments[ID].HeadID].Name;

            Home.ViewModel.Content = new DepartmentDetailsViewModel(ID)
            {
                DepartmentName = Hospital.Departments[ID].Name,
                DepartmetnId = Hospital.Departments[ID].ID,
                HeadName = headname
            };
        }
    }
}
