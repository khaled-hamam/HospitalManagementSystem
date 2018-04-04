using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
   abstract class Person
    {
        // member variables
        public string _id { get; set; }
        protected string name { get; set; }
        protected DateTime birthDate { get; set; }
        protected string Address { get; set; }
        // constructors
        public Person()
        {
            this._id = "";
            this.name = "";
            this.Address = "";
        }
        public Person(string _id, string name, DateTime birthDate, string Address)
        {
            this._id = _id;
            this.name = name;
            this.birthDate = birthDate;
            this.Address = Address;
        }
        // member methods
        public int getAge()
        {
            return 2018 - birthDate.Year;
        }

    }

}
