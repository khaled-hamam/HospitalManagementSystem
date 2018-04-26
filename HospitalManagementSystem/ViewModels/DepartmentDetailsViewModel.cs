using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentDetailsViewModel: BaseViewModel
    {
        public ObservableCollection<String> DoctorsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }
        public ObservableCollection<String> PatientsList { get; set; }
        public String DepartmentName { set; get; }
        public String HeadName { set; get; }
        public List<ComboBoxPairs> DoctorsComboBoxItems;
        public List<ComboBoxPairs> NursesComboBoxItems;
        public List<ComboBoxPairs> PatientsComboBoxItems;

        public DepartmentDetailsViewModel()
        {
            DoctorsComboBoxItems = new List<ComboBoxPairs>();
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if(employee.GetType()==typeof(Doctor))
                    DoctorsComboBoxItems.Add(new ComboBoxPairs(employee.ID, employee.Name));
            }

            NursesComboBoxItems = new List<ComboBoxPairs>();
            foreach (Employee employee in Hospital.Employees.Values)
            {
                if (employee.GetType() == typeof(Nurse))
                    NursesComboBoxItems.Add(new ComboBoxPairs(employee.ID, employee.Name));
            }

        }


    }
}
