CREATE TYPE [type].[UTT__Profile] AS TABLE
(
	[Code]						[type].[UDT__Code]
	, [Title]					[type].[UDT__Title]
	, [FirstName]				[type].[UDT__FirstName]
	, [LastName]				[type].[UDT__LastName]
	, [EmailAddress]			[type].[UDT__EmailAddress]
	, [MobileTelephoneNumber]	[type].[UDT__TelephoneNumber]
	, [WorkTelephoneNumber]		[type].[UDT__TelephoneNumber]
	, [HomeTelephoneNumber]		[type].[UDT__TelephoneNumber]
)
