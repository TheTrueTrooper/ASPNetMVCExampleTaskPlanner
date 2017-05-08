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
