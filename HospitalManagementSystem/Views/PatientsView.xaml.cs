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
        public PatientViewModel ViewModel { get; set; }

        public PatientsView()
        {
            ViewModel = new PatientViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }
        public void addPatient(object sender, RoutedEventArgs e)
        {
            ViewModel.Patients.Add(
                new PatientCardViewModel
                {
                    Name = "Name",
                    Type = "resident",
                    ShortDiagnosis = "fever2"
                }
            );
            // Closing the Dialog
            DialogHost.CloseDialogCommand.Execute(addPatientDialaog, null);
        }
    }
}
