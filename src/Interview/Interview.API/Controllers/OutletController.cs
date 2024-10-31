using Interview.Application.Enums;
using Interview.Application.Features.Queries.Outlet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OutletController : ControllerBase
{
    private readonly IMediator _mediator;
    public OutletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOutletAsync(string code)
    {
        var query = new GetOutletByCodeQuery { Code = code };
        var user = await _mediator.Send(query);
        if (user != null && user.StatusCode == StatusCodeEnum.Success)
        {
            return Ok(user);
        }
        return NotFound(user);
    }
}
