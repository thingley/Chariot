CREATE TABLE [data].[ContactDetails]
(
	[ContactDetailsID]			BIGINT							NOT NULL	CONSTRAINT [DF__data__ContactDetails__ContactDetailsID] DEFAULT (NEXT VALUE FOR [data].[SQ__ContactDetails])
	, [RV]						ROWVERSION						NOT NULL
	, [EmailAddress]			[type].[UDT__EmailAddress]		NOT NULL
	, [MobileTelephoneNumber]	[type].[UDT__TelephoneNumber]	NOT NULL
	, [WorkTelephoneNumber]		[type].[UDT__TelephoneNumber]	NOT NULL
	, [HomeTelephoneNumber]		[type].[UDT__TelephoneNumber]	NOT NULL

	, CONSTRAINT [PK__data__ContactDetails__ContactDetailsID] PRIMARY KEY ([ContactDetailsID])
);
GO
