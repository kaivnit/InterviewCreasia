CREATE TABLE [dbo].[Attendance](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[EmployeeId] [UNIQUEIDENTIFIER] NOT NULL,
	[OutletId] [UNIQUEIDENTIFIER] NOT NULL,
	[Date] [DATE] NOT NULL,
	[Time] [TIME] NOT NULL,
	[Type] [VARCHAR](3) NOT NULL,
	CONSTRAINT [PK_Attendance_Id] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*****	Object: Start Foreign Key  *****/
ALTER TABLE [dbo].[Attendance]  ADD  CONSTRAINT [FK_Attendance_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[Attendance]  ADD  CONSTRAINT [FK_Attendance_OutletId] FOREIGN KEY([OutletId])
REFERENCES [dbo].[Outlet] ([Id])
GO
/*****	Object: End Foreign Key  *****/

/*****	Star adding description  *****/
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Primary Key',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Id of Employee Table',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'EmployeeId'
GO

EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Id of Outlet Table',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'OutletId'
GO

EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Date of attendance',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Time of attendance',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'Time'
GO

EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description',
	@value=N'Timekeeping type (IN = Check in, OUT = Check out)',
	@level0type=N'SCHEMA',
	@level0name=N'dbo',
	@level1type=N'TABLE',
	@level1name=N'Attendance',
	@level2type=N'COLUMN',
	@level2name=N'Type'
GO
/*****	End adding description	*****/