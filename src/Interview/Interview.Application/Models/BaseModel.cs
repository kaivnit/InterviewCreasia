using System.ComponentModel.DataAnnotations;

namespace Interview.Application.Models;

public abstract class BaseModel
{
    [Key]
    public Guid Id { get; set; }
}
