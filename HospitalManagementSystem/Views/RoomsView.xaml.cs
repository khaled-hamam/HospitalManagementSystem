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
            if (ViewModel.ValidateRoomtType())
            {
                ViewModel.Rooms.Add(
                    new RoomCardViewModel
                    {
                        RoomNumber = "RoomNumber",
                        Type = "Type",
                        Capacity = "Capcaity",
                    }
                );

                // Closing the Dialog
                DialogHost.CloseDialogCommand.Execute(addRoomDialaog, null);
            } else {
                MessageBox.Show("Invalid Entry");
            }

        }
    }
}
