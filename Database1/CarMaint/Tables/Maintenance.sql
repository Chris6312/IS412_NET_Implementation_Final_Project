create table [CarMaint].[Maintenance] (
	[MaintID]			int				identity (1, 1) not null,
	[MaintPerformed]	nvarchar (50)	not null,
	[MaintCost]			float			not null,
	[CreatedDate]		date			not null
);
GO
alter table [CarMaint].[Maintenance]
	add constraint [Def_Maintenance_CreatedDate] default GetDate() for [CreatedDate];