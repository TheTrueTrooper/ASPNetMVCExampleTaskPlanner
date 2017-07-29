--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store messages to do with the Projects
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
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
