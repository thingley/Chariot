CREATE FUNCTION [sfunc].[GetAuditDescriptionForCharacterColumn]
(
    @pColumnName NVARCHAR(MAX)
	, @pOldValue NVARCHAR(MAX)
	, @pNewValue NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @OldIsEmpty BIT = CASE LEN(ISNULL(@pOldValue, '')) WHEN 0 THEN 1 ELSE 0 END;
    DECLARE @NewIsEmpty BIT = CASE LEN(ISNULL(@pNewValue, '')) WHEN 0 THEN 1 ELSE 0 END;

	RETURN @pColumnName
			+	CASE
					WHEN ((@OldIsEmpty = 0) AND (@NewIsEmpty = 0))
						THEN N' changed from ''' + @pOldValue + N''' to ''' + @pNewValue + N''''
					WHEN ((@OldIsEmpty = 1) AND (@NewIsEmpty = 0))
						THEN N' set to ''' + @pNewValue + N''''
					WHEN ((@OldIsEmpty = 0) AND (@NewIsEmpty = 1))
						THEN N' changed from ''' + @pOldValue + N''' to empty'
					WHEN ((@OldIsEmpty = 1) AND (@NewIsEmpty = 1))
						THEN N' remains empty'
				END;
END;
GO