using Interview.Domain.Entities;

namespace Interview.Domain.Interfaces;

public interface IOutletRepository
{
    Task<OutletEntity> GetOutletByCodeAsync(string code);
}
