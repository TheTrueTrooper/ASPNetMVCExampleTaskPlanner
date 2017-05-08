CREATE TABLE [dbo].[Sessions]
(
    [Code] CHAR(28) NOT NULL, 
	[UserID] INT NOT NULL PRIMARY KEY, 
    [TimeLastValidated] DateTime NOT  NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_Sessions_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])
)
