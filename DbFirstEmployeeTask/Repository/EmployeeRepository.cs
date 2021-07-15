using DbFirstEmployeeTask.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstEmployeeTask.Repository
{
    public class EmployeeRepository
    {

        EmployeeDbContext context;
        public EmployeeRepository(EmployeeDbContext context) {
            this.context = context;
            
        }

        public Either<Failure,string> AddEmployee(Employee employee) {
            try
            {
                Console.WriteLine($"employee is:{employee.ToString()} ");
                context.Employees.Add(employee);
                context.SaveChanges();

                return "Successfully Added";
            }
            catch (Exception e) {  
            Console.WriteLine($"catch {e.InnerException.Message}");
                return new BadRequestFailure("Email Already exists");
            }
        }

        public Either<Failure, string> LoginEmployee(string email, string password)
        {
            try
            {
                Employee employee1 = (from employee in context.Employees
                                      where employee.Email == email &&
                                      employee.Password == password
                                      select employee)?.First();

                if (employee1 == null)
                {
                    return new BadRequestFailure("Invalid Credentials");
                }

                if (employee1.AuthStatus == AuthStatus.Authenticated) {
                    return new AlreadyLoggedInFailure("Already Logged In.");
                }

                employee1.AuthStatus = AuthStatus.Authenticated;

                context.Entry(employee1).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return "Successfully Logged In";
            }
            catch (Exception e)
            {
                return new BadRequestFailure("Invalid User");
            }
        }


        public Either<Failure, string> UpdateName(string email, string password, string name)
        {
            try
            {
                Employee employee1 = (from employee in context.Employees
                                      where employee.Email == email &&
                                      employee.Password == password
                                      select employee)?.First();

                if (employee1 == null)
                {
                    return new BadRequestFailure("Invalid Credentials");
                }


                employee1.Name = name;

                context.Entry(employee1).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return "Successfully Changed";
            }
            catch (Exception e) {
                return new BadRequestFailure("Invalid User");
            }
            
        }


        public Either<Failure, string> LogoutEmployee(string email)
        {
            try
            {
                Employee employee1 = (from employee in context.Employees
                                      where employee.Email == email
                                      select employee)?.First();

                if (employee1 == null)
                {
                    return new BadRequestFailure("Invalid Credentials");
                }

                if (employee1.AuthStatus == AuthStatus.UnAuthenticated)
                {
                    return new AlreadyLoggedInFailure("Already Logged Out.");
                }

                employee1.AuthStatus = AuthStatus.UnAuthenticated;

                context.Entry(employee1).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return "Successfully Logged Out";
            }
            catch (Exception e)
            {
                return new BadRequestFailure("Invalid User");
            }
        }


        public Either<Failure, string> DeleteEmployee(string email,string password)
        {
            try
            {
                Employee employee1 = (from employee in context.Employees
                                      where employee.Email == email && employee.Password == password
                                      select employee)?.First();

                if (employee1 == null)
                {
                    return new BadRequestFailure("Invalid Credentials");
                }

                context.Employees.Remove(employee1);

                context.SaveChanges();

                return "Account deleted.";
            }
            catch (Exception e)
            {
                return new BadRequestFailure("Invalid User");
            }
        }

        public Either<Failure, List<Employee>> GetEmployeeList()
        {
            try
            {
                var employees = (from employee in context.Employees
                                      select employee)?.ToList<Employee>();


                if (employees == null)
                {
                    return new BadRequestFailure("Bad Request");
                }

                return employees;
            }
            catch (Exception e)
            {
                return new BadRequestFailure("Bad Request");
            }
        }



    }
}
