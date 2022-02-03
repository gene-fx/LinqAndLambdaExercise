using System.Linq;
using System.Collections.Generic;
using LinqAndLambdaExercise.Entities;

namespace LinqAndLambdaExercise.Services
{
    class Filters : FileManagement
    {
        public List<Employee> FindById (string fileName, int id)
        {
            List<Employee> result = FileReader(fileName).Where(x => x.Id == id).ToList();

            return result;
        }

        public List<Employee> FindByName(string fileName, string name)
        {
            List<Employee> result = FileReader(fileName).Where(x => x.Name == name).ToList();

            return result;
        }

        public List<Employee> FindByEmail(string fileName, string email)
        {
            List<Employee> result = FileReader(fileName).Where(x => x.Email == email).ToList();

            return result;
        }

        public List<Employee> FindBySalary(string fileName, double salary)
        {
            List<Employee> result = FileReader(fileName).Where(x => x.Salary == salary).ToList();

            return result;
        }
    }
}
    