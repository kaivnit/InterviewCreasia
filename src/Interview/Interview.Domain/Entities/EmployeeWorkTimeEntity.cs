namespace Interview.Domain.Entities;

public class EmployeeWorkTimeEntity
{
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string OutletCode { get; set; }
    public string OutletName { get; set; }
    public List<WorkTimeEntity> WorkTime { get; set; }
}
