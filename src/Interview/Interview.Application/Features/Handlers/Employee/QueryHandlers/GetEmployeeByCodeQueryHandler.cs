using AutoMapper;
using Interview.Application.DTOs;
using Interview.Application.Enums;
using Interview.Application.Features.Queries.Employee;
using Interview.Application.Models;
using Interview.Domain.Interfaces;
using MediatR;

namespace Interview.Application.Features.Handlers.Employee.QueryHandlers;

public class GetEmployeeByCodeQueryHandler : IRequestHandler<GetEmployeeByCodeQuery, ResponseModel<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    public GetEmployeeByCodeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<EmployeeDto>> Handle(GetEmployeeByCodeQuery request, CancellationToken cancellationToken)
    {
        ResponseModel<EmployeeDto> result = new ResponseModel<EmployeeDto>();
        try
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByCodeAsync(request.Code);
            if (employeeEntity != null)
            {
                var userDto = _mapper.Map<EmployeeDto>(employeeEntity);

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
