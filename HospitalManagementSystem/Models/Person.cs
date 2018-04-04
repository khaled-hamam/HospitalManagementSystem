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
        protected string Address;
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
        public void setId(string id)
        {
            this._id = id;
        }
        public string getId()
        {
            return this._id;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return this.name;
        }
        public void setBirthDate(DateTime dt)
        {
            this.birthDate = dt;
        }
        public DateTime getBirthDate()
        {
            return this.birthDate;
        }
        public void setAddres(string ad)
        {
            this.Address=ad;
        }
        public string getAddress()
        {
            return this.Address;
        }

        public int getAge()
        {
            return 2018 - birthDate.Year;
        }

    }

}
