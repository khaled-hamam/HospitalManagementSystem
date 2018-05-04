using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentDetailsViewModel : BaseViewModel
    {
        /// <summary>
        /// Displayed Data Properites
        /// </summary>
        public String DepartmetnId { get; set; }
        public ObservableCollection<String> DoctorsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }
        public ObservableCollection<String> PatientsList { get; set; }
        public String DepartmentName { set; get; }
        public String HeadName { set; get; }
        public String EditDepartmentName { get; set; }
        public String  DoctorsCount { get; set; }
        public String NursesCount { get; set;}
        public String   PatientsCount { get; set; }

        public ICommand EditDepartment { get; set; }
        public ICommand DeleteDepartment { get; set; }
        public DepartmentDetailsViewModel(String id)
        {
            DepartmetnId = id;
            EditDepartment = new RelayCommand(EditDepartments);
            DeleteDepartment = new RelayCommand(DeleteDepartments);
            DoctorsList = new ObservableCollection<string>();
            NursesList = new ObservableCollection<string>();
            PatientsList = new ObservableCollection<string>();

            DoctorsCount = "Doctors: " + Hospital.Departments[id].Doctors.Count.ToString();
            NursesCount = "Nurses: " + Hospital.Departments[id].Nurse.Count.ToString();
          foreach(Doctor doctor in Hospital.Departments[id].Doctors.Values)
            {
                DoctorsList.Add(doctor.Name);
            }
          
          foreach(Nurse nurse in Hospital.Departments[id].Nurse.Values)
            {
                NursesList.Add(nurse.Name);
            }
          //TODO Patients List
        }

        public void EditDepartments()
        {
            DepartmentName = EditDepartmentName;
            Hospital.Departments[DepartmetnId].Name = EditDepartmentName;
            HospitalDB.UpdateDepartment(Hospital.Departments[DepartmetnId]);
            Home.ViewModel.CloseRootDialog();

        }

        public void DeleteDepartments()
        {
            Hospital.Departments.Remove(DepartmetnId);
            Home.ViewModel.CloseRootDialog();
            Home.ViewModel.Content = new DepartmentsViewModel();
            //TODO delete from DB
        }
    }
}
