CREATE TABLE [system].[AuditLog]
(
	[AuditLogID]		BIGINT			NOT NULL	CONSTRAINT [DF__AuditLog__AuditLogID] DEFAULT (NEXT VALUE FOR [system].[SQ__AuditLog])
	, [User]			NVARCHAR(MAX)	NOT NULL	CONSTRAINT [DF__AuditLog__User] DEFAULT (SUSER_SNAME())
	, [AuditEntityID]	BIGINT			NOT NULL
	, [EntityID]		BIGINT			NOT NULL
	, [EntityRV]		BINARY(8)		NOT NULL
	, [Date]			DATETIME2		NOT NULL
	, [Description]		NVARCHAR(MAX)	NOT NULL

	, CONSTRAINT [PK__AuditLog__AuditLogID]		PRIMARY KEY CLUSTERED ([AuditLogID])
	, CONSTRAINT [FK__AuditLog__AuditEntityID]	FOREIGN KEY ([AuditEntityID]) REFERENCES [system].[AuditEntity] ([AuditEntityID])
	, CONSTRAINT [CH__AuditLog__Description]	CHECK (LEN([Description]) > 0)
);
GO

CREATE NONCLUSTERED INDEX [IX__AuditLog__AuditEntityID__EntityID]
	ON [system].[AuditLog] ([AuditEntityID], [EntityID]) ON FG__INDEXES;
GO
