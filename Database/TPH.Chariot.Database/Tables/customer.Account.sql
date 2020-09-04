CREATE TABLE [customer].[Account]
(
	[AccountID]			BIGINT						NOT NULL	CONSTRAINT [DF__Account__AccountID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Account])
	, [RV]				ROWVERSION					NOT NULL
	, [CustomerID]		BIGINT						NOT NULL
	, [Code]			[type].[UDT__Code]			NOT NULL
	, [Account]			[type].[UDT__EntityName]	NOT NULL
	, [Active]			BIT							NOT NULL	CONSTRAINT [DF__Account__Active] DEFAULT (0)

	, CONSTRAINT [PK__Account__AccountID]			PRIMARY KEY CLUSTERED ([AccountID])
	, CONSTRAINT [FK__Account__CustomerID]			FOREIGN KEY ([CustomerID]) REFERENCES [customer].[Customer] ([CustomerID])
	, CONSTRAINT [UQ__Account__CustomerID__Code]	UNIQUE NONCLUSTERED ([CustomerID], [Code]) ON FG__INDEXES
	, CONSTRAINT [CH__Account__Code]				CHECK (LEN([Code]) > 0)
	, CONSTRAINT [UQ__Account__CustomerID__Account]	UNIQUE NONCLUSTERED ([CustomerID], [Account]) ON FG__INDEXES
	, CONSTRAINT [CH__Account__Account]				CHECK (LEN([Account]) > 0)
);
GO

-- TODO: check searches just on accountid still use this index!!!
CREATE NONCLUSTERED INDEX [IX__Account__CustomerID__Active]
	ON [customer].[Account] ([CustomerID], [Active])
	ON FG__INDEXES;
GO
