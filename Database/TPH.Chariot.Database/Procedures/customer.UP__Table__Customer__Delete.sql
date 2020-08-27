CREATE PROCEDURE [customer].[UP__Table__Customer__Delete]
	@pCustomerID	BIGINT
	, @pRV			ROWVERSION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @rowCount INT;

	BEGIN TRY
		IF (ISNULL(@pCustomerID, 0) = 0)
			THROW 50000, 'ChariotException:A CustomerID value must be provided.', 1;

		IF (EXISTS(SELECT 1 FROM [customer].[Account] WHERE [CustomerID] = @pCustomerID))
			THROW 50000, 'ChariotException:This Customer has Account records. Either delete these Account records or update the Customer as not active.', 1;

		SET NOCOUNT OFF;
		BEGIN TRANSACTION;

		DELETE FROM [customer].[Customer]
		WHERE ([CustomerID] = @pCustomerID)
		AND ([RV] = @pRV);

		SET @rowCount = @@ROWCOUNT;

		SET NOCOUNT ON;
		COMMIT TRANSACTION;

		IF (@rowCount= 0)
		BEGIN
			IF (NOT EXISTS(SELECT 1 FROM [customer].[Customer] WHERE [CustomerID] = @pCustomerID))
				THROW 50000, 'ChariotException:A Customer with this CustomerID was not found.', 1;
			ELSE
				THROW 50000, 'ChariotException:The current record version does not match the indicated record version. The Customer record has not been deleted.', 1;
		END
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

GRANT EXECUTE ON [customer].[UP__Table__Customer__Delete] TO [CustomerWriter];
GO
