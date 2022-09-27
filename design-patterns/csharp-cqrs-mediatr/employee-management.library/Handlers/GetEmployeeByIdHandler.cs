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
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeModel>
    {
        private readonly IMediator _mediator;

        public GetEmployeeByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EmployeeModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            // in real scenario, we'll injecting the IDataAccess to retrieve the employee by Id from a db
            var employees = await _mediator.Send(new GetEmployeeListQuery());
            var searchedEmployee = employees.FirstOrDefault(q => q.Id == request.Id);
#pragma warning disable CS8603 // Possible null reference return.
            return searchedEmployee;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
