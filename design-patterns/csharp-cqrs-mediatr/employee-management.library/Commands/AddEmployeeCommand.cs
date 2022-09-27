using employee_management.library.Models;
using MediatR;

namespace employee_management.library.Commands
{
    public class AddEmployeeCommand : IRequest<EmployeeModel>
    {
        private string _firstName;
        private string _lastName;

        public AddEmployeeCommand(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }
    }
}
