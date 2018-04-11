using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModels;
using HospitalManagementSystem.Views.Components;
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
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        private List<Employee> employees;
        private List<EmployeeCardViewModel> mvs;

        public EmployeesView()
        {
            InitializeComponent();
        }

        private void addEmployee(object sender, RoutedEventArgs e)
        {
            var employeeCard = new EmployeeCard { DataContext = new EmployeeCardViewModel() };
            employeeList.Children.Add(employeeCard);
        }
    }
}
