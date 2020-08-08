CREATE TABLE [customer].[Customer]
(
	[CustomerID]	BIGINT			NOT NULL	CONSTRAINT [DF__customer__Customer__CustomerID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Customer])
	, [RV]			ROWVERSION		NOT NULL
	, [Code]		[type].[UDT__Code]
	, [Customer]	[type].[UDT__EntityName]
	, [Active]		BIT				NOT NULL	CONSTRAINT [DF__customer__Customer__Active] DEFAULT (0)

	, CONSTRAINT [PK__customer__Customer__CustomerID]	PRIMARY KEY CLUSTERED ([CustomerID])
	, CONSTRAINT [UQ__customer__Customer__Code]			UNIQUE NONCLUSTERED ([Code]) ON FG__INDEXES
	, CONSTRAINT [CH__customer__Customer__Code]			CHECK (LEN(Code) > 0)
	, CONSTRAINT [UQ__customer__Customer__Customer]		UNIQUE NONCLUSTERED ([Customer]) ON FG__INDEXES
	, CONSTRAINT [CH__customer__Customer__Customer]		CHECK (LEN(Customer) > 0)
);
