using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;

namespace HospitalManagementSystem.ViewModels
{
    public class RoomsViewModel : BaseViewModel
    {
        public ObservableCollection<RoomCardViewModel> Rooms { get; set; }

        public RoomsViewModel()
        {
            Rooms = new ObservableCollection<RoomCardViewModel>();
            foreach (Room room in Hospital.Rooms.Values)
            {
                String type;
                if (room.GetType() == typeof(PrivateRoom))
                    type = "Private Room";
                else if (room.GetType() == typeof(SemiPrivateRoom))
                    type = "Semi Private Room";
                else
                    type = "StandardWard Room";

                Rooms.Add(
                    new RoomCardViewModel
                    {
                        RoomNumber = room.RoomNumber,
                        Type = type,
                        Capacity = room.Patients.Count.ToString() + "/" + room.Capacity
                    }
                );
            }
        }
    }
}
