--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data in about a Task
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Tasks]
(
	[TaskID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[SubContractorID] INT NULL,
	[TaskTypeID] INT NULL,
	[ProjectID] INT NOT NULL,
    [Description] NVARCHAR(250) NOT NULL, 

	[DurationTicks] bigint not null default 0,

	[ActualStartDate] DateTime NULL, 
    [ActualEndDate] DateTime NULL,

	--time that the project was opened utc
    [CreationDate] DateTime NOT NULL DEFAULT GETDATE(), 


	CONSTRAINT [FK_Projects_SubContractorID] FOREIGN KEY ([SubContractorID]) REFERENCES [Companys]([CompanyID]),
    CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [Projects]([ProjectID]), 
    CONSTRAINT [FK_Tasks_TaskTypes] FOREIGN KEY ([TaskTypeID]) REFERENCES [TaskTypes]([TaskTypeID]),
)
