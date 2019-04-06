CREATE PROCEDURE [dbo].[Variables_GetList]
	@DymbnailID int
AS
BEGIN
    SELECT
        ID,
        DymbnailID,
        [Name],
        Title,
        Description
    FROM [dbo].[Variables]
    WHERE DymbnailID = @DymbnailID
END