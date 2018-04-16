using System;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentCardViewModel : BaseViewModel
    {
        public String Name { get; set; }
        public int EmployeesNumber { get; set; }
        public int PatientsNumber { get; set; }
    }
}
