CREATE PROCEDURE [dbo].[Dymbnails_GetList]
AS
BEGIN
	SELECT
	    ID,
	    Title,
	    Description,
	    RatingUp AS [Rating.Up],
	    RatingDown AS [Rating.Down]
	FROM
	    dbo.Dymbnails
	WHERE
	    ID >= 1000
END