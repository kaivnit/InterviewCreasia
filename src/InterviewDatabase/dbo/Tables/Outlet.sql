CREATE TABLE [dbo].[Outlet](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[Code] [NVARCHAR](100) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	CONSTRAINT [PK_Outlet_Id] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*****	Object: Start Unique  *****/
ALTER TABLE [dbo].[Outlet] 
ADD CONSTRAINT [UIDX_Outlet_Code] UNIQUE ([Code])
GO
/*****	End Unique  *****/

/*****	Star adding description  *****/
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Primary Key',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Outlet',
	@level2type=N'COLUMN',
	@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Code of Outlet',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Outlet',
	@level2type=N'COLUMN',
	@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Name of Outlet',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Outlet',
	@level2type=N'COLUMN',
	@level2name=N'Name'
GO
/*****	End adding description	*****/