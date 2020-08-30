CREATE FUNCTION [sfunc].[GetAuditDescriptionForBooleanColumn]
(
    @pColumnName VARCHAR(MAX)
	, @pOldValue BIT
	, @pNewValue BIT
)
RETURNS VARCHAR(MAX)
AS
BEGIN
    DECLARE @OldIsSet BIT = ISNULL(@pOldValue, 0);
    DECLARE @NewIsSet BIT = ISNULL(@pNewValue, 0);

	RETURN @pColumnName +	CASE
								WHEN (@OldIsSet = 0 AND @NewIsSet = 1) THEN N' flag switched ON'
								WHEN (@OldIsSet = 1 AND @NewIsSet = 0) THEN N' flag switched OFF'
								WHEN (@OldIsSet = 1 AND @NewIsSet = 1) THEN N' flag remains ON'
								WHEN (@OldIsSet = 0 AND @NewIsSet = 0) THEN N' flag remains OFF'
							END;
END;
