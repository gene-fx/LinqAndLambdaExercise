using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using LinqAndLambdaExercise.Entities;
using LinqAndLambdaExercise.Services;

namespace LinqAndLambdaExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("--------EMPLOYEE MANAGEMENT--------");
                Console.Write("Plese enter the data file source name or 'exit' to leave: ");
                string fileName = Console.ReadLine();
                while (String.IsNullOrEmpty(fileName) || String.IsNullOrWhiteSpace(fileName))
                {
                    Console.Write(" Enter a valid name: ");
                    fileName = Console.ReadLine();
                }
                if (fileName.ToLower().Equals("exit"))
                    Environment.Exit(0);

                if (fileName.Contains('.'))
                {
                    fileName = fileName.Substring(0, fileName.IndexOf('.'));
                    fileName += ".txt";
                }
                else
                    fileName += ".txt";

                FileManagement fileManagement = new FileManagement();
                fileManagement.FileExists(fileName);
                List<Employee> employees = fileManagement.FileReader(fileName);

                Console.WriteLine($"How do you wanto to select the emplyee from '{fileName}' data source");
                Console.Write("By name, Id, email or add new employee?: ");
                string userOpt = Console.ReadLine();
                while (String.IsNullOrEmpty(userOpt) || String.IsNullOrWhiteSpace(userOpt) && !userOpt.Contains("name") && !userOpt.Contains("email") && !userOpt.Contains("add"))
                {
                    Console.Write(" Enter a valid option: ");
                    userOpt = Console.ReadLine();
                }

                if (userOpt.ToLower().Contains("name"))//FIND BY NAME
                {
                    Console.Write("Enter the name of the employee: ");
                    string empName = Console.ReadLine();
                    while (String.IsNullOrEmpty(empName) || String.IsNullOrWhiteSpace(empName))
                    {
                        Console.Write(" Enter a valid name: ");
                        empName = Console.ReadLine();
                    }

                    string treatedName = fileManagement.NameTreatment(empName.ToLower());

                    bool isTheSameName = false;

                    foreach(Employee emp in employees)
                    {
                        if(emp.Name == treatedName)
                        {
                            isTheSameName = true;
                        }
                    }

                    while (isTheSameName == false)
                    {
                        Console.Clear();
                        Console.WriteLine($" There is no {treatedName} in {fileName} data source.");
                        Console.WriteLine(" The name must be exactly the same as the one you want from the list below:");
                        foreach(Employee emp in employees)
                        {
                            Console.WriteLine("  " + emp);
                        }
                        empName = Console.ReadLine();
                        treatedName = fileManagement.NameTreatment(empName.ToLower());
                        foreach (Employee emp in employees)
                        {
                            if (emp.Name == treatedName)
                            {
                                isTheSameName = true;
                            }
                        }
                    }

                    Console.WriteLine($"\nChoose an optin to do with {treatedName}");
                    Console.Write("\n Change name, change email or change salary: ");
                    userOpt = Console.ReadLine();
                    while (String.IsNullOrEmpty(userOpt) || String.IsNullOrWhiteSpace(userOpt) && !userOpt.Contains("name") && !userOpt.Contains("email") && !userOpt.Contains("salary"))
                    {
                        Console.Write(" Enter a valid option: ");
                        userOpt = Console.ReadLine();
                    }

                    if (userOpt.ToLower().Contains("name"))
                    {
                        Console.Write("Write the new name: ");
                        string newName = fileManagement.NameTreatment(Console.ReadLine());

                        var result = employees.Where(x => x.Name == treatedName).Select(x => x.Name = newName);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("email"))
                    {
                        Console.Write("Write the new email: ");
                        string newMail = Console.ReadLine();
                        while (fileManagement.IsValidEmail(newMail) != true)
                        {
                            Console.Write(" Enter a valid email address: ");
                            newMail = Console.ReadLine();
                        }

                        var result = employees.Where(x => x.Name == treatedName).Select(x => x.Email = newMail);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("salary"))
                    {
                        Console.WriteLine("Enter the new value: ");
                        int newSalary = int.Parse(Console.ReadLine());
                        while (newSalary <= 0)
                        {
                            Console.Write(" Enter a valid amount: ");
                            newSalary = int.Parse(Console.ReadLine());
                        }

                        var result = employees.Where(x => x.Name == treatedName).Select(x => x.Salary = newSalary);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                }
                else if (userOpt.ToLower().Contains("id"))//FIND BY ID
                {
                    Console.Write("Enter the ID of the employee: ");
                    int userOptId = int.Parse(Console.ReadLine());
                    while(String.IsNullOrEmpty(userOptId.ToString()) || String.IsNullOrWhiteSpace(userOptId.ToString()) || userOptId <= 0)
                    {
                        Console.Write(" Enter a valid ID: ");
                        userOptId = int.Parse(Console.ReadLine());
                    }

                    bool isTheSameId = false;

                    foreach(Employee emp in employees)
                    {
                        if (emp.Id == userOptId)
                            isTheSameId = true;
                    }

                    while(isTheSameId == false)
                    {
                        Console.Clear();
                        Console.WriteLine($" There is no ID {userOptId} in {fileName} data source.");
                        Console.WriteLine(" The ID must be exactly the same as the one you want from the list below:");
                        foreach (Employee emp in employees)
                        {
                            Console.WriteLine("  " + emp);
                        }
                        Console.Write(" Enter a valid ID: ");
                        userOptId = int.Parse(Console.ReadLine());
                        foreach (Employee emp in employees)
                        {
                            if (emp.Id == userOptId)
                                isTheSameId = true;
                        }
                    }

                    Console.WriteLine($"\nChoose an optin to do with {userOptId}");
                    Console.Write("\n Change name, change email or change salary: ");
                    userOpt = Console.ReadLine();
                    while (String.IsNullOrEmpty(userOpt) || String.IsNullOrWhiteSpace(userOpt) && !userOpt.Contains("name") && !userOpt.Contains("email") && !userOpt.Contains("salary"))
                    {
                        Console.Write(" Enter a valid option: ");
                        userOpt = Console.ReadLine();
                    }

                    if (userOpt.ToLower().Contains("name"))
                    {
                        Console.Write("Write the new name: ");
                        string newName = fileManagement.NameTreatment(Console.ReadLine());

                        var result = employees.Where(x => x.Id == userOptId).Select(x => x.Name = newName);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("email"))
                    {
                        Console.Write("Write the new email: ");
                        string newMail = Console.ReadLine();
                        while (fileManagement.IsValidEmail(newMail) != true)
                        {
                            Console.Write(" Enter a valid email address: ");
                            newMail = Console.ReadLine();
                        }

                        var result = employees.Where(x => x.Id == userOptId).Select(x => x.Email = newMail);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("salary"))
                    {
                        Console.WriteLine("Enter the new value: ");
                        int newSalary = int.Parse(Console.ReadLine());
                        while (newSalary <= 0)
                        {
                            Console.Write(" Enter a valid amount: ");
                            newSalary = int.Parse(Console.ReadLine());
                        }

                        var result = employees.Where(x => x.Id == userOptId).Select(x => x.Salary = newSalary);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                }
                else if (userOpt.ToLower().Contains("id"))//FIND BY EMAIL
                {
                    Console.Write("Enter the email of the employee: ");
                    string empMail = Console.ReadLine();
                    while (String.IsNullOrEmpty(empMail) || String.IsNullOrWhiteSpace(empMail) || fileManagement.IsValidEmail(empMail) == false)
                    {
                        Console.Write(" Enter a valid email: ");
                        empMail = Console.ReadLine();
                    }

                    bool isTheSameEmail = false;

                    foreach(Employee emp in employees)
                    {
                        if(emp.Email == empMail)
                        {
                            isTheSameEmail = true;
                        }
                    }

                    while(isTheSameEmail == false)
                    {
                        Console.Clear();
                        Console.WriteLine($" There is no email {empMail} in {fileName} data source.");
                        Console.WriteLine(" The ID must be exactly the same as the one you want from the list below:");
                        foreach (Employee emp in employees)
                        {
                            Console.WriteLine("  " + emp);
                        }
                        Console.Write(" Enter a valid email: ");
                        empMail = Console.ReadLine();

                        foreach (Employee emp in employees)
                        {
                            if (emp.Email == empMail)
                            {
                                isTheSameEmail = true;
                            }
                        }
                    }

                    Console.WriteLine($"\nChoose an optin to do with {empMail}");
                    Console.Write("\n Change name, change email or change salary: ");
                    userOpt = Console.ReadLine();
                    while (String.IsNullOrEmpty(userOpt) || String.IsNullOrWhiteSpace(userOpt) && !userOpt.Contains("name") && !userOpt.Contains("email") && !userOpt.Contains("salary"))
                    {
                        Console.Write(" Enter a valid option: ");
                        userOpt = Console.ReadLine();
                    }

                    if (userOpt.ToLower().Contains("name"))
                    {
                        Console.Write("Write the new name: ");
                        string newName = fileManagement.NameTreatment(Console.ReadLine());

                        var result = employees.Where(x => x.Email == empMail).Select(x => x.Name = newName);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("email"))
                    {
                        Console.Write("Write the new email: ");
                        string newMail = Console.ReadLine();
                        while (fileManagement.IsValidEmail(newMail) != true)
                        {
                            Console.Write(" Enter a valid email address: ");
                            newMail = Console.ReadLine();
                        }

                        var result = employees.Where(x => x.Email == empMail).Select(x => x.Email = newMail);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                    else if (userOpt.ToLower().Contains("salary"))
                    {
                        Console.WriteLine("Enter the new value: ");
                        int newSalary = int.Parse(Console.ReadLine());
                        while (newSalary <= 0)
                        {
                            Console.Write(" Enter a valid amount: ");
                            newSalary = int.Parse(Console.ReadLine());
                        }

                        var result = employees.Where(x => x.Email == empMail).Select(x => x.Salary = newSalary);

                        foreach (var emp in result)
                        {
                            Console.WriteLine(emp);
                        }

                        fileManagement.FileWriter(fileName, employees);
                    }
                }
                else if (userOpt.ToLower().Contains("add"))
                {
                    Console.Write("Enter the name of the employee: ");
                    string name = fileManagement.NameTreatment(Console.ReadLine());
                    while (String.IsNullOrWhiteSpace(name))
                    {
                        Console.Write(" Please enter a valid name:");
                        name = fileManagement.NameTreatment(Console.ReadLine());
                    }
                    Console.Write($"\n{name} Email: ");
                    string email = Console.ReadLine();
                    while (fileManagement.IsValidEmail(email) != true)
                    {
                        Console.Write(" Please enter a valid email address:");
                        email = Console.ReadLine();
                    }
                    Console.Write($"\n{name} salary: ");
                    double salary = double.Parse(Console.ReadLine());
                    while (salary < 0 || salary == 0)
                    {
                        Console.Write(" Please enter a valid salary amount");
                        salary = double.Parse(Console.ReadLine());
                    }

                    Employee employee = new Employee(name, email, salary);

                    fileManagement.AddEmployee(fileName, employee);

                    List<Employee> updatedList = fileManagement.FileReader(fileName);

                    foreach(Employee emp in updatedList)
                    {
                        Console.WriteLine(emp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
