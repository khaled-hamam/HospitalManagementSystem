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
        protected string _id;
        protected string name;
        protected DateTime birthDate;
        protected string address;

        public string Name{ get{ return this.name;} set { this.name = value;}}
        public string Id { get { return this._id; } set { this._id = value; } }
        public DateTime BirthDate { get { return this.birthDate; } set { this.birthDate = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
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
        public Person(string name, DateTime birthDate, string Address)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.BirthDate = birthDate;
            this.Address = Address;
        }
    }
}
