﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class SemiPrivateRoom : Room
    {
        
        public SemiPrivateRoom() : base(Hospital.Config.SemiPrivateRoomCapacity, Hospital.Config.SemiPrivateRoomPrice)
        {
        }
    }
}
