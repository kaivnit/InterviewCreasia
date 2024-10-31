using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Domain.Entities;

[Table("Outlet")]
public class OutletEntity : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}
