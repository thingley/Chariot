CREATE TABLE [data].[Person]
(
	[PersonID]		BIGINT					NOT NULL	CONSTRAINT [DF__data__Person__PersonID] DEFAULT (NEXT VALUE FOR [data].[SQ__Person])
	, [RV]			ROWVERSION				NOT NULL
	, [Title]		[type].[UDT__Title]		NOT NULL
	, [FirstName]	[type].[UDT__FirstName]	NOT NULL
	, [LastName]	[type].[UDT__LastName]	NOT NULL

	, CONSTRAINT [PK__data__Person__PersonID] PRIMARY KEY ([PersonID])
);
GO

