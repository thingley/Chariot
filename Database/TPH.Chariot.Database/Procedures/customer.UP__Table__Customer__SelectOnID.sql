CREATE PROCEDURE [customer].[UP__Table__Customer__SelectOnID]
	@pCustomerID	BIGINT
AS
BEGIN
	SELECT *
	FROM [customer].[Customer]
	WHERE [CustomerID] = @pCustomerID;
END;
GO

GRANT EXECUTE ON [customer].[UP__Table__Customer__SelectOnID] TO [CustomerReader];
GO
