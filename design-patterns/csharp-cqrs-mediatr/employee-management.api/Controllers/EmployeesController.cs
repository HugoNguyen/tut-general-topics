using employee_management.library.Commands;
using employee_management.library.Models;
using employee_management.library.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace employee_management.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<EmployeeModel>> Get()
        {
            return await _mediator.Send(new GetEmployeeListQuery());
        }

        [HttpGet("{id}")]
        public async Task<EmployeeModel> Get(int id)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery(id));
        }

        [HttpPost]
        public async Task<EmployeeModel> Post([FromBody]EmployeeModel employeeModel)
        {
            return await _mediator.Send(new AddEmployeeCommand(employeeModel.FirstName, employeeModel.LastName));
        }
    }
}
