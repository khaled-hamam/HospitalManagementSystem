using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    class Medicine
    {
        private string id;
        private string name;
        private DateTime startingDate;
        private DateTime endingDate;
        // getters & setters
        public string Id { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public DateTime StartingDate { get { return this.startingDate; } set { this.startingDate = value; } }
        public DateTime EndingDate { get { return this.endingDate; } set { this.endingDate = value; } }
        // constructors
        public Medicine()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "";
            this.StartingDate = new DateTime();
            this.StartingDate = new DateTime();
        }

        public Medicine(string name, DateTime startingDate, DateTime endingDate)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
        }
    }
}
