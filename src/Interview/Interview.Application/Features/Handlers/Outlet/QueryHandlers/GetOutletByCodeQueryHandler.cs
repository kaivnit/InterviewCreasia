using AutoMapper;
using Interview.Application.DTOs;
using Interview.Application.Enums;
using Interview.Application.Features.Queries.Outlet;
using Interview.Application.Models;
using Interview.Domain.Interfaces;
using MediatR;

namespace Interview.Application.Features.Handlers.Outlet.QueryHandlers;

public class GetOutletByCodeQueryHandler : IRequestHandler<GetOutletByCodeQuery, ResponseModel<OutletDto>>
{
    private readonly IOutletRepository _outletRepository;
    private readonly IMapper _mapper;
    public GetOutletByCodeQueryHandler(IOutletRepository outletRepository, IMapper mapper)
    {
        _outletRepository = outletRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<OutletDto>> Handle(GetOutletByCodeQuery request, CancellationToken cancellationToken)
    {
        ResponseModel<OutletDto> result = new ResponseModel<OutletDto>();
        try
        {
            var employeeEntity = await _outletRepository.GetOutletByCodeAsync(request.Code);
            if (employeeEntity != null)
            {
                var userDto = _mapper.Map<OutletDto>(employeeEntity);

                result.ResponseData = userDto;
                result.StatusCode = StatusCodeEnum.Success;
                result.Message = "Successfully";
            }

        }
        catch (Exception ex)
        {
            result.StatusCode = StatusCodeEnum.Unknown;
            result.Message = ex.Message;
            result.ResponseData = null;
        }

        return result;
    }
}
