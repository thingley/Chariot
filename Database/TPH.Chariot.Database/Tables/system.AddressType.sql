CREATE TABLE [system].[AddressType]
(
	[AddressTypeID]		BIGINT						NOT NULL
	, [Code]			[type].[UDT__Code]			NOT NULL
	, [AddressType]		[type].[UDT__EntityName]	NOT NULL

	, CONSTRAINT [PK__AddressType__AddressTypeID]	PRIMARY KEY CLUSTERED ([AddressTypeID])
	, CONSTRAINT [UQ__AddressType__Code]			UNIQUE NONCLUSTERED ([Code]) ON FG__INDEXES
	, CONSTRAINT [CH__AddressType__Code]			CHECK (LEN([Code]) > 0)
	, CONSTRAINT [UQ__AddressType__AuditEntity]		UNIQUE NONCLUSTERED ([AddressType]) ON FG__INDEXES
	, CONSTRAINT [CH__AddressType__AuditEntity]		CHECK (LEN([AddressType]) > 0)
);
GO