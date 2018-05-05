using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class DepartmentDetailsViewModel : BaseViewModel
    {
        /// <summary>
        /// Displayed Data Properites
        /// </summary>
        public String DepartmetnId { get; set; }
        public String DepartmentName { set; get; }
        public String HeadName { set; get; }
        public ObservableCollection<String> DoctorsList { get; set; }
        public ObservableCollection<String> NursesList { get; set; }
        public ObservableCollection<String> PatientsList { get; set; }
        public String DoctorsCount { get; set; }
        public String NursesCount { get; set;}
        public String PatientsCount { get; set; }

        //Edit Department Data
        public String EditDepartmentName { get; set; }

        /// <summary>
        /// Commands Properties
        /// </summary>
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

            //Initializing Displayed Data Properties
            DepartmentName = Hospital.Departments[id].Name;
            EditDepartmentName = Hospital.Departments[id].Name;

            if (String.IsNullOrEmpty(Hospital.Departments[id].HeadID)) HeadName = "N/A";
            else HeadName = Hospital.Employees[Hospital.Departments[id].HeadID].Name;

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
            if (String.IsNullOrEmpty(EditDepartmentName))
            {
                MessageBox.Show("Department Name can't be Empty");
            }
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
