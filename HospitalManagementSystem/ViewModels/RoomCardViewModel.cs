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
            if (Hospital.Rooms[ID].GetType() == typeof(PrivateRoom))
                type = "Private Room";
            else if (Hospital.Rooms[ID].GetType() == typeof(SemiPrivateRoom))
                type = "Semi Private Room";
            else
                type = "StandardWard Room";

            Home.ViewModel.Content = new RoomDetailsViewModel(ID)
            {
                RoomNumber = Hospital.Rooms[ID].RoomNumber.ToString(),
                RoomID = Hospital.Rooms[ID].ID,
                RoomType = type,
                roomCapacity = $"{Hospital.Rooms[ID].Patients.Count} / {Hospital.Rooms[ID].Capacity}",
                roomPrice = $"{Hospital.Rooms[ID].Price}$",
            };
        }
    }
}
