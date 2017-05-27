CREATE TABLE [dbo].[TaskLinkers]
(
	[linkerID] INT NOT NULL PRIMARY KEY, 
	[TaskID] INT NOT NULL, 
	[NextTaskID] INT NOT NULL, 

    CONSTRAINT [FK_TaskLinkers_Tasks_Owner] FOREIGN KEY ([TaskID]) REFERENCES [Tasks]([TaskID]),
	CONSTRAINT [FK_TaskLinkers_Tasks_Next] FOREIGN KEY ([NextTaskID]) REFERENCES [Tasks]([TaskID]),

	CONSTRAINT [AK_TaskLinkers_Tasks_OwnerNext] UNIQUE ([TaskID], [NextTaskID]), 

    CONSTRAINT [CK_TaskLinkers_TaskNext] CHECK ([TaskID] != [NextTaskID])
)
