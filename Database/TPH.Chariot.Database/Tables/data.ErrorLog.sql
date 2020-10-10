CREATE TABLE [data].[ErrorLog]
(
	[ErrorLogID]		BIGINT			NOT NULL	CONSTRAINT [DF__ErrorLog__ErrorLogID] DEFAULT (NEXT VALUE FOR [data].[SQ__ErrorLog])
	, [ErrorNumber]		BIGINT			NULL
	, [ErrorProcedure]	VARCHAR(MAX)	NULL
	, [ErrorMessage]	VARCHAR(MAX)	NULL
	, [ErrorSeverity]	BIGINT			NULL
	, [ErrorState]		BIGINT			NULL

	, CONSTRAINT [PK__ErrorLog__ErrorLogID] PRIMARY KEY CLUSTERED ([ErrorLogID])
);
GO
