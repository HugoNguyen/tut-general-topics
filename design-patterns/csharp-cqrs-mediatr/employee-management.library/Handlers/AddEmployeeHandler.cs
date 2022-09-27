using employee_management.library.Commands;
using employee_management.library.Data;
using employee_management.library.Models;
using MediatR;

namespace employee_management.library.Handlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, EmployeeModel>
    {
        private readonly IDataAccess _dataAccess;

        public AddEmployeeHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.AddEmployee(request.FirstName, request.LastName));
        }
    }
}
