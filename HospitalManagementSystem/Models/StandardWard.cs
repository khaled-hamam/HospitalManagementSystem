using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class StandardWard : Room
    {
       
        public StandardWard() : base(Hospital.Config.StandardWardCapacity, Hospital.Config.StandardWardPrice)
        {
        }
    }
}
