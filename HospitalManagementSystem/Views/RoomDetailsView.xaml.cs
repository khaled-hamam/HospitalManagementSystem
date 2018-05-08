
using HospitalManagementSystem.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Forms;

namespace HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for RoomDetailsView.xaml
    /// </summary>
    public partial class RoomDetailsView : System.Windows.Controls.UserControl
    {

        public RoomDetailsView()
        {

            InitializeComponent();

        }

        private void RemoveNurseFromRoom(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((RoomDetailsViewModel)DataContext).RemoveNurse();
           
        }
        private void ClearEditRoom(object sender, DialogClosingEventArgs eventArgs)
        {
            ((RoomDetailsViewModel)DataContext).textValidation = "";
            RoomNumberTextBox.Text = "";
        }
    }
}
