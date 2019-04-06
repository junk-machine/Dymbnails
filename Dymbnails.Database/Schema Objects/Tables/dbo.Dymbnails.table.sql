CREATE TABLE [dbo].[Dymbnails]
(
	ID int NOT NULL IDENTITY(1000, 1), 
	Title nvarchar(80) NOT NULL,
	Description nvarchar(1024) NULL,
	Content nvarchar(max) NOT NULL,
	RatingUp int NOT NULL DEFAULT(0),
	RatingDown int NOT NULL DEFAULT(0)
);
