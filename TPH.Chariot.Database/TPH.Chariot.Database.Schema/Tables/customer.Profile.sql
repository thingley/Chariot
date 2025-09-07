CREATE TABLE [customer].[Profile]
(
	[ProfileID]				BIGINT		NOT NULL	CONSTRAINT [DF__customer__Profile__ProfileID] DEFAULT (NEXT VALUE FOR [customer].[SQ__Profile])
	, [RV]					ROWVERSION	NOT NULL
	, [AccountID]			BIGINT		NOT NULL
	, [PersonID]			BIGINT		NOT NULL
	, [ContactDetailsID]	BIGINT		NOT NULL
	, [Active]				BIT			NOT NULL	CONSTRAINT [DF__customer__Profile__Active] DEFAULT (0)

	, CONSTRAINT [PK__customer__Profile__ProfileID] PRIMARY KEY CLUSTERED ([ProfileID])

	, CONSTRAINT [FK__customer__Profile__AccountID]	FOREIGN KEY ([AccountID]) REFERENCES [customer].[Account] ([AccountID])
	, CONSTRAINT [FK__customer__Profile__PersonID]	FOREIGN KEY ([PersonID]) REFERENCES [customer].[Profile] ([ProfileID])
	, CONSTRAINT [FK__customer__Profile__ContactDetailsID]	FOREIGN KEY ([ContactDetailsID]) REFERENCES [customer].[ContactDetails] ([ContactDetailsID])
)
