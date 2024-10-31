using Dapper;
using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using System.Data;

namespace Interview.Infrastructure.Repositories;

public class OutletRepository : IOutletRepository
{
    private readonly IUnitOfWork _unitOfWork;
    public OutletRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OutletEntity> GetOutletByCodeAsync(string code)
    {
        _unitOfWork.Begin();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("Code", code, DbType.String);
            var result = await _unitOfWork.Connection
                                          .QuerySingleOrDefaultAsync<OutletEntity>("sp_GetOutletByCode",
                                                                                parameters,
                                                                                commandType: CommandType.StoredProcedure,
                                                                                transaction: _unitOfWork.Transaction);
            _unitOfWork.Commit();
            return await Task.FromResult(result);
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
