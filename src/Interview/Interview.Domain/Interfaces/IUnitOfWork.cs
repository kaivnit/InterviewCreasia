using System.Data;

namespace Interview.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}
