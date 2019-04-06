CREATE TABLE [dbo].[Variables]
(
    ID bigint NOT NULL IDENTITY, 
	DymbnailID int NOT NULL,
	[Name] nvarchar(128) NOT NULL,
	Title nvarchar(128) NOT NULL,
	Description nvarchar(1024) NULL
);
