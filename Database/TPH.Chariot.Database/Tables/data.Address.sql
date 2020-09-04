CREATE TABLE [data].[Address]
(
	[AddressID]				BIGINT						NOT NULL	CONSTRAINT [DF__Address__AddressID] DEFAULT (NEXT VALUE FOR [data].[SQ__Address])
	, [RV]					ROWVERSION					NOT NULL
	, [Lookup]				[type].[UDT__Lookup]		NOT NULL
	, [OrganisationName]	[type].[UDT__EntityName]	NOT NULL
	, [Line1]				NVARCHAR(100)				NOT NULL
	, [Line2]				NVARCHAR(100)				NOT NULL
	, [Line3]				NVARCHAR(100)				NOT NULL
	, [Line4]				NVARCHAR(100)				NOT NULL
	, [Line5]				NVARCHAR(100)				NOT NULL
	, [ZipCode]				NVARCHAR(20)				NOT NULL
	, [CountryID]			BIGINT						NOT NULL
	, [AddressTypeID]		BIGINT						NOT NULL

	, CONSTRAINT [PK__Address__AddressID]		PRIMARY KEY CLUSTERED ([AddressID])
	, CONSTRAINT [CH__Address__Line1]			CHECK (LEN([Line1]) > 0)
	, CONSTRAINT [CH__Address__ZipCode]			CHECK (LEN([ZipCode]) > 0)
	, CONSTRAINT [FK__Address__CountryID]		FOREIGN KEY ([CountryID]) REFERENCES [system].[Country] ([CountryID])
	, CONSTRAINT [FK__Address__AddressTypeID]	FOREIGN KEY ([AddressTypeID]) REFERENCES [system].[AddressType] ([AddressTypeID])
);
GO

CREATE NONCLUSTERED INDEX [IX__Address__CountryID]
	ON [data].[Address] ([CountryID])
	ON FG__INDEXES;
GO

CREATE NONCLUSTERED INDEX [IX__Address__AddressTypeID]
	ON [data].[Address] ([AddressTypeID])
	ON FG__INDEXES;
GO