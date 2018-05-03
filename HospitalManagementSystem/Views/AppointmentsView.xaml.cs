using System.Windows.Controls;
using HospitalManagementSystem.ViewModels;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System;

namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsView : UserControl
    {
        public AppointmentsViewModel ViewModel { get; set; }

        public AppointmentsView()
        {
            ViewModel = new AppointmentsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            AppointmentDatePicker.BlackoutDates.AddDatesInPast();
            PatientNameComboBox.DisplayMemberPath = "Value";
            PatientNameComboBox.SelectedValuePath = "Key";
            PatientNameComboBox.ItemsSource = ViewModel.patientsComboBoxItems;

            DoctorNameComboBox.DisplayMemberPath = "Value";
            DoctorNameComboBox.SelectedValuePath = "Key";
            DoctorNameComboBox.ItemsSource = ViewModel.doctorsComboBoxItems;
        }

        public void addSubmit(object sender, RoutedEventArgs e)
        {
            // TODO: Openning a Message Box with Add
            // TODO: Add to Hospital Class
            // TODO: Update DB
            if (ViewModel.Validate())
            {
                ViewModel.addAppointment();
                Home.ViewModel.CloseRootDialog();
                AppointmentDuration.Clear();
                AppointmentTimePicker.SelectedTime = null;
                PatientNameComboBox.SelectedItem = null;
                DoctorNameComboBox.SelectedItem = null;
                AppointmentDatePicker.SelectedDate = DateTime.Today;

            }
            else
            {
                MessageBox.Show("Invalid Input!");

            }

        }
    }
}
