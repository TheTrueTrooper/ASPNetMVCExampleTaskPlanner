CREATE TABLE [dbo].[TaskTypes]
(
	[TaskTypeID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[CompanyID] INT NOT NULL,
    [Tag] NVARCHAR(20) NOT NULL, 

    CONSTRAINT [FK_TaskTypes_Companys] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID])
)
