CREATE TABLE [customer].[Account]
(
	[AccountID]			BIGINT							NOT NULL	CONSTRAINT [DF__customer__Account__AccountID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Account])
	, [RV]				ROWVERSION						NOT NULL
	, [Code]			[type].[UDT__EntityCode]		NOT NULL
	, [Name]			[type].[UDT__EntityName]		NOT NULL
	, [CustomerID]		BIGINT							NOT NULL
	, [Active]			BIT								NOT NULL	CONSTRAINT [DF__customer__Account__Active] DEFAULT (0)

	, CONSTRAINT [PK__customer__Account__AccountID]		PRIMARY KEY CLUSTERED ([AccountID])

	, CONSTRAINT [FK__customer__Account__CustomerID]	FOREIGN KEY ([CustomerID]) REFERENCES [customer].[Customer] ([CustomerID])

	, CONSTRAINT [UQ__customer__Account__Code]			UNIQUE NONCLUSTERED ([Code])
	, CONSTRAINT [UQ__customer__Account__Name]			UNIQUE NONCLUSTERED ([Name])

	, CONSTRAINT [CH__customer__Account__Code]			CHECK (LEN([Code]) > 0)
	, CONSTRAINT [CH__customer__Account__Name]			CHECK (LEN([Name]) > 0)
)
