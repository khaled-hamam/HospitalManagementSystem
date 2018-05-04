using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModels;
using HospitalManagementSystem.Views;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for RoomDetailsView.xaml
    /// </summary>
    public partial class RoomDetailsView : UserControl
    {
        public RoomDetailsViewModel ViewModel { get; set; }
        public RoomCardViewModel CardViewModel { get; set; }

        public RoomDetailsView()
        {

            InitializeComponent();
            
        }

        private void assignPatientToRoom(object sender, RoutedEventArgs e)
        {
            CardViewModel = new RoomCardViewModel();
            ViewModel = new RoomDetailsViewModel(CardViewModel.ID);
            Home.ViewModel.CloseRootDialog();
            ViewModel.AssignPatient();
        }
        private void assignNurseToRoom(object sender, RoutedEventArgs e)
        {
            CardViewModel = new RoomCardViewModel();
            ViewModel = new RoomDetailsViewModel(CardViewModel.ID);
            Home.ViewModel.CloseRootDialog();
            ViewModel.AssignNurse();
        }
    }
}
