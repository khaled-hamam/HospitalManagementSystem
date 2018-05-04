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
    /// Interaction logic for EmployeeDetailsVeiw.xaml
    /// </summary>
    public partial class EmployeeDetailsVeiw : UserControl
    {
        public EmployeeDetailsVeiwModel ViewModel { get; set; }
        public EmployeeDetailsVeiw()
        {
            InitializeComponent();
        }

        private void DeleteEmployee(object sender, MouseButtonEventArgs e)
        {
            ViewModel.DeleteEmployee();
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            ViewModel.EditEmployee();
            Home.ViewModel.CloseRootDialog();
        }
    }
}
