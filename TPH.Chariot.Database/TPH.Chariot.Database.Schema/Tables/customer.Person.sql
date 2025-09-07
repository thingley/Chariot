CREATE TABLE [customer].[Person]
(
	[PersonID]			BIGINT							NOT NULL	CONSTRAINT [DF__customer__Person__PersonID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Person])
	, [RV]				ROWVERSION						NOT NULL
	, [Title]			[type].[UDT__PersonTitle]		NOT NULL
	, [Forename]		[type].[UDT__PersonForename]	NOT NULL
	, [Surname]			[type].[UDT__PersonSurname]		NOT NULL

	, CONSTRAINT [PK__customer__Person__PersonID] PRIMARY KEY CLUSTERED ([PersonID])
	
	, CONSTRAINT [CH__customer__Profile__Title]			CHECK (LEN([Title]) > 0)
	, CONSTRAINT [CH__customer__Profile__Forename]		CHECK (LEN([Forename]) > 0)
	, CONSTRAINT [CH__customer__Profile__Surname]		CHECK (LEN([Surname]) > 0)
);
