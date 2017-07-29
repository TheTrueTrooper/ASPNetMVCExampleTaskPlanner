--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data to link tasks
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
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
