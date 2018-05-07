using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModels;
using HospitalManagementSystem.Views.Components;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public EmployeesViewModel ViewModel { get; set; }

        public EmployeesView()
        {
            ViewModel = new EmployeesViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            EmployeeDepartmentComboBox.DisplayMemberPath = "Value";
            EmployeeDepartmentComboBox.SelectedValuePath = "Key";
            EmployeeDepartmentComboBox.ItemsSource = ViewModel.ComboBoxItems;
        }

        public void addEmployee(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ValidateName() )
            {
                ViewModel.addEmployee();
                EmployeeNameTextBox.Clear();
                EmployeeAddressTextBox.Clear();
                EmployeeBirthDatePicker.SelectedDate = DateTime.Today;
                EmployeeSalaryTextBox.Clear();
                EmployeeDepartmentComboBox.SelectedItem = null;
                EmpoloyeeRoleComboBox.SelectedItem = null;
                Home.ViewModel.CloseRootDialog();
            }
         
        }

        private void ClearAddEmployee(object sender, DialogClosingEventArgs eventArgs)
        {
            EmployeeNameTextBox.Clear();
            EmployeeAddressTextBox.Clear();
            EmployeeBirthDatePicker.SelectedDate = DateTime.Today;
            EmployeeSalaryTextBox.Clear();
            EmployeeDepartmentComboBox.SelectedItem = null;
            EmpoloyeeRoleComboBox.SelectedItem = null;
            validation.Text = "";
        }

    }
}
