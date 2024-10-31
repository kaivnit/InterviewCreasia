namespace Interview.Application.DTOs;

public class EmployeeWorkTimeDto
{
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string OutletCode { get; set; }
    public string OutletName { get; set; }
    public List<WorkTimeDto> WorkTime { get; set; }
}
