using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace LinqAndLambdaExercise.Entities
{
    class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public int Id { get; set; }

        public Employee(string name, string email, double salary, int id)
        {
            Name = name;
            Email = email;
            Salary = salary;
            Id = id;
        }

        public Employee(string name, string email, double salary)
        {
            Name = name;
            Email = email;
            Salary = salary;
            GenereteId();
        }

        public void GenereteId()
        {
            Id = Name.GetHashCode() + Email.GetHashCode();
        }

        public override string ToString()
        {
            return Id + ", " + Name + ", " + Email + ", " + Salary.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
