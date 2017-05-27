CREATE TABLE [dbo].[UserValidations]
(
	[Code] CHAR(28) NOT NULL, 
	[UserID] INT NOT NULL PRIMARY KEY, 
    [TimeIssued] DateTime NOT  NULL DEFAULT GETDATE(), 

    CONSTRAINT [FK_UserValidations_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])
)
