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
        protected String id;
        protected String name;
        protected DateTime birthDate;
        protected String address;

        public String Name { get{ return this.name;} set { this.name = value;}}
        public String ID { get { return this.id; } set { this.id = value; } }
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
            this.ID = Guid.NewGuid().ToString();
            this.Name = "";
            this.Address = "";
            this.BirthDate = DateTime.Today;
        }
        public Person(String name, DateTime birthDate, String Address)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = name;
            this.BirthDate = birthDate;
            this.Address = Address;
        }
    }
}
