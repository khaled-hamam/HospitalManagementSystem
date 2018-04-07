using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class StandardWard : Room
    {
        const int standardCapacity = 6;
        const double standardPrice = 15;
        public StandardWard() : base(standardCapacity, standardPrice)
        {
        }
    }
}
