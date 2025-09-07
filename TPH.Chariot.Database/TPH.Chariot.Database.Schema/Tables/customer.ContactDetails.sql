CREATE TABLE [customer].[ContactDetails]
(
	[ContactDetailsID]					BIGINT							NOT NULL		CONSTRAINT [DF__customer__ContactDetails__ContactDetailsID] DEFAULT (NEXT VALUE FOR [customer].[SQ__ContactDetails])
	, [RV]								ROWVERSION						NOT NULL
	, [MobileTelephoneCountryCodeID]	BIGINT							NULL
	, [Mobile]							[type].[UDT__TelephoneNumber]	NULL
	, [HomeTelephoneCountryCodeID]		BIGINT							NULL
	, [Home]							[type].[UDT__TelephoneNumber]	NULL
	, [WorkTelephoneCountryCodeID]		BIGINT							NULL
	, [Work]							[type].[UDT__TelephoneNumber]	NULL
	, [Email]							[type].[UDT__Email]				NULL
	, [Note]							[type].[UDT__Note]				NOT NULL

	, CONSTRAINT [PK__customer__ContactDetails__ContactDetailsID] PRIMARY KEY CLUSTERED ([ContactDetailsID])

	, CONSTRAINT [FK__customer__ContactDetails__MobileTelephoneCountryCodeID] FOREIGN KEY ([MobileTelephoneCountryCodeID]) REFERENCES [system].[TelephoneCountryCode] ([TelephoneCountryCodeID])
	, CONSTRAINT [FK__customer__ContactDetails__HomeTelephoneCountryCodeID] FOREIGN KEY ([HomeTelephoneCountryCodeID]) REFERENCES [system].[TelephoneCountryCode] ([TelephoneCountryCodeID])
	, CONSTRAINT [FK__customer__ContactDetails__WorkTelephoneCountryCodeID] FOREIGN KEY ([WorkTelephoneCountryCodeID]) REFERENCES [system].[TelephoneCountryCode] ([TelephoneCountryCodeID])

	, CONSTRAINT [CH__customer__ContactDetails__MobileTelephoneCountryCodeID] CHECK ((([Mobile] IS NOT NULL) AND ([MobileTelephoneCountryCodeID] IS NOT NULL)) OR (([Mobile] IS NULL) AND ([MobileTelephoneCountryCodeID] IS NULL)))
	, CONSTRAINT [CH__customer__ContactDetails__HomeTelephoneCountryCodeID] CHECK ((([Home] IS NOT NULL) AND ([HomeTelephoneCountryCodeID] IS NOT NULL)) OR (([Home] IS NULL) AND ([HomeTelephoneCountryCodeID] IS NULL)))
	, CONSTRAINT [CH__customer__ContactDetails__WorkTelephoneCountryCodeID] CHECK ((([Work] IS NOT NULL) AND ([WorkTelephoneCountryCodeID] IS NOT NULL)) OR (([Work] IS NULL) AND ([WorkTelephoneCountryCodeID] IS NULL)))
);
