using employee_management.library.Data;
using employee_management.library.Models;
using employee_management.library.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_management.library.Handlers
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeModel>>
    {
        private readonly IDataAccess _dataAccess;

        public GetEmployeeListHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<List<EmployeeModel>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.GetEmployees());
        }
    }
}
