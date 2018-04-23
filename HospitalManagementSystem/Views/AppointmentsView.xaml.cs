using System.Windows.Controls;
using HospitalManagementSystem.ViewModels;
using System.Windows;
using MaterialDesignThemes.Wpf;

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
        }

        public void addAppointment(object sender, RoutedEventArgs e)
        {
            // TODO: Openning a Message Box with Add
            // TODO: Add to Hospital Class
            // TODO: Update DB
            if (ViewModel.Validate() && AppointmentTimePicker.SelectedTime != null && AppointmentDatePicker.SelectedDate !=null)
            {
                ViewModel.Appointments.Add(
                    new AppointmentCardViewModel
                    {
                        PatientName = "Patient Name",
                        DoctorName = "Doctor Name",
                        Duration = "00:30",
                        AppointmentDate = "Appointment Date"
                    }
                );
                // Closing the Dialog
                DialogHost.CloseDialogCommand.Execute(addAppointmentDialaog, null);
            }
            else
            {
                MessageBox.Show("Input Not Valid");
            }
        }
    }
}
