using Interview.Application.Models;

namespace Interview.Application.DTOs;

public class OutletDto : BaseModel
{
    public string OutletCode { get; set; }
    public string OutletName { get; set; }
}
