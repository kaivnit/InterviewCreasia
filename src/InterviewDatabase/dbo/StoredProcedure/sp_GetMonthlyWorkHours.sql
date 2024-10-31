CREATE PROCEDURE [dbo].[sp_GetMonthlyWorkHours]
    @Year INT,
    @Month INT
AS
BEGIN
    DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1);
    DECLARE @EndDate DATE = EOMONTH(@StartDate);

    -- Tạo bảng tạm để lưu kết quả
    CREATE TABLE #TempAttendance
    (
        EmployeeId UNIQUEIDENTIFIER,
        OutletId UNIQUEIDENTIFIER,
        WorkDate DATE,
        InTime TIME,
        OutTime TIME
    );

    -- Chèn dữ liệu vào bảng tạm #TempAttendance
    INSERT INTO #TempAttendance (EmployeeId, OutletId, WorkDate, InTime, OutTime)
    SELECT 
        a.EmployeeId,
        a.OutletId,
        a.Date AS WorkDate,
        MIN(CASE WHEN a.Type = 'IN' THEN a.Time END) AS InTime,
        MAX(CASE WHEN a.Type = 'OUT' THEN a.Time END) AS OutTime
    FROM 
        Attendance a
    WHERE 
        a.Date BETWEEN @StartDate AND @EndDate
    GROUP BY 
        a.EmployeeId, a.OutletId, a.Date;

    -- Tạo bảng tạm để lưu kết quả cuối cùng
    CREATE TABLE #MonthlyWorkHours
    (
        EmployeeCode NVARCHAR(100),
        EmployeeName NVARCHAR(100),
        OutletCode NVARCHAR(100),
        OutletName NVARCHAR(100),
        WorkDate DATE,
        TotalHours FLOAT
    );

    -- Tính toán tổng thời gian làm việc theo giờ cho từng ngày trong tháng đối với mỗi nhân viên
    INSERT INTO #MonthlyWorkHours (EmployeeCode, EmployeeName, OutletCode, OutletName, WorkDate, TotalHours)
    SELECT 
        e.Code AS EmployeeCode,
        e.Name AS EmployeeName,
        o.Code AS OutletCode,
        o.Name AS OutletName,
        t.WorkDate,
        DATEDIFF(MINUTE, t.InTime, t.OutTime) / 60.0 AS TotalHours
    FROM 
        #TempAttendance t
    JOIN 
        Employee e ON t.EmployeeId = e.Id
    JOIN 
        Outlet o ON t.OutletId = o.Id;

    -- Trả về kết quả dưới dạng JSON
    SELECT 
        EmployeeCode,
        EmployeeName,
        OutletCode,
        OutletName,
        (
            SELECT 
                WorkDate AS [Date],
                TotalHours AS [Time]
            FROM #MonthlyWorkHours AS wt
            WHERE wt.EmployeeCode = mw.EmployeeCode AND wt.OutletCode = mw.OutletCode
            FOR JSON PATH
        ) AS WorkTime
    FROM 
        #MonthlyWorkHours AS mw
    GROUP BY 
        EmployeeCode, EmployeeName, OutletCode, OutletName
    FOR JSON PATH, ROOT('EmployeeWorkTimes');

    -- Xóa bảng tạm
    DROP TABLE #TempAttendance;
    DROP TABLE #MonthlyWorkHours;
END
GO