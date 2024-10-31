CREATE PROCEDURE [dbo].[sp_GetOutletByCode]
    @Code [NVARCHAR](100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Code], [Name]
    FROM [dbo].[Outlet]
    WHERE [Code] = @Code;
END