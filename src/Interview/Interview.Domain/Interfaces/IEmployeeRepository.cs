using Interview.Domain.Entities;
using Interview.Domain.Models;

namespace Interview.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<EmployeeEntity> GetEmployeeByCodeAsync(string code);
    Task<EmployeeAttendanceInformationEntity> GetEmployeeAttendanceInformationAsync(EmployeeAttendanceInformationRequestDb request);
}
