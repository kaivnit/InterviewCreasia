using AutoMapper;
using Interview.Application.DTOs;
using Interview.Application.Features.Queries.Employee;
using Interview.Domain.Entities;
using Interview.Domain.Models;

namespace Interview.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeEntity, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name));

        CreateMap<OutletEntity, OutletDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OutletCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.OutletName, opt => opt.MapFrom(src => src.Name));

        CreateMap<WorkTimeEntity, WorkTimeDto>();
        CreateMap<EmployeeWorkTimeEntity, EmployeeWorkTimeDto>();
        CreateMap<EmployeeAttendanceInformationEntity, EmployeeAttendanceInformationDto>()
                .ForMember(dest => dest.EmployeeWorkTimes, opt => opt.MapFrom(src => src.EmployeeWorkTimes));

        CreateMap<GetEmployeeAttendanceInformationQuery, EmployeeAttendanceInformationRequestDb>();
    }
}
