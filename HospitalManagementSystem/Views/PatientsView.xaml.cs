using HospitalManagementSystem.ViewModels;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for PatientsView.xaml
    /// </summary>
    public partial class PatientsView : UserControl
    {
        public PatientsViewModel ViewModel { get; set; }

        public PatientsView()
        {
            InitializeComponent();
        }
        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            PatientAddressTextBox.Clear();
            PatientNameTextBox.Clear();
            PatientTypeComboBox.SelectedIndex = -1;
            PatientDepartmentCombobox.SelectedIndex = -1;
            RoomNumberComboBox.SelectedIndex = -1;
        }

    }
}
