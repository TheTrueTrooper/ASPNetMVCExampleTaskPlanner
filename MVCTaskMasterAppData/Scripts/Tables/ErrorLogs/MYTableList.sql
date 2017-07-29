--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about a the Tables
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[MYTableList]
(
	[TableID] TINYINT NOT NULL  PRIMARY KEY IDENTITY(1,1), 
    [TableName] CHAR(20) NOT NULL, 
    CONSTRAINT [AK_MYTableList_Name] UNIQUE ([TableName])
)
