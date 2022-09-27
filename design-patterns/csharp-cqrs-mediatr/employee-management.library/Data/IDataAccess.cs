using employee_management.library.Models;

namespace employee_management.library.Data
{
    public interface IDataAccess
    {
        List<EmployeeModel> GetEmployees();
        EmployeeModel AddEmployee(string firstName, string lastName);
    }
}
