using HospitalManagementSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for DepartmentDetailsView.xaml
    /// </summary>
    public partial class DepartmentDetailsView : UserControl
    {

        //public DepartmentDetailsViewModel ViewModel { get; set; }

        public DepartmentDetailsView()
        {
            //ViewModel = new DepartmentDetailsViewModel("1");
            //DataContext = ViewModel;
            InitializeComponent();
            DepartmentDoctorComboBox.DisplayMemberPath = "Value";
            DepartmentDoctorComboBox.SelectedValuePath = "Key";
            DepartmentDoctorComboBox.ItemsSource = ((DepartmentDetailsViewModel)DataContext).DoctorsComboBoxItems;

            DepartmentNursesComboBox.DisplayMemberPath = "Value";
            DepartmentNursesComboBox.SelectedValuePath = "Key";
            DepartmentNursesComboBox.ItemsSource = ((DepartmentDetailsViewModel)DataContext).NursesComboBoxItems;

            DepartmentPatientsComboBox.DisplayMemberPath = "Value";
            DepartmentPatientsComboBox.SelectedValuePath = "Key";
            DepartmentPatientsComboBox.ItemsSource = ((DepartmentDetailsViewModel)DataContext).PatientsComboBoxItems;

        }


        private void DeleteDepartment(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditDepartment(object sender, RoutedEventArgs e)
        {

        }
    }
}
