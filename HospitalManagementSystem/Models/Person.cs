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
