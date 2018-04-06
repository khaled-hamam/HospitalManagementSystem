using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class StandardWard : Room
    {
        //Constructors
        public StandardWard(int capacity, double price) : base(capacity, price)
        {

        }
    }
}
