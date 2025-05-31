CREATE TABLE [customer].[Customer]
(
	[CustomerID]	BIGINT						NOT NULL	CONSTRAINT [DF__customer__Customer__CustomerID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Customer])
	, [RV]			ROWVERSION					NOT NULL
	, [Code]		[type].[UDT__EntityCode]	NOT NULL
	, [Name]		[type].[UDT__EntityName]	NOT NULL
	, [Active]		BIT							NOT NULL	CONSTRAINT [DF__customer__Customer__Active] DEFAULT (0)

	, CONSTRAINT [PK__customer__Customer__CustomerID]	PRIMARY KEY CLUSTERED ([CustomerID])

	, CONSTRAINT [UQ__customer__Customer__Code]			UNIQUE NONCLUSTERED ([Code])
	, CONSTRAINT [UQ__customer__Customer__Name]			UNIQUE NONCLUSTERED ([Name])

	, CONSTRAINT [CH__customer__Customer__Code]			CHECK (LEN([Code]) > 0)
	, CONSTRAINT [CH__customer__Customer__Name]			CHECK (LEN([Name]) > 0)
);
