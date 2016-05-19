PRINT N'Creating CarMaint...';
GO
CREATE SCHEMA [CarMaint]
	AUTHORIZATION [dbo];
GO
PRINT N'Creating CarMaint.Parts...';
GO
CREATE TABLE [CarMaint].[Parts] (
	[PartID]		Int					identity (1, 1) not null,
	[PartName]		nvarchar (50)		not null,
	[UnitPrice]		float				not null,
	[PartQuantity]	int					not null,
	[SubTotal]		float				not null,
	[AddDate]		date				not null
);
GO
PRINT N'Creating CarMaint.MaintPerformed...';
GO
create table [CarMaint].[Maintenance] (
	[MaintID]			int				identity (1, 1) not null,
	[MaintPerformed]	nvarchar (50)	not null,
	[MaintCost]			float			not null,
	[CreatedDate]		date			not null
);
GO
PRINT N'Creating CarMaint.Def_Parts_AddDate...';
GO
alter table [CarMaint].[Parts]
	add constraint [Def_Parts_AddDate] default GetDate() for [AddDate];
GO
PRINT N'Creating CarMaint.MaintPerformed.Def_Maintenance.CreatedDate...';
GO
alter table [CarMaint].[Maintenance]
	add constraint [Def_Maintenance_CreateddDate] default GetDate() for [CreatedDate];
GO
PRINT N'Creating Sales.uspNewPart...';
GO
CREATE PROCEDURE [CarMaint].[uspNewPart]
@PartName NVARCHAR (50),
@UnitPrice FLOAT,
@PartQuantity INT,
@SubTotal FLOAT,
@AddDate DATE,
@PartID INT OUTPUT
AS
BEGIN
INSERT INTO [CarMaint].[Parts] (PartName, UnitPrice, PartQuantity, SubTotal, AddDate) VALUES (@CustomerName, @UnitPrice, @PartQuantity, @SubTotal, @AddDate);
SET @PartID = SCOPE_IDENTITY();
RETURN @@ERROR
END
GO
PRINT N'Creating Sales.uspNewMaint...';
GO
CREATE PROCEDURE [CarMaint].[uspNewMaint]
@MaintPerformed NVARCHAR (50),
@MaintCost FLOAT, 
@CreatedDate DATE,
@MaintID INT OUTPUT
AS
BEGIN
INSERT INTO [CarMaint].[Maintenance] (MaintPerformed, MaintCost, CreatedDate) VALUES (@MaintPerformed, @MaintCost, @CreatedDate);
SET @MaintID = SCOPE_IDENTITY();
RETURN @@ERROR
END
GO