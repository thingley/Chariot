CREATE PROCEDURE [customer].[UP__Table__Customer__Insert]
	@pCustomerID			BIGINT						OUTPUT
	, @pRV					ROWVERSION					OUTPUT
	, @pCode				[type].[UDT__Code]			OUTPUT
	, @pCustomer			[type].[UDT__EntityName]	OUTPUT
	, @pActive				BIT							OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @nextID BIGINT;

	BEGIN TRY
		IF (ISNULL(@pCustomerID, 0) <= 0)
			SET @nextID = NEXT VALUE FOR [customer].[SQ__Customer];
		ELSE
			SET @nextID = @pCustomerID;

		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		INSERT INTO [customer].[Customer]
		(
			[CustomerID]
			, [Code]
			, [Customer]
			, [Active]
		)
		VALUES
		(
			@nextID
			, @pCode
			, @pCustomer
			, @pActive
		);

		SET NOCOUNT ON;
		COMMIT TRANSACTION;

		SELECT
			@pCustomerID = [CustomerID]
			, @pRV = [RV]
			, @pCode = [Code]
			, @pCustomer = [Customer]
			, @pActive = [Active]
		FROM [customer].[Customer]
		WHERE [CustomerID] = @nextID;
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

GRANT EXECUTE ON [customer].[UP__Table__Customer__Insert] TO [CustomerWriter];
GO
