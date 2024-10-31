using Interview.Application.DTOs;
using Interview.Application.Models;
using MediatR;

namespace Interview.Application.Features.Queries.Employee;

public class GetEmployeeByCodeQuery : IRequest<ResponseModel<EmployeeDto>>
{
    public string Code { get; set; }
}
