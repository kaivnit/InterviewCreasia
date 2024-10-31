CREATE PROCEDURE [dbo].[sp_GetEmployeeByCode]
    @Code [NVARCHAR](100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Code], [Name]
    FROM [dbo].[Employee]
    WHERE [Code] = @Code;
END