using Dapper;
using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using Interview.Domain.Models;
using Newtonsoft.Json;
using System.Data;

namespace Interview.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IUnitOfWork _unitOfWork;
    public EmployeeRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeEntity> GetEmployeeByCodeAsync(string code)
    {
        _unitOfWork.Begin();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("Code", code, DbType.String);
            var result = await _unitOfWork.Connection
                                          .QuerySingleOrDefaultAsync<EmployeeEntity>("sp_GetEmployeeByCode",
                                                                                parameters,
                                                                                commandType: CommandType.StoredProcedure,
                                                                                transaction: _unitOfWork.Transaction);
            //await GetTest(code);
            //await GetTestDay(code);
            await GetTestDayHElloworld(code);

            _unitOfWork.Commit();
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<EmployeeAttendanceInformationEntity> GetEmployeeAttendanceInformationAsync(EmployeeAttendanceInformationRequestDb request)
    {
        var result = new EmployeeAttendanceInformationEntity();
        _unitOfWork.Begin();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Year", request.Year, DbType.Int32);
            parameters.Add("@Month", request.Month, DbType.Int32);
            var jsonResult = await _unitOfWork.Connection
                                          .QueryAsync<string>("sp_GetMonthlyWorkHours",
                                                                parameters,
                                                                commandType: CommandType.StoredProcedure,
                                                                transaction: _unitOfWork.Transaction);

            if (jsonResult != null)
            {
                string stringJsonResult = string.Join(" ", jsonResult);
                result = JsonConvert.DeserializeObject<EmployeeAttendanceInformationEntity>(stringJsonResult);
            }

            _unitOfWork.Commit();
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task GetTest(string code)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Year", 2023, DbType.Int64);
        parameters.Add("Month", 10, DbType.Int64);
        var result = await _unitOfWork.Connection
                                      .QueryAsync<MonthlyWorkHoursEntity>("sp_GetMonthlyWorkHours",
                                                                            parameters,
                                                                            commandType: CommandType.StoredProcedure,
                                                                            transaction: _unitOfWork.Transaction);
        var helloworld = JsonConvert.SerializeObject(result);
        
    }

    public async Task GetTestDay(string code)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Date", "2023-10-01", DbType.String);
        var result = await _unitOfWork.Connection
                                      .QueryAsync<DailyWorkHours>("sp_GetDailyWorkHours",
                                                                            parameters,
                                                                            commandType: CommandType.StoredProcedure,
                                                                            transaction: _unitOfWork.Transaction);
        var helloworld = "asdsadsa";

    }


    public async Task GetTestDayHElloworld(string code)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Year", 2023, DbType.Int32);
        parameters.Add("@Month", 10, DbType.Int32);
        var jsonResult = await _unitOfWork.Connection
                                      .QueryAsync<string>("sp_GetMonthlyWorkHours",
                                                            parameters,
                                                            commandType: CommandType.StoredProcedure,
                                                            transaction: _unitOfWork.Transaction);
        if (jsonResult != null)
        {
            string result = string.Join(" ", jsonResult);
            var model = JsonConvert.DeserializeObject<EmployeeAttendanceInformationEntity>(result);
        }

    }
}
