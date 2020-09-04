CREATE TABLE [customer].[Profile]
(
	[ProfileID]				BIGINT				NOT NULL	CONSTRAINT [DF__Profile__ProfileID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Profile])
	, [RV]					ROWVERSION			NOT NULL
	, [AccountID]			BIGINT				NOT NULL
	, [Code]				[type].[UDT__Code]	NOT NULL
	, [Active]				BIT					NOT NULL	CONSTRAINT [DF__Profile__Active] DEFAULT (1)
	, [PersonID]			BIGINT				NOT NULL
	, [ContactDetailsID]	BIGINT				NOT NULL

	, CONSTRAINT [PK__Profile__ProfileID]			PRIMARY KEY CLUSTERED ([ProfileID])
	, CONSTRAINT [FK__Profile__AccountID]			FOREIGN KEY ([AccountID]) REFERENCES [customer].[Account] ([AccountID])
	, CONSTRAINT [UQ__Profile__AccountID__Code]		UNIQUE NONCLUSTERED ([AccountID], [Code]) ON FG__INDEXES
	, CONSTRAINT [CH__Profile__Code]				CHECK (LEN([Code]) > 0)
	, CONSTRAINT [FK__Profile__PersonID]			FOREIGN KEY ([PersonID]) REFERENCES [data].[Person] ([PersonID])
	, CONSTRAINT [FK__Profile__ContactDetailsID]	FOREIGN KEY ([ContactDetailsID]) REFERENCES [data].[ContactDetails] ([ContactDetailsID])
);
GO

-- TODO: check searches just on accountid still use this index!!!
CREATE NONCLUSTERED INDEX [IX__Profile__AccountID__Active]
	ON [customer].[Profile] ([AccountID], [Active])
	ON FG__INDEXES;
GO

CREATE NONCLUSTERED INDEX [IX__Profile__PersonID]
	ON [customer].[Profile] ([PersonID])
	ON FG__INDEXES;
GO

CREATE NONCLUSTERED INDEX [IX__Profile__ContactDetails]
	ON [customer].[Profile] ([ContactDetailsID])
	ON FG__INDEXES;
GO
