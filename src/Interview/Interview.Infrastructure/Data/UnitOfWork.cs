using Interview.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Interview.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly string _connectionString;
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _disposed;
    public UnitOfWork(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection Connection => _connection ??= new SqlConnection(_connectionString);
    public IDbTransaction Transaction => _transaction;

    private void EnsureConnectionOpen()
    {
        if (_connection == null || string.IsNullOrEmpty(_connection.ConnectionString))
        {
            _connection = new SqlConnection(_connectionString);
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }

    public void Begin()
    {
        EnsureConnectionOpen();
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            _connection.Close();
            Dispose();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
        }
        finally
        {
            _connection.Close();
            Dispose();
            _transaction = null;
        }
    }

    #region Handle Dispose
    ~UnitOfWork()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }

            _disposed = true;
        }
        else
        {
            return;
        }
    }
    #endregion Handle Dispose
}
