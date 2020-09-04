CREATE TABLE [customer].[ProfileAddress]
(
	[ProfileAddressID]		BIGINT		NOT NULL	CONSTRAINT [DF__ProfileAddress__ProfileAddressID] DEFAULT (NEXT VALUE FOR [customer].[SQ__ProfileAddress])
	, [ProfileID]			BIGINT		NOT NULL
	, [AddressID]			BIGINT		NOT NULL

	, CONSTRAINT [PK__ProfileAddress__ProfileAddressID]	PRIMARY KEY CLUSTERED ([ProfileAddressID])
	, CONSTRAINT [FK__ProfileAddress__ProfileID]		FOREIGN KEY ([ProfileID]) REFERENCES [customer].[Profile] ([ProfileID])
	, CONSTRAINT [FK__ProfileAddress__AddressID]		FOREIGN KEY ([AddressID]) REFERENCES [data].[Address] ([AddressID])
);
GO

CREATE INDEX [IX__ProfileAddress__ProfileID]
	ON [customer].[ProfileAddress] ([ProfileID])
	ON FG__INDEXES;
GO

CREATE INDEX [IX__ProfileAddress__AddressID]
	ON [customer].[ProfileAddress] ([AddressID])
	ON FG__INDEXES;
GO

