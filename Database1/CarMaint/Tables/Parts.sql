CREATE TABLE [CarMaint].[Parts] (
	[PartID]		Int					identity (1, 1) not null,
	[PartName]		nvarchar (50)		not null,
	[UnitPrice]		float				not null,
	[PartQuantity]	int					not null,
	[SubTotal]		float				not null,
	[AddDate]		date				not null
);
GO
alter table [CarMaint].[Parts]
	add constraint [Def_Parts_AddDate] default GetDate() for [AddDate];