using HospitalManagementSystem.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentsViewModel
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
