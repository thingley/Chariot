CREATE PROCEDURE [data].[UP__Table__ErrorLog__Insert]
	@pErrorLogID		BIGINT
	, @pErrorNumber		BIGINT
	, @pErrorProcedure	VARCHAR(MAX)
	, @pErrorMessage	VARCHAR(MAX)
	, @pErrorSeverity	BIGINT
	, @pErrorState		BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @nextID BIGINT;

	BEGIN TRY
		IF(ISNULL(@pErrorLogID, 0) <= 0)
			SET @nextID = NEXT VALUE FOR [data].[SQ__ErrorLog];
		ELSE
			SET @nextID = @pErrorLogID;

		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		INSERT INTO [data].[ErrorLog]
		(
			[ErrorLogID]
			, [ErrorNumber]
			, [ErrorProcedure]
			, [ErrorMessage]
			, [ErrorSeverity]
			, [ErrorState]
		)
		VALUES
		(
			@pErrorLogID
			, @pErrorNumber
			, @pErrorProcedure
			, @pErrorMessage
			, @pErrorSeverity
			, @pErrorState
		);

		SET NOCOUNT ON;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		SET NOCOUNT ON;
		IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION;
		THROW;
	END CATCH

SPEnd:
	RETURN 0;
END;
GO

GRANT EXECUTE ON [data].[UP__Table__ErrorLog__Insert] TO [DataWriter];
GO
