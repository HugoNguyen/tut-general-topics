using employee_management.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_management.library.Data
{
    public class DataAccess : IDataAccess
    {
        private List<EmployeeModel> _employees = new();
        public DataAccess()
        {
            _employees.Add(new EmployeeModel { Id = 1, FirstName = "first", LastName = "Employee" });
            _employees.Add(new EmployeeModel { Id = 2, FirstName = "second", LastName = "Employee" });
        }

        public EmployeeModel AddEmployee(string firstName, string lastName)
        {
            EmployeeModel newEmployee = new() { FirstName = firstName, LastName = lastName };
            newEmployee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(newEmployee);
            return newEmployee;
        }

        public List<EmployeeModel> GetEmployees()
        {
            return _employees;
        }
    }
}
