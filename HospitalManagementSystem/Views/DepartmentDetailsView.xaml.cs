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
        public DepartmentDetailsView()
        {
            InitializeComponent();
        }


        private void DeleteDepartment(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditDepartment(object sender, RoutedEventArgs e)
        {
            Home.ViewModel.CloseRootDialog();

        }
    }
}
