CREATE PROCEDURE [customer].[UP__Table__Customer__SelectAll]
AS
BEGIN
	SELECT *
	FROM [customer].[Customer];
END;
GO

GRANT EXECUTE ON [customer].[UP__Table__Customer__SelectAll] TO [CustomerReader];
GO
