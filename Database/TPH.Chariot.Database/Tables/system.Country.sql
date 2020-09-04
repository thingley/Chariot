CREATE TABLE [system].[Country]
(
	[CountryID]		BIGINT						NOT NULL
	, [Code]		[type].[UDT__Code]			NOT NULL
	, [Country]		[type].[UDT__EntityName]	NOT NULL

	, CONSTRAINT [PK__Country__CountryID]	PRIMARY KEY CLUSTERED ([CountryID])
	, CONSTRAINT [UQ__Country__Code]		UNIQUE NONCLUSTERED ([Code]) ON FG__INDEXES
	, CONSTRAINT [CH__Country__Code]		CHECK (LEN([Code]) > 0)
	, CONSTRAINT [UQ__Country__AuditEntity] UNIQUE NONCLUSTERED ([Country]) ON FG__INDEXES
	, CONSTRAINT [CH__Country__AuditEntity] CHECK (LEN([Country]) > 0)
);
GO