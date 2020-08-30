CREATE PROCEDURE [system].[UP__Publish__UpdateSystemTables]
	@pDisplayOutput	BIT = 0
AS
BEGIN
	EXEC [system].[UP__Table__AuditEntity__Populate]
		@pDisplayOutput = @pDisplayOutput;

	RETURN 0;
END;
GO

GRANT EXECUTE ON [system].[UP__Publish__UpdateSystemTables] TO [SystemWriter];
GO
