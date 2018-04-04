using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    abstract class Employee:Person
    {
        protected double salary { get; set; }
        protected DateTime employmentDate { get; set; }
        protected Department department { get; set; }

        public Employee()
        {
            this.salary = 0.0;
            this.employmentDate = new DateTime();
            this.department = new Department();
        }

        public Employee(double salary, Department department)
        {
            this.salary = salary;
            this.employmentDate = new DateTime();
            this.department = department;
        }

        public void raiseSalary(int percent)
        {
            this.salary += salary * percent / 100;
        }

        public void decrementSalary(int percent)
        {
            this.salary -= salary * percent / 100;
        }

    }
}
