﻿namespace Interview.Domain.Entities;

public class DailyWorkHours
{
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string OutletCode { get; set; }
    public string WorkDate { get; set; }
    public float TotalHours { get; set; }
}
