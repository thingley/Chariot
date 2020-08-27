CREATE PROCEDURE [customer].[UP__Table__Account__Insert]
	@pAccountID		BIGINT						OUTPUT
	, @pRV			ROWVERSION					OUTPUT
	, @pCustomerID	BIGINT						OUTPUT
	, @pCode		[type].[UDT__Code]			OUTPUT
	, @pAccount		[type].[UDT__EntityName]	OUTPUT
	, @pActive		BIT							OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @nextID BIGINT;

	BEGIN TRY
		IF(ISNULL(@pAccountID, 0) <= 0)
			SET @nextID = NEXT VALUE FOR [customer].[SQ__Account];
		ELSE
			SET @nextID = @pAccountID;

		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		INSERT INTO [customer].[Account]
		(
			[AccountID]
			, [CustomerID]
			, [Code]
			, [Account]
			, [Active]
		)
		VALUES
		(
			@nextID
			, @pCustomerID
			, @pCode
			, @pAccount
			, @pActive
		);

		SET NOCOUNT ON;
		COMMIT TRANSACTION;

		SELECT
			@pAccountID = [AccountID]
			, @pRV = [RV]
			, @pCustomerID = [CustomerID]
			, @pCode = [Code]
			, @pAccount = [Account]
			, @pActive = [Active]
		FROM [customer].[Account]
		WHERE [AccountID] = @nextID;
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

GRANT EXECUTE ON [customer].[UP__Table__Account__Insert] TO [CustomerWriter];
GO
