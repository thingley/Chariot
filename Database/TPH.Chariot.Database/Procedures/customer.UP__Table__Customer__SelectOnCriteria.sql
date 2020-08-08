CREATE PROCEDURE [customer].[UP__Table__Customer__SelectOnCriteria]
	@pCode			[type].[UDT__Code]
	, @pCustomer	[type].[UDT__EntityName]
	, @pActive		BIT
AS
BEGIN
	DECLARE
		@Code NVARCHAR(20)= ISNULL(@pCode, '')
		, @Customer NVARCHAR(100) = ISNULL(@pCustomer, '');

	SELECT *
	FROM [customer].[Customer]
	WHERE ((@Code = '') OR ([Code] LIKE ('%' + @pCode + '%')))
	AND ((@Customer = '') OR ([Customer] LIKE ('%' + @pCustomer + '%')))
	AND ((@pActive IS NULL) OR ([Active] = @pActive));
END;
GO

GRANT EXECUTE ON [customer].[UP__Table__Customer__SelectOnCriteria] TO [CustomerReader];
GO
