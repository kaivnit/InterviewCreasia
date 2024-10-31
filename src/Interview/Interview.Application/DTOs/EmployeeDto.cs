using Interview.Application.Models;

namespace Interview.Application.DTOs;

public class EmployeeDto : BaseModel
{
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
}
