--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about a the Errors in data entry that are not handled. or log handled ones.
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[ErrorTable]
(
	[ErrorID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Time] DATETIME NOT NULL DEFAULT GetDate(), 
    [ErrorMessage] CHAR(100) NOT NULL, 
    [Type] TINYINT NOT NULL, 
    [Table] TINYINT NOT NULL, 
    [SQLErrorCode] INT NOT NULL, 
    [MyErrorCode] INT NOT NULL, 

    CONSTRAINT [FK_ErrorTable_MYTables] FOREIGN KEY ([Type]) REFERENCES [OperationTypes]([OperationTypeID]), 
    CONSTRAINT [FK_ErrorTable_OperationTypes] FOREIGN KEY ([Table]) REFERENCES [MYTableList]([TableID])
)
