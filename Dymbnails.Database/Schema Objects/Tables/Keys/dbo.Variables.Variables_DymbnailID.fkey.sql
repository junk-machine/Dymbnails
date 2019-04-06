ALTER TABLE [dbo].[Variables]
	ADD CONSTRAINT [Variables_DymbnailID] 
	FOREIGN KEY (DymbnailID)
	REFERENCES Dymbnails (ID)	

