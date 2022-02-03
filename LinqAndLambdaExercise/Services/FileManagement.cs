using System;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Collections.Generic;
using System.Globalization;
using LinqAndLambdaExercise.Entities;
using System.Threading;

namespace LinqAndLambdaExercise.Services
{
    class FileManagement : TextTreatment
    {
        string path = Path.GetTempPath();

        public void AddEmployee(string fileName, Employee employee)
        {
            string fileExt = FileExtensionAdder(fileName);

            using (FileStream fs = new FileStream(path + fileExt, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    long endPoint = fs.Length;
                    fs.Seek(endPoint, SeekOrigin.Begin);
                    sw.WriteLine(employee);
                }
                    
            }
        }

        public List<Employee> FileReader(string fileName)
        {
            string fileExt = FileExtensionAdder(fileName);

            string[] fileReader = File.ReadAllLines(path + fileExt);

            if (String.IsNullOrEmpty(File.ReadAllText(path + fileExt)) || String.IsNullOrWhiteSpace(File.ReadAllText(path + fileExt)))
            {
                Console.WriteLine($"The file {fileExt} is empty");
                Thread.Sleep(1500);
                FileExists(fileExt);
            }

            List<Employee> inputList = new List<Employee>();


            foreach (string line in fileReader)
            {
                string[] fileLines = line.Split(',');
                inputList.Add(new Employee(fileLines[1].Trim(), fileLines[2].Trim(), double.Parse(fileLines[3].Trim()), int.Parse(fileLines[0].Trim())));
            }

            return inputList;
        }

        public void DefautFileWriter(string fileName)
        {
            string fileExt = FileExtensionAdder(fileName);

            using (FileStream fs = new FileStream(path + fileExt, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    List<Employee> outPutList = new List<Employee>();

                    outPutList.Add(new Employee("Maria", "maria@gmail.com", 3200.00));
                    outPutList.Add(new Employee("Alex", "alex@gmail.com", 1900.00));
                    outPutList.Add(new Employee("Marco", "marco@gmail.com", 1700.00));
                    outPutList.Add(new Employee("Bob", "bob@gmail.com", 3500.00));
                    outPutList.Add(new Employee("Anna", "anna@gmail.com", 2800.00));

                    foreach (Employee emp in outPutList)
                    {
                        sw.WriteLine(emp);
                    }
                }
            }
        }

        public void FileWriter(string fileName, List<Employee> employees)
        {
            string fileExt = FileExtensionAdder(fileName);

            File.WriteAllText(path + fileExt, string.Empty);

            using (FileStream fs = new FileStream(path + fileExt, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (Employee emp in employees)
                        sw.WriteLine(emp);
                }
            }
        }

        public void UserFileWriter(int numberOfLines, string fileName)
        {
            string fileExt = FileExtensionAdder(fileName);

            File.WriteAllText(path + fileExt, string.Empty);

            List<Employee> outPutList = new List<Employee>();

            using (FileStream fs = new FileStream(path + fileExt, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 1; i <= numberOfLines; i++)
                    {
                        Console.Clear();
                        Console.Write($"#{i} Name: ");
                        string name = Console.ReadLine();
                        while (String.IsNullOrWhiteSpace(name))
                        {
                            Console.Write(" Please enter a valid name:");
                            name = Console.ReadLine();
                        }
                        Console.Write($"\n{name} Email: ");
                        string email = Console.ReadLine();
                        while (IsValidEmail(email) != true)
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

                        outPutList.Add(new Employee(NameTreatment(name.ToLower()), email, salary));

                    }

                    foreach (Employee emp in outPutList)
                    {
                        sw.WriteLine(emp);
                    }
                }
            }
        }

        public void CreatFile(string fileName)
        {
            string fileExt = FileExtensionAdder(fileName);

            using (FileStream fs = new FileStream(path + fileExt, FileMode.Create))
            {
                Console.WriteLine($"\nNew file {fileExt} successfully created");
            }
        }

        public void FileExists(string fileName)
        {
            int menuOpt = 10;

            string fileExt = FileExtensionAdder(fileName);

            while (menuOpt != 0)
            {
                try
                {
                    if (File.Exists(path + fileExt))
                    {
                        Console.WriteLine("--------FILE MANAGEMENT--------");
                        Console.WriteLine($"\nAlready there is a {fileExt} file in the system temp directory");
                        Console.WriteLine("\nPlease, choose an option:");
                        Console.WriteLine("1. See the file content");
                        Console.WriteLine("2. Overwrite the file");
                        Console.WriteLine("3. Create another file (diferent name)");
                        Console.WriteLine("0. Continue");
                        Console.Write("Option: ");
                        menuOpt = int.Parse(Console.ReadLine());
                        while (menuOpt < 0 || menuOpt > 3)
                        {
                            Console.WriteLine("  Enter a valid option");
                            Console.Write("  Option: ");
                            menuOpt = int.Parse(Console.ReadLine());
                        }

                        switch (menuOpt)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("-----------------------------------------");
                                foreach (Employee emp in FileReader(fileExt))
                                {
                                    Console.WriteLine(emp);
                                }
                                Console.WriteLine("-----------------------------------------");
                                continue;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("--OVERWRITING FILE--");

                                Console.WriteLine("1. Overwrite with defaut example");
                                Console.WriteLine("2. Overwrite with my information");
                                Console.Write("Option: ");
                                int case2Opt = int.Parse(Console.ReadLine());
                                while (case2Opt < 0 || case2Opt > 2)
                                {
                                    Console.WriteLine("  Enter a valid option");
                                    Console.Write("  Option: ");
                                    case2Opt = int.Parse(Console.ReadLine());
                                }

                                if (case2Opt == 1)
                                {
                                    DefautFileWriter(fileExt);
                                    Console.WriteLine("-----------------------------------------");
                                    Console.WriteLine("The file was writtern with: ");
                                    Console.WriteLine();
                                    foreach (Employee emp in FileReader(fileExt))
                                    {
                                        Console.WriteLine(emp);
                                    }
                                    Console.WriteLine("-----------------------------------------");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("--OVERWRITING FILE--");
                                    Console.Write("How many employees do you want to registry?: ");
                                    int qtd = int.Parse(Console.ReadLine());
                                    UserFileWriter(qtd, fileExt);

                                    Console.WriteLine("-----------------------------------------");
                                    Console.WriteLine("End of registration");
                                    Console.WriteLine("The file was writtern with: ");
                                    Console.WriteLine();
                                    foreach (Employee emp in FileReader(fileExt))
                                    {
                                        Console.WriteLine(emp);
                                    }
                                    Console.WriteLine("-----------------------------------------");
                                }
                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("--CREATING NEW FILE--");
                                Console.Write("Enter the name of the file (WITHOUT EXTENTION): ");
                                string newFileName = Console.ReadLine();
                                if (newFileName.Contains('.'))
                                {
                                    newFileName = newFileName.Substring(0, newFileName.IndexOf('.'));
                                    newFileName += ".txt";
                                }
                                else
                                    newFileName += ".txt";

                                CreatFile(newFileName);


                                Console.WriteLine("How do you want to fill the file?");
                                Console.WriteLine("1. Create with defaut example");
                                Console.WriteLine("2. Create with my information");
                                Console.Write("Option: ");
                                int case3Opt = int.Parse(Console.ReadLine());
                                while (case3Opt < 0 || case3Opt > 2)
                                {
                                    Console.WriteLine("  Enter a valid option");
                                    Console.Write("  Option: ");
                                    case3Opt = int.Parse(Console.ReadLine());
                                }
                                if (case3Opt == 1)
                                {
                                    DefautFileWriter(newFileName);
                                    Console.WriteLine("-----------------------------------------");
                                    Console.WriteLine("The file was writtern with: ");
                                    Console.WriteLine();
                                    foreach (Employee emp in FileReader(fileExt))
                                    {
                                        Console.WriteLine(emp);
                                    }
                                    Console.WriteLine("-----------------------------------------");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("--MANUAL FILLING--");
                                    Console.Write("How many employees do you want to registry?: ");
                                    int qtd = int.Parse(Console.ReadLine());
                                    UserFileWriter(qtd, newFileName);

                                    Console.WriteLine("-----------------------------------------");
                                    Console.WriteLine("End of registration");
                                    Console.WriteLine("The file was writtern with: ");
                                    Console.WriteLine();
                                    foreach (Employee emp in FileReader(fileExt))
                                    {
                                        Console.WriteLine(emp);
                                    }
                                    Console.WriteLine("-----------------------------------------");
                                }
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("--------FILE MANAGEMENT--------");
                        Console.WriteLine($"\nThere is no {fileExt} in system temp folder.");
                        Console.Write("\nDo you want to create this file (y/n)?: ");
                        char opt = char.Parse(Console.ReadLine());
                        if (opt.ToString().ToLower() == "y")
                        {
                            CreatFile(fileExt);

                            Console.WriteLine("File created successfully");
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine("\nHow do you want to fill the file");
                            Console.WriteLine("1. Fill with defaut example");
                            Console.WriteLine("2. Fill with my information");
                            Console.Write("Option: ");
                            int fillOpt = int.Parse(Console.ReadLine());
                            while (fillOpt < 0 || fillOpt > 2)
                            {
                                Console.WriteLine("  Enter a valid option");
                                Console.Write("  Option: ");
                                fillOpt = int.Parse(Console.ReadLine());
                            }
                            if (fillOpt == 1)
                            {
                                DefautFileWriter(fileExt);
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("The file was writtern with: ");
                                Console.WriteLine();
                                foreach (Employee emp in FileReader(fileExt))
                                {
                                    Console.WriteLine(emp);
                                }
                                Console.WriteLine("-----------------------------------------");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("--MANUAL FILLING--");
                                Console.Write("How many employees do you want to registry?: ");
                                int qtd = int.Parse(Console.ReadLine());
                                UserFileWriter(qtd, fileExt);

                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("End of registration");
                                Console.WriteLine("The file was writtern with: ");
                                Console.WriteLine();
                                foreach (Employee emp in FileReader(fileExt))
                                {
                                    Console.WriteLine(emp);
                                }
                                Console.WriteLine("-----------------------------------------");
                            }
                        }
                        else
                        {
                            Console.Write("Want to try again (y/n)?: ");
                            string tryAgain = Console.ReadLine();
                            if (tryAgain.ToLower() == "y" && menuOpt != 0)
                            {
                                Console.WriteLine("Name a file: ");
                                fileExt = Console.ReadLine();
                                if (fileExt.Contains('.'))
                                {
                                    fileExt = fileExt.Substring(0, fileExt.IndexOf('.'));
                                    fileExt += ".txt";
                                }
                                else
                                    fileExt += ".txt";

                                FileExists(fileExt);
                            }
                            else
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.Write("-----ERROR: ");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("The program is restarting");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    Thread.Sleep(500);
                    Console.Write(". ");
                    continue;
                }
            }

        }
    }
}
