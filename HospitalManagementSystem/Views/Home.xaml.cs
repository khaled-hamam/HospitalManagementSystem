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
        public Home()
        {
            InitializeComponent();
            DataContext = new EmployeesViewModel();
        }

        private void navigateToDepartments(object sender, MouseButtonEventArgs e)
        {
            DataContext = new DepartmentsViewModel();
        }

        private void navigateToEmployees(object sender, MouseButtonEventArgs e)
        {
            DataContext = new EmployeesViewModel();
        }
        private void navigateToPatients(object sender, RoutedEventArgs e)
        {
            DataContext = new PatientsViewModel();
        }
        private void navigateToAppointments(object sender, RoutedEventArgs e)
        {   
            DataContext = new AppointmentsViewModel();
        }
    }
}
