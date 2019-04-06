CREATE PROCEDURE [dbo].[Dymbnails_GetByID]
    @ID int
AS
BEGIN
	SELECT
	    ID,
	    Title,
	    Description,
	    Content,
	    RatingUp AS [Rating.Up],
	    RatingDown AS [Rating.Down]
	FROM
	    dbo.Dymbnails
	WHERE
	    ID = @ID
END