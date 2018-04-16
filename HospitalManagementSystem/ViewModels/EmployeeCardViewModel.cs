using System;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeeCardViewModel : BaseViewModel
    {
        public String Name { get; set; }
        public String Role { get; set; }
        public String Department { get; set; }
        public String Salary { get; set; }
    }
}
