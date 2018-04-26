using HospitalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for AppointmentPatientDetailsView.xaml
    /// </summary>
    public partial class AppointmentPatientDetailsView : UserControl
    {
        public AppointmentPatientDetailsViewModel ViewModel { get; set; }
        public AppointmentPatientDetailsView()
        {
            ViewModel = new AppointmentPatientDetailsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void DeleteAppointmentPatient(object sender, MouseButtonEventArgs e)
        {
            ViewModel.DeleteAppointmentPatient();

        }

        private void EditAppointmentPatient(object sender, RoutedEventArgs e)
        {

        }
    }
}
