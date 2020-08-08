CREATE PROCEDURE [customer].[UP__Table__Customer__Update]
	@pCustomerID			BIGINT						OUTPUT
	, @pRV					ROWVERSION					OUTPUT
	, @pCode				[type].[UDT__Code]			OUTPUT
	, @pCustomer			[type].[UDT__EntityName]	OUTPUT
	, @pActive				BIT							OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF (ISNULL(@pCustomerID, 0) = 0)
		GOTO SPEnd;

	BEGIN TRY
		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		UPDATE [customer].[Customer] SET
			[Code] = @pCode
			, [Customer] = @pCustomer
			, [Active] = @pActive
		WHERE ([CustomerID] = @pCustomerID)
		AND ([RV] = @pRV);

		SET NOCOUNT ON;
		COMMIT TRANSACTION;

		SELECT
			@pRV = [RV]
			, @pCode = [Code]
			, @pCustomer = [Customer]
			, @pActive = [Active]
		FROM [customer].[Customer]
		WHERE [CustomerID] = @pCustomerID;
	END TRY
	BEGIN CATCH
		SET NOCOUNT ON;
		IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION;
		THROW;
	END CATCH

SPEnd:
	RETURN 0;
END
GO

GRANT EXECUTE ON [customer].[UP__Table__Customer__Update] TO [CustomerWriter];
GO
