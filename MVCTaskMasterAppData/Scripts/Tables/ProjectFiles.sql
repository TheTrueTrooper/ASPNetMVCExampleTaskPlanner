--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about the Projects Files
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[ProjectFiles]
(
	[FileID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProjectID] INT NOT NULL, 
	--size is 2 tothe 16 - 1 = 65,536 - 1 bytes
    [File] VARBINARY(16) NOT NULL, 
    CONSTRAINT [FK_ProjectFiles_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [Projects]([ProjectID])
)
