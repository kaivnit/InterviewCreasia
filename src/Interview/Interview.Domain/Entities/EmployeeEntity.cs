using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Domain.Entities;

[Table("Employee")]
public class EmployeeEntity : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}
