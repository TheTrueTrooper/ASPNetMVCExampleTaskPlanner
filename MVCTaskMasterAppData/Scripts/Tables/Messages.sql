CREATE TABLE [dbo].[Messages]
(
	[MessageID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL,
	[ProjectID] INT NOT NULL, 
    [Text] NVARCHAR(2000) NULL, 
	/*The Scope of people that the message should be visible to will be done in another table lat*/
	

    CONSTRAINT [FK_Messages_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]), 
    CONSTRAINT [FK_Messages_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [Projects]([ProjectID]) 
)
