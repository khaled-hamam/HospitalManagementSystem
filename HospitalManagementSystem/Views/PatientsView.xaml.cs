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
            ViewModel = new PatientsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            PatientRoomNumberComboBox.DisplayMemberPath = "Value";
            PatientRoomNumberComboBox.SelectedValuePath = "Key";
            PatientRoomNumberComboBox.ItemsSource = ViewModel.ComboBoxItems;
        }
        public void addPatient(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.ValidateInput())
            { MessageBox.Show("Invalid Entry"); return;}
            ViewModel.addPatient();
            // Closing the Dialog
            DialogHost.CloseDialogCommand.Execute(addPatientDialaog, null);
            PatientNameTextBox.Clear();
            PatientAddressTextBox.Clear();
            PatientBirthDatetDatePicker.SelectedDate = DateTime.Today;
            PatientTypeComboBox.SelectedIndex = -1;
        } 
  
    }
}
