CREATE PROCEDURE [dbo].[Dymbnails_Update]
    @ID int,
    @Title nvarchar(80),
	@Description nvarchar(1024),
	@Content nvarchar(max)
AS
BEGIN
    UPDATE dbo.Dymbnails
    SET
        Title = @Title,
        Description = @Description,
        Content = @Content
	WHERE
	    ID = @ID
END