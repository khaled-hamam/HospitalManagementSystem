using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using HospitalManagementSystem.Views.Components;
using MaterialDesignThemes.Wpf;
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
        public String DepartmentId { get; set; }
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
        public String textValidation { get; set; }
        /// <summary>
        /// Commands Properties
        /// </summary>
        public ICommand EditDepartment { get; set; }
        public ICommand DeleteDepartment { get; set; }

        public DepartmentDetailsViewModel(String id)
        {
            DepartmentId = id;
            EditDepartment = new RelayCommand(EditDepartments);
            DeleteDepartment = new RelayCommand(DeleteDepartments);
            DoctorsList = new ObservableCollection<String>();
            NursesList = new ObservableCollection<String>();
            PatientsList = new ObservableCollection<String>();

            //Initializing Displayed Data Properties
            DepartmentName = Hospital.Departments[id].Name;
            EditDepartmentName = Hospital.Departments[id].Name;

            if (String.IsNullOrEmpty(Hospital.Departments[id].HeadID)) HeadName = "N/A";
            else HeadName = Hospital.Employees[Hospital.Departments[id].HeadID].Name;

            DoctorsCount = "Doctors: " + Hospital.Departments[id].Doctors.Count.ToString();
            NursesCount = "Nurses: " + Hospital.Departments[id].Nurse.Count.ToString();
            PatientsCount = "Patients: " + Hospital.Departments[id].Patients.Count.ToString();
            foreach(Doctor doctor in Hospital.Departments[id].Doctors.Values)
            {
                DoctorsList.Add(doctor.Name);
            }
          
            foreach(Nurse nurse in Hospital.Departments[id].Nurse.Values)
            {
                NursesList.Add(nurse.Name);
            }

           foreach(Patient patient in Hospital.Departments[id].Patients.Values)
            {
                PatientsList.Add(patient.Name);
            }
        }

        public void EditDepartments()
        {
            if (String.IsNullOrEmpty(EditDepartmentName))
            {
                textValidation = "Department Name is empty";
                return;
            }
            DepartmentName = EditDepartmentName;
            Hospital.Departments[DepartmentId].Name = EditDepartmentName;
            HospitalDB.UpdateDepartment(Hospital.Departments[DepartmentId]);
            Home.ViewModel.CloseRootDialog();
        }

        public async void DeleteDepartments()
        {
            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {
                // Delete Logic Here
            Hospital.DeleteDepartment(DepartmentId);
            HospitalDB.DeleteDepartment(DepartmentId);
            Home.ViewModel.Content = new DepartmentsViewModel();
            }

        }
    }
}
