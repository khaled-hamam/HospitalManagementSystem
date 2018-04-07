using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class SemiPrivateRoom : Room
    {
        const int semiPrivateCapacity = 2;
        const double semiPrivatePrice = 50;
        public SemiPrivateRoom() : base(semiPrivateCapacity, semiPrivatePrice)
        {
        }
    }
}
