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
        protected String _id;
        protected String name;
        protected DateTime birthDate;
        protected String address;

        public String Name { get{ return this.name;} set { this.name = value;}}
        public String Id { get { return this._id; } set { this._id = value; } }
        public DateTime BirthDate { get { return this.birthDate; } set { this.birthDate = value; } }
        public String Address { get { return this.address; } set { this.address = value; } }
        public int Year { get; }
        public int Age {
            get
            {
                return Year - (this.birthDate.Year); 
            }
        }
        // constructors
        public Person()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "";
            this.Address = "";
        }
        public Person(String name, DateTime birthDate, String Address)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.BirthDate = birthDate;
            this.Address = Address;
        }
    }
}
