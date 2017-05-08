CREATE TABLE [dbo].[MYTableList]
(
	[TableID] TINYINT NOT NULL  PRIMARY KEY IDENTITY(1,1), 
    [TableName] CHAR(20) NOT NULL, 
    CONSTRAINT [AK_MYTableList_Name] UNIQUE ([TableName])
)
