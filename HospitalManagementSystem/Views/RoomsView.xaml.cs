using HospitalManagementSystem.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;


namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : UserControl
    {
        public RoomsViewModel ViewModel { get; set; }

        public RoomsView()
        {
            ViewModel = new RoomsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }

        public void addRoom(object sender, RoutedEventArgs e)
        {
            // TODO: Openning a Message Box with Add
            // TODO: Add to Hospital Class
            // TODO: Update DB
           if(ViewModel.ValidateRoom())
            {
                ViewModel.addRoom();
                RoomNumberTextBox.Clear();
                RoomtTypeComboBox.SelectedItem = null;
                Home.ViewModel.CloseRootDialog();
            }
        }

    }
}
