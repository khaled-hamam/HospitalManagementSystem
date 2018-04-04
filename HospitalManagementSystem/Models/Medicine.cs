using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Medicine
    {
        private string id { get; set; }
        private string name { get; set; }
        private DateTime startingDate { get; set; }
        private DateTime endingDate { get; set; }
        public Medicine()
        {
            this.id = Guid.NewGuid().ToString();
            this.name = "";
            this.startingDate = new DateTime();
            this.startingDate = new DateTime();
        }

        public Medicine(string name, DateTime startingDate, DateTime endingDate)
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.startingDate = startingDate;
            this.endingDate = endingDate;
        }
    }
}
