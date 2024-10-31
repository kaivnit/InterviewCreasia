using System.ComponentModel.DataAnnotations;

namespace Interview.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } // Sử dụng NanoId
}
