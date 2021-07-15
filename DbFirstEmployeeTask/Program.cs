using DbFirstEmployeeTask.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstEmployeeTask
{
    class Program
    {
        static void Main(string[] args)
        {


            using (var db = new EmployeeDbContext()) {
                char option;
                do {
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login.");
                    Console.WriteLine("3. Edit Name");
                    Console.WriteLine("4. Logout");
                    Console.WriteLine("5. Delete My Account.");
                    Console.WriteLine("6. Display All Employees");
                    Console.Write("Enter Your choice: ");
                    option = Console.ReadLine()[0];
                    EmployeeRepository repo = new EmployeeRepository(new EmployeeDbContext());
                    switch (option) {
                        case '1':
                            Employee employee = new Employee();
                            Console.WriteLine("Enter Name: ");
                            employee.Name = Console.ReadLine();

                            Console.WriteLine("Enter id: ");
                            employee.Id =Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter Email: ");
                            employee.Email = Console.ReadLine();

                            Console.WriteLine("Enter Password: ");
                            employee.Password = Console.ReadLine();

                            Console.WriteLine("Enter Mobile Number: ");
                            employee.Mobile = Console.ReadLine();

                            employee.AuthStatus = AuthStatus.UnAuthenticated;

                            var errorOrString = repo.AddEmployee(employee);

                            string msg = errorOrString.Match(
                                error => $"{error.Message}",
                                result => $"{result}"
                                );
                            Console.WriteLine(msg);
                            break;


                        case '2':
                            Console.WriteLine("Enter your email: ");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter you password : ");
                            string pass = Console.ReadLine();

                            errorOrString = repo.LoginEmployee(email, pass);

                            msg = errorOrString.Match(
                               error => $"{error.Message}",
                               result => $"{result}"
                               );
                            Console.WriteLine(msg);
                            break;
                        case '3':
                            Console.WriteLine("Enter email: ");
                            email = Console.ReadLine();
                            Console.WriteLine("Enter password: ");
                            pass = Console.ReadLine();

                            Console.WriteLine("Enter new name: ");
                            string name = Console.ReadLine();


                            errorOrString = repo.UpdateName(email, pass, name);

                            msg = errorOrString.Match(
                               error => $"{error.Message}",
                               result => $"{result}"
                               );
                            Console.WriteLine(msg);
                            break;


                        case '4':
                            Console.WriteLine("Enter email: ");
                            email = Console.ReadLine();
                            errorOrString = repo.LogoutEmployee(email);
                            msg = errorOrString.Match(
                               error => $"{error.Message}",
                               result => $"{result}"
                               );
                            Console.WriteLine(msg);
                            break;

                        case '5':
                            Console.WriteLine("Enter email: ");
                            email = Console.ReadLine();
                            Console.WriteLine("Enter password: ");
                            pass = Console.ReadLine();
                            errorOrString = repo.DeleteEmployee(email, pass);
                            msg = errorOrString.Match(
                               error => $"{error.Message}",
                               result => $"{result}"
                               );
                            Console.WriteLine(msg);
                            break;

                        case '6':
                            var errorOrList = repo.GetEmployeeList();

                            List<Employee> empList  = new List<Employee>();
                            msg = errorOrList.Match(
                                error => msg = error.Message,
                                (list)=>
                                {
                                    empList = list;
                                    return "";
                                }
                                );

                            if (msg == ""){
                                foreach (var emp in empList) {
                                    Console.WriteLine(emp.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{msg}");
                            }

                            break; 
                    }
                } while (option != 'e');
            }
        }
    }
}
