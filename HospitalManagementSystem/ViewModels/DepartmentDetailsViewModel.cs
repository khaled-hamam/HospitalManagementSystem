using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentDetailsViewModel : BaseViewModel
    {
        public String DepartmetnId { get; set; }
        public ObservableCollection<String> DoctorsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }
        public ObservableCollection<String> PatientsList { get; set; }
        public String DepartmentName { set; get; }
        public String HeadName { set; get; }
        public List<ComboBoxPairs> DoctorsComboBoxItems;
        public List<ComboBoxPairs> NursesComboBoxItems;
        public List<ComboBoxPairs> PatientsComboBoxItems;

        public DepartmentDetailsViewModel(String id)
        {
            DoctorsComboBoxItems = new List<ComboBoxPairs>();
            foreach (Doctor doctor in Hospital.Departments[id].Doctors.Values)
            {
                DoctorsComboBoxItems.Add(new ComboBoxPairs(doctor.ID, doctor.Name));
            }

            /* NursesComboBoxItems = new List<ComboBoxPairs>();
             foreach (Nurse nurse in Hospital.Departments[DepartmetnId].Nurse.Values)
             {
                     NursesComboBoxItems.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
             }*/

        }


    }
}
