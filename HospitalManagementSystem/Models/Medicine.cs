using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Medicine
    {
        private String id;
        private String name;
        private DateTime startingDate;
        private DateTime endingDate;
        // getters & setters
        public String ID { get { return this.id; } set { this.id = value; } }
        public String Name { get { return this.name; } set { this.name = value; } }
        public DateTime StartingDate { get { return this.startingDate; } set { this.startingDate = value; } }
        public DateTime EndingDate { get { return this.endingDate; } set { this.endingDate = value; } }
        // constructors
        public Medicine()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = "";
            this.StartingDate = new DateTime();
            this.StartingDate = new DateTime();
        }

        public Medicine(String name, DateTime startingDate, DateTime endingDate)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = name;
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
        }
    }
}
