CREATE PROCEDURE [dbo].[sp_GetDailyWorkHours]
    @Date DATE
AS
BEGIN
    -- Tạo bảng tạm để lưu kết quả
    CREATE TABLE #DailyWorkHours
    (
        EmployeeCode NVARCHAR(100),
        EmployeeName NVARCHAR(100),
        OutletCode NVARCHAR(100),
        OutletName NVARCHAR(100),
        WorkDate DATE,
        TotalHours FLOAT
    );

    -- Tạo bảng tạm để lưu thời gian vào và ra
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
        a.Date = @Date
    GROUP BY 
        a.EmployeeId, a.OutletId, a.Date;

    -- Tính toán tổng thời gian làm việc theo giờ cho từng ngày đối với mỗi nhân viên
    INSERT INTO #DailyWorkHours (EmployeeCode, EmployeeName, OutletCode, OutletName, WorkDate, TotalHours)
    SELECT 
        e.Code AS EmployeeCode,
        e.Name AS EmployeeName,
        o.Code AS OutletCode,
        o.Code AS OutletName,
        t.WorkDate,
        SUM(DATEDIFF(MINUTE, t.InTime, t.OutTime)) / 60.0 AS TotalHours
    FROM 
        #TempAttendance t
    JOIN 
        Employee e ON t.EmployeeId = e.Id
    JOIN 
        Outlet o ON t.OutletId = o.Id
    GROUP BY 
        e.Code, e.Name, o.Code, t.WorkDate;

    -- Trả về kết quả
    SELECT * FROM #DailyWorkHours;

    -- Xóa bảng tạm
    DROP TABLE #DailyWorkHours;
    DROP TABLE #TempAttendance;
END
GO