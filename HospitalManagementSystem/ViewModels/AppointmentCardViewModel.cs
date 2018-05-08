using System;
using System.Windows.Input;
using HospitalManagementSystem.ViewModels;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using MaterialDesignThemes.Wpf;
using HospitalManagementSystem.Views.Components;
using HospitalManagementSystem.Views;
using System.Windows;

namespace HospitalManagementSystem.ViewModels
{
    public class AppointmentCardViewModel : BaseViewModel
    {
        public String ID { get; set; }
        public String PatientName { get; set; }
        public String DoctorName { get; set; }
        public String AppointmentDate { get; set; }
        public String Duration { get; set; }
        public String appointmentBill { get; set; }

        public ICommand deleteAppointment { get; set; }

        public AppointmentCardViewModel()
        {
            deleteAppointment = new RelayCommand(DeleteAppointment);
        }

        public async void DeleteAppointment()
        {
            // ((AppointmentsViewModel)Home.ViewModel.Content).FilteredAppointments.Clear();
            object result = await DialogHost.Show(new DeleteMessageBox(), "RootDialog");
            if (result.Equals(true))
            {
                ((AppointmentsViewModel)Home.ViewModel.Content).DeleteAppointment(ID);
            }
        }
    }
}
