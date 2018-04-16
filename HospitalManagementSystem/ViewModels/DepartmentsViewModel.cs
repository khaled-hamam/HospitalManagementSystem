using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentCardViewModel> Departments { get; set; }
        public DepartmentsViewModel()
        {
            Departments = new ObservableCollection<DepartmentCardViewModel>();
            foreach(Department department in  Hospital.Departments)
            {
                Departments.Add(
                    new DepartmentCardViewModel
                    {
                        Name = department.Name,
                        EmployeesNumber= department.Nurse.Count+department.Doctors.Count,
                        PatientsNumber = department.Patients.Count                 
                    }
                    );
            }
        }
    }
}
