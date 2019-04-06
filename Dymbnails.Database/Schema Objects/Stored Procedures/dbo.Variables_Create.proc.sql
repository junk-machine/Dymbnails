CREATE PROCEDURE [dbo].[Variables_Create]
	@DymbnailID int,
	@Name varchar(128),
	@Title nvarchar(128),
	@Description nvarchar(1024)
AS
BEGIN
    INSERT INTO [dbo].[Variables] (
        DymbnailID,
        [Name],
        Title,
        Description
    ) VALUES (
        @DymbnailID,
        @Name,
        @Title,
        @Description
    )
    SELECT CAST(SCOPE_IDENTITY() AS bigint)
END