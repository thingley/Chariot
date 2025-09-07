CREATE TABLE [system].[TelephoneCountryCode]
(
	[TelephoneCountryCodeID]		BIGINT						NOT NULL	CONSTRAINT [DF__system__TelephoneCountryCode__TelephoneCountryCodeID] DEFAULT (NEXT VALUE FOR [system].[SQ__TelephoneCountryCode])
	, [RV]							ROWVERSION					NOT NULL
	, [Code]						[type].[UDT__EntityCode]	NOT NULL
	, [Name]						[type].[UDT__EntityName]	NOT NULL

	, CONSTRAINT [PK__system__TelephoneCountryCode__TelephoneCountryCodeID] PRIMARY KEY CLUSTERED ([TelephoneCountryCodeID])

	, CONSTRAINT [UQ__system__TelephoneCountryCode__Code]					UNIQUE NONCLUSTERED ([Code])
	, CONSTRAINT [UQ__system__TelephoneCountryCode__Name]					UNIQUE NONCLUSTERED ([Name])

	, CONSTRAINT [CH__system__TelephoneCountryCode__Code]					CHECK (LEN([Code]) > 0)
	, CONSTRAINT [CH__system__TelephoneCountryCode__Name]					CHECK (LEN([Name]) > 0)
)
