using Interview.Application.DTOs;
using Interview.Application.Models;
using MediatR;

namespace Interview.Application.Features.Queries.Employee;

public class GetEmployeeAttendanceInformationQuery : IRequest<ResponseModel<List<EmployeeWorkTimeDto>>>
{
    public int Year { get; set; }
    public int Month { get; set; }
}
