
CREATE TABLE [dbo].[OperationTypes]
(
	[OperationTypeID] TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OperationType] CHAR(6) NOT NULL,
    CONSTRAINT [AK_OperationType_OperationType] UNIQUE ([OperationType])
)


