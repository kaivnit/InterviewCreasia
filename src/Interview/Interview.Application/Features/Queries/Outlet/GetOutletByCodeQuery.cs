using Interview.Application.DTOs;
using Interview.Application.Models;
using MediatR;

namespace Interview.Application.Features.Queries.Outlet;

public class GetOutletByCodeQuery : IRequest<ResponseModel<OutletDto>>
{
    public string Code { get; set; }
}
