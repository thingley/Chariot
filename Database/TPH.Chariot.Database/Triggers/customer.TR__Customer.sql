CREATE TRIGGER [customer].[TR__customer__Customer] ON [customer].[Customer] FOR DELETE, INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE
		@insert				BIT = 0
		, @update			BIT = 0
		, @delete			BIT = 0
		, @void				BIT = 0
		, @auditTimeStamp	DATETIME2 = GETDATE();

	IF EXISTS (SELECT 1 FROM [inserted])
		IF EXISTS(SELECT 1 FROM [deleted])
			SET @update = 1;
		ELSE
			SET @insert = 1;
	ELSE
		IF EXISTS(SELECT 1 FROM [deleted])
			SET @delete = 1;
		ELSE
			SET @void = 1;

	IF (@void = 1)
		RETURN;

	-- Validation

	-- Auditing
	IF (@insert = 1)
	BEGIN
		INSERT INTO [system].[AuditLog]
		(
			[AuditEntityID]
			, [EntityID]
			, [EntityRV]
			, [Date]
			, [Description]
		)
		SELECT
			1
			, i.[CustomerID]
			, i.[RV]
			, @auditTimeStamp
			, 'Created.'
		FROM [inserted] i;

		GOTO TREnd;
	END

	IF (@update = 1)
	BEGIN
		IF (UPDATE([Code]))
		BEGIN
			INSERT INTO [system].[AuditLog]
			(
				[AuditEntityID]
				, [EntityID]
				, [EntityRV]
				, [Date]
				, [Description]
			)
			SELECT
				1
				, i.[CustomerID]
				, i.[RV]
				, @auditTimeStamp
				, [sfunc].[GetAuditDescriptionForCharacterColumn]('Code', d.[Code], i.[Code])
			FROM [inserted] i
			JOIN [deleted] d ON i.[CustomerID] = d.[CustomerID]
			WHERE i.[Code] <> d.[Code];
		END

		IF (UPDATE([Customer]))
		BEGIN
			INSERT INTO [system].[AuditLog]
			(
				[AuditEntityID]
				, [EntityID]
				, [EntityRV]
				, [Date]
				, [Description]
			)
			SELECT
				1
				, i.[CustomerID]
				, i.[RV]
				, @auditTimeStamp
				, [sfunc].[GetAuditDescriptionForCharacterColumn]('Customer', d.[Customer], i.[Customer])
			FROM inserted i
			JOIN deleted d ON i.[CustomerID] = d.[CustomerID]
			WHERE i.[Customer] <> d.[Customer];
		END

		IF (UPDATE([Active]))
		BEGIN
			INSERT INTO [system].[AuditLog]
			(
				[AuditEntityID]
				, [EntityID]
				, [EntityRV]
				, [Date]
				, [Description]
			)
			SELECT
				1
				, i.[CustomerID]
				, i.[RV]
				, @auditTimeStamp
				, [sfunc].[GetAuditDescriptionForCharacterColumn]('Active', d.[Active], i.[Active])
			FROM [inserted] i
			JOIN [deleted] d ON i.[CustomerID] = d.[CustomerID]
			WHERE i.[Active] <> d.[Active];
		END

		GOTO TREnd;
	END
	
	IF (@delete = 1)
	BEGIN
		INSERT INTO [system].[AuditLog]
		(
			[AuditEntityID]
			, [EntityID]
			, [EntityRV]
			, [Date]
			, [Description]
		)
		SELECT
			1
			, d.[CustomerID]
			, d.[RV]
			, @auditTimeStamp
			, 'Deleted.'
		FROM [deleted] d;

		GOTO TREnd;
	END

	TREnd:
END