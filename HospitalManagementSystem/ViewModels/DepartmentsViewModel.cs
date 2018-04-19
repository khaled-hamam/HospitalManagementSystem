using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentCardViewModel> Departments { get; set; }
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
