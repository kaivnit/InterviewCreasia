using AutoMapper;
using Interview.Application.DTOs;
using Interview.Application.Enums;
using Interview.Application.Features.Queries.Employee;
using Interview.Application.Models;
using Interview.Domain.Interfaces;
using Interview.Domain.Models;
using MediatR;

namespace Interview.Application.Features.Handlers.Employee.QueryHandlers;

public class GetEmployeeAttendanceInformationQueryHandler : IRequestHandler<GetEmployeeAttendanceInformationQuery, ResponseModel<List<EmployeeWorkTimeDto>>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    public GetEmployeeAttendanceInformationQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<EmployeeWorkTimeDto>>> Handle(GetEmployeeAttendanceInformationQuery request, CancellationToken cancellationToken)
    {
        ResponseModel<List<EmployeeWorkTimeDto>> result = new ResponseModel<List<EmployeeWorkTimeDto>>();
        try
        {
            var requestDb = _mapper.Map<EmployeeAttendanceInformationRequestDb>(request);
            var employeeEntity = await _employeeRepository.GetEmployeeAttendanceInformationAsync(requestDb);
            if (employeeEntity != null)
            {
                var userDto = _mapper.Map<EmployeeAttendanceInformationDto>(employeeEntity);

                result.ResponseData = userDto.EmployeeWorkTimes;
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
