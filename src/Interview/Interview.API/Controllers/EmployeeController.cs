using Interview.Application.Enums;
using Interview.Application.Features.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeeAsync(string code)
    {
        var query = new GetEmployeeByCodeQuery { Code = code };
        var user = await _mediator.Send(query);
        if (user != null && user.StatusCode == StatusCodeEnum.Success)
        {
            return Ok(user);
        }
        return NotFound();
    }

    [HttpPost("attendance")]
    public async Task<IActionResult> GetEmployeeAttendanceInformationAsync(GetEmployeeAttendanceInformationQuery request)
    {
        var user = await _mediator.Send(request);
        if (user != null && user.StatusCode == StatusCodeEnum.Success)
        {
            return Ok(user);
        }
        return NotFound();
    }
}
