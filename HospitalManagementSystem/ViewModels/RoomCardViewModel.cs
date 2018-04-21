using System;


namespace HospitalManagementSystem.ViewModels
{
    public class RoomCardViewModel : BaseViewModel
    {
       public int RoomNumber { get; set; }
       public String Type { get; set; }
       public String Capacity { get; set; }
    }
}
