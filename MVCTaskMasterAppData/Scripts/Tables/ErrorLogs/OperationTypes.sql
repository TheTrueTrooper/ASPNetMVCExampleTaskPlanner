--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about a Error Types
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[OperationTypes]
(
	[OperationTypeID] TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OperationType] CHAR(6) NOT NULL,
    CONSTRAINT [AK_OperationType_OperationType] UNIQUE ([OperationType])
)


