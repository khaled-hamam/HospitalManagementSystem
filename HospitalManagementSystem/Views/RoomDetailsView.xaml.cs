using HospitalManagementSystem.ViewModels;
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

        public RoomDetailsView()
        {

            InitializeComponent();
        }

        public void editRoom(object sender, RoutedEventArgs e)
        {
            // TODO: Openning a Message Box with Add
            // TODO: Add to Hospital Class
            // TODO: Update DB

            // Closing the Dialog
            Home.ViewModel.CloseRootDialog();


        }
        private void deleteRoom(object sender, MouseButtonEventArgs e)
        {

        }
        private void assignPatientToRoom(object sender, MouseButtonEventArgs e)
        {

        }
        private void assignNurseToRoom(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
