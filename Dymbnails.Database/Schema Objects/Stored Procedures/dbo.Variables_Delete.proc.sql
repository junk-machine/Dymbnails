CREATE PROCEDURE [dbo].[Variables_Delete]
	@ID bigint
AS
BEGIN
    DELETE FROM [dbo].[Variables] WHERE ID = @ID
END