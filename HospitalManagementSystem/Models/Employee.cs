using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    abstract class Employee:Person
    {
        protected double salary;
        protected DateTime employmentDate;
        protected Department department;
        public double Salary { get { return this.salary; } set { this.salary = value; } }
        public DateTime EmploymentDate { get { return this.employmentDate; } set { this.employmentDate = value; } }
        public Department Department { get { return this.department; } set { this.department = value; } }

        public Employee()
        {
            this.Salary = 0.0;
            this.EmploymentDate = new DateTime();
            this.Department = new Department();
        }

        public Employee(double salary, Department department)
        {
            this.Salary = salary;
            this.EmploymentDate = new DateTime();
            this.Department = department;
        }

        public void raiseSalary(int percent)
        {
            this.Salary += salary * percent / 100;
        }

        public void decrementSalary(int percent)
        {
            this.Salary -= salary * percent / 100;
        }

    }
}
