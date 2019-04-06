CREATE PROCEDURE [dbo].[Dymbnails_GetContentByID]
	@ID int
AS
BEGIN
	SELECT Content FROM [dbo].[Dymbnails] WHERE ID = @ID
END
