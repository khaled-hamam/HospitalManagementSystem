using HospitalManagementSystem.ViewModels;
using MaterialDesignThemes.Wpf;
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

        private void ClearEditDepartment(object sender, DialogClosingEventArgs eventArgs)
        {
            validation.Text = "";
            DepartmentNameTextBox = null;
        }
    }
}
