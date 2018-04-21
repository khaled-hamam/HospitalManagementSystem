using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        public ObservableCollection<EmployeeCardViewModel> Employees { get; set; }

        public EmployeesViewModel()
        {
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
    }
}
