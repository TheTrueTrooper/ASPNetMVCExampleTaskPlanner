CREATE TABLE [dbo].[Tasks]
(
	[TaskID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[SubContractorID] INT NOT NULL,
	[TaskTypeID] INT NOT NULL,
	[ProjectID] INT NOT NULL,
	[PrevTask] INT NULL,
	[NextTask] INT NULL,
    [Description] NVARCHAR(250) NULL, 

    [StartDate] DateTime NOT NULL, 
    [EndDate] DateTime NULL, 

	--time that the project was opened utc
    [CreationDate] DateTime NOT NULL DEFAULT GETDATE(), 


	CONSTRAINT [FK_Projects_SubContractorID] FOREIGN KEY ([SubContractorID]) REFERENCES [Companys]([CompanyID]),
    CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [Projects]([ProjectID]), 
    CONSTRAINT [FK_Tasks_Tasks_Prev] FOREIGN KEY ([PrevTask]) REFERENCES [Tasks]([TaskID]), 
    CONSTRAINT [FK_Tasks_Tasks_Next] FOREIGN KEY ([NextTask]) REFERENCES [Tasks]([TaskID]), 
    CONSTRAINT [FK_Tasks_TaskTypes] FOREIGN KEY ([TaskTypeID]) REFERENCES [TaskTypes]([TaskTypeID]),

	CONSTRAINT [CK_Tasks_EndDate] CHECK (EndDate < StartDate or EndDate is NULL), 
)
