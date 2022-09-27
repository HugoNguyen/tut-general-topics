using employee_management.library.Models;
using MediatR;

namespace employee_management.library.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeModel>
    {
        private int _id;

        public GetEmployeeByIdQuery(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }
    }
}
