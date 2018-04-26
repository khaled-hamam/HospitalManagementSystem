using HospitalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagementSystem.Views.Components
{
    /// <summary>
    /// Interaction logic for DepartmentDetailsView.xaml
    /// </summary>
    public partial class DepartmentDetailsView : UserControl
    {

        public DepartmentDetailsViewModel ViewModel { get; set; }

        public DepartmentDetailsView()
        {
            ViewModel = new DepartmentDetailsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            DepartmentDoctorComboBox.DisplayMemberPath = "Value";
            DepartmentDoctorComboBox.SelectedValuePath = "Key";
            DepartmentDoctorComboBox.ItemsSource = ViewModel.DoctorsComboBoxItems;

            DepartmentNursesComboBox.DisplayMemberPath = "Value";
            DepartmentNursesComboBox.SelectedValuePath = "Key";
            DepartmentNursesComboBox.ItemsSource = ViewModel.NursesComboBoxItems;

            DepartmentPatientsComboBox.DisplayMemberPath = "Value";
            DepartmentPatientsComboBox.SelectedValuePath = "Key";
            DepartmentPatientsComboBox.ItemsSource = ViewModel.PatientsComboBoxItems;

        }
       

        private void DeleteDepartment(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditDepartment(object sender, RoutedEventArgs e)
        {

        }
    }
}
