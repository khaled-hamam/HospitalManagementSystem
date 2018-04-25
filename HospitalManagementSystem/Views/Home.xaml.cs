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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static HomeViewModel ViewModel { get; set; }

        public Home()
        {
            ViewModel = new HomeViewModel();
            DataContext = ViewModel;
            ViewModel.InitializeHospital();
            InitializeComponent();
        }

        private void navigateToDepartments(object sender, MouseButtonEventArgs e)
        {
            ViewModel.Content = new DepartmentsViewModel();
        }

        private void navigateToEmployees(object sender, MouseButtonEventArgs e)
        {
            ViewModel.Content = new EmployeesViewModel();
        }
        private void navigateToPatients(object sender, RoutedEventArgs e)
        {
            ViewModel.Content = new PatientsViewModel();
        }

        private void navigateToAppointments(object sender, RoutedEventArgs e)
        {   
            ViewModel.Content = new AppointmentsViewModel();
        }

        private void navigateToRooms(object sender, RoutedEventArgs e)
        {
            ViewModel.Content = new RoomsViewModel();
        }

    }
}
