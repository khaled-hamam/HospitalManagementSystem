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

           if(ViewModel.ValidateRoom())
            {
                ViewModel.addRoom();
                RoomNumberTextBox.Clear();
                RoomtTypeComboBox.SelectedItem = null;
                Home.ViewModel.CloseRootDialog();
            }
        }
        private void ClearAddRoom(object sender, DialogClosingEventArgs eventArgs)
        {
            RoomNumberTextBox.Clear();
            RoomtTypeComboBox.SelectedItem = null;
            validation.Text = "";
        }

    }
}
