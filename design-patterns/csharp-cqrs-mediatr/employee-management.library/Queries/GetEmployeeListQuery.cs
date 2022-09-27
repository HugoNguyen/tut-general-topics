using employee_management.library.Models;
using MediatR;

namespace employee_management.library.Queries
{
    public class GetEmployeeListQuery : IRequest<List<EmployeeModel>>
    {
    }
}
