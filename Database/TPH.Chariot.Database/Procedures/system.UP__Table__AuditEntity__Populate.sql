CREATE PROCEDURE [system].[UP__Table__AuditEntity__Populate]
	@pDisplayOutput	BIT = 0
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	BEGIN TRY
		DECLARE @Data TABLE
		(
			[AuditEntityID]			BIGINT						NOT NULL
			, [Code]				[type].[UDT__Code]			NOT NULL
			, [AuditEntity]			[type].[UDT__EntityName]	NOT NULL
		);         

		DECLARE @Output TABLE
		(
			[Action]				NVARCHAR(10)	NOT NULL
			, [OutputMessage]		NVARCHAR(MAX)	NOT NULL
		);

		INSERT INTO @Data
		(
			[AuditEntityID]
			, [Code]
			, [AuditEntity]
		)
		VALUES
			(1, N'C', N'Customer')
			, (2, N'A', N'Account');

		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		MERGE [system].[AuditEntity] AS [Target]
		USING @Data AS [Source]
		ON [Target].[AuditEntityID] = [Source].[AuditEntityID]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT
			(
				[AuditEntityID]
				, [Code]
				, [AuditEntity]
			)
			VALUES
			(
				[Source].[AuditEntityID]
				, [Source].[Code]
				, [Source].[AuditEntity]
			)
		WHEN MATCHED THEN
			UPDATE SET
				[Target].[Code]  = [Source].[Code]
				, [Target].[AuditEntity]  = [Source].[AuditEntity]
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		OUTPUT
			$action AS [Action]
			, ISNULL(inserted.[AuditEntity], deleted.[AuditEntity]) AS [OutputMessage]
		INTO @output;

		SET NOCOUNT ON;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		SET NOCOUNT ON;
		IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION;
		THROW;
	END CATCH

	IF (@pDisplayOutput = 1)
		SELECT * FROM @Output;

	RETURN 0;
END;
GO

GRANT EXECUTE ON [system].[UP__Table__AuditEntity__Populate] TO [SystemWriter];
GO