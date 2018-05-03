using HospitalManagementSystem.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Linq;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.ViewModels
{
    public class RoomsViewModel : BaseViewModel
    {
        /// <summary>
        /// Items Properties
        /// </summary>
        public ObservableCollection<RoomCardViewModel> Rooms { get; set; }
        public ObservableCollection<RoomCardViewModel> FilteredRooms { get; set; }

        /// <summary>
        /// Search Bar Properties
        /// </summary>
        public String SearchQuery { get; set; }

        /// <summary>
        /// Add Dialog Properites
        /// </summary>
        public String RoomNumber { get; set; }
        public String RoomType { get; set; }

        public ICommand SearchAction { get; set; }

        public RoomsViewModel()
        {
            Rooms = new ObservableCollection<RoomCardViewModel>();
            SearchAction = new RelayCommand(Search);
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
                        ID = room.ID,
                        RoomNumber = room.RoomNumber,
                        Type = type,
                        Capacity = room.Patients.Count.ToString() + "/" + room.Capacity
                    }
                );
            }
            FilteredRooms= new ObservableCollection<RoomCardViewModel>(Rooms);
        }


        private void Search()
        {
            if (String.IsNullOrEmpty(SearchQuery))
            {
                FilteredRooms = new ObservableCollection<RoomCardViewModel>(Rooms); return;
            }

            FilteredRooms = new ObservableCollection<RoomCardViewModel>(
                Rooms.Where(room => room.RoomNumber.ToString().Contains(SearchQuery))
            );
        }

        public void addRoom()
        {
            if(RoomType == "Private")
            {
                PrivateRoom newPrivateRoom = new PrivateRoom
                {
                    RoomNumber = int.Parse(RoomNumber),
                };
                Rooms.Add(
                    new RoomCardViewModel
                    {
                        ID = newPrivateRoom.ID,
                        RoomNumber = newPrivateRoom.RoomNumber,
                        Type = "Private Room",
                        Capacity = newPrivateRoom.Patients.Count.ToString() + '/' + newPrivateRoom.Capacity.ToString()
                    }
               );

                FilteredRooms.Add(
                    new RoomCardViewModel
                    {
                        ID = newPrivateRoom.ID,
                        RoomNumber = newPrivateRoom.RoomNumber,
                        Type = "Private Room",
                        Capacity = newPrivateRoom.Patients.Count.ToString() + '/' + newPrivateRoom.Capacity.ToString()
                    }
               );

                Hospital.Rooms.Add(newPrivateRoom.ID, newPrivateRoom);
                HospitalDB.InsertRoom(newPrivateRoom);
            }
 
            else if (RoomType == "Semi Private")
            {
                SemiPrivateRoom newSemiPrivateRoom = new SemiPrivateRoom
                {
                    RoomNumber = int.Parse(RoomNumber)
                };
                Rooms.Add(
                       new RoomCardViewModel
                       {
                           ID = newSemiPrivateRoom.ID,
                           RoomNumber = newSemiPrivateRoom.RoomNumber,
                           Type = "Semi Private Room",
                           Capacity = newSemiPrivateRoom.Patients.Count.ToString() + '/' + newSemiPrivateRoom.Capacity.ToString()
                       }
                  );

                FilteredRooms.Add(
                    new RoomCardViewModel
                    {
                        ID = newSemiPrivateRoom.ID,
                        RoomNumber = newSemiPrivateRoom.RoomNumber,
                        Type = "Semi Private Room" ,
                        Capacity = newSemiPrivateRoom.Patients.Count.ToString() + '/' + newSemiPrivateRoom.Capacity.ToString()
                    }
               );

                Hospital.Rooms.Add(newSemiPrivateRoom.ID, newSemiPrivateRoom);
                HospitalDB.InsertRoom(newSemiPrivateRoom);
            }

            else if(RoomType == "Standard Ward")
            {
                StandardWard newStandardWardRoom = new StandardWard
                {
                    RoomNumber = int.Parse(RoomNumber)
                };
                Rooms.Add(
                       new RoomCardViewModel
                       {
                           ID = newStandardWardRoom.ID,
                           RoomNumber = newStandardWardRoom.RoomNumber,
                           Type = "StandardWard Room",
                           Capacity = newStandardWardRoom.Patients.Count.ToString() + '/' + newStandardWardRoom.Capacity.ToString()
                       }
                  );

                FilteredRooms.Add(
                    new RoomCardViewModel
                    {
                        ID = newStandardWardRoom.ID,
                        RoomNumber = newStandardWardRoom.RoomNumber,
                        Type = "StandardWard Room",
                        Capacity = newStandardWardRoom.Patients.Count.ToString() + '/' + newStandardWardRoom.Capacity.ToString()
                    }
               );

                Hospital.Rooms.Add(newStandardWardRoom.ID, newStandardWardRoom);
                HospitalDB.InsertRoom(newStandardWardRoom);
            }
        }

        public bool ValidateRoom()
        {

            RoomNumber = (RoomNumber != null) ? RoomNumber.Trim() : "";
            if (RoomType == "") return false;

            if (RoomNumber == "") return false;
            foreach (Room room in Hospital.Rooms.Values)
            {
                if (int.Parse(RoomNumber) == room.RoomNumber)
                    return false;
            }
            
            return true;
        }


    }
}
