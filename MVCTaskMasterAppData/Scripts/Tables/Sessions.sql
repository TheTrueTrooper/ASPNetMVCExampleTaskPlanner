--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to aid in the verify of a session that is a normalized drop away 
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Sessions]
(
    [Code] CHAR(28) NOT NULL, 
	[UserID] INT NOT NULL PRIMARY KEY, 
    [TimeLastValidated] DateTime NOT  NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_Sessions_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])
)
