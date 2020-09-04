CREATE TABLE [system].[AuditEntity]
(
	[AuditEntityID]		BIGINT						NOT NULL
	, [Code]			[type].[UDT__Code]			NOT NULL
	, [AuditEntity]		[type].[UDT__EntityName]	NOT NULL

	, CONSTRAINT [PK__AuditEntity__AuditEntityID]	PRIMARY KEY CLUSTERED ([AuditEntityID])
	, CONSTRAINT [UQ__AuditEntity__Code]			UNIQUE NONCLUSTERED ([Code]) ON FG__INDEXES
	, CONSTRAINT [CH__AuditEntity__Code]			CHECK (LEN([Code]) > 0)
	, CONSTRAINT [UQ__AuditEntity__AuditEntity]		UNIQUE NONCLUSTERED ([AuditEntity]) ON FG__INDEXES
	, CONSTRAINT [CH__AuditEntity__AuditEntity]		CHECK (LEN([AuditEntity]) > 0)
);
GO
