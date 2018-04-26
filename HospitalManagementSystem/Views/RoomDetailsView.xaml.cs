using HospitalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


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
            ViewModel = new RoomDetailsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
