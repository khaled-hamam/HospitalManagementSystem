using HospitalManagementSystem.Views;
using System;
using System.Windows.Input;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.ViewModels
{
    public class RoomCardViewModel : BaseViewModel
    {
        public String ID { get; set; }
        public int RoomNumber { get; set; }
        public String Type { get; set; }
        public String Capacity { get; set; }

        public ICommand NavigateToDetailsAction { get; set; }

        public RoomCardViewModel()
        {
            NavigateToDetailsAction = new RelayCommand(navigateToDetails);
        }

        public void navigateToDetails()
        {
            String type;
            String Capacity;
            String Price;
            if (Hospital.Rooms[ID].GetType() == typeof(PrivateRoom))
            {
                type = "Private Room";
                Capacity = Hospital.Config.PrivateRoomCapacity.ToString();
                Price = Hospital.Config.PrivateRoomPrice.ToString();
            }
            else if (Hospital.Rooms[ID].GetType() == typeof(SemiPrivateRoom))
            {
                type = "Semi Private Room";
                Capacity = Hospital.Config.SemiPrivateRoomCapacity.ToString();
                Price = Hospital.Config.SemiPrivateRoomPrice.ToString();
            }
            else
            {
                type = "StandardWard Room";
                Capacity = Hospital.Config.StandardWardCapacity.ToString();
                Price = Hospital.Config.StandardWardPrice.ToString();
            }
            Home.ViewModel.Content = new RoomDetailsViewModel(ID)
            {
                RoomID = Hospital.Rooms[ID].ID,
                RoomType = type,
                roomCapacity = $"{Hospital.Rooms[ID].Patients.Count} / {Capacity}",
                roomPrice = $"{Price}$",
            };
        }
    }
}
