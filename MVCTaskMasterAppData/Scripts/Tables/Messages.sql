--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store messages to do with the Projects
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Messages]
(
	[MessageID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	 /*The Scope of people that the message should be visible to will be done in another table lat*/
    [UserID] INT NOT NULL,
	[ReplyToID] INT NULL, 
	[DirectToID] INT NULL, 
	[ProjectGroupID] INT NULL, 
	[TaskTypesGroupID] INT NULL, 
	[CompanyGroupID] INT NULL, 
	[OfficeGroupID] INT NULL, 
	 /*the message*/
    [Text] NVARCHAR(2000) NULL, 
    
	[Time] DATETIME NOT NULL DEFAULT GetDate(), 

    CONSTRAINT [FK_Messages_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]), 
	CONSTRAINT [FK_Messages_DirectToID] FOREIGN KEY ([DirectToID]) REFERENCES [Users]([UserID]), 
	CONSTRAINT [FK_Messages_ReplyToID] FOREIGN KEY ([ReplyToID]) REFERENCES [Messages]([MessageID]), 
    CONSTRAINT [FK_Messages_ProjectsGroupID] FOREIGN KEY ([ProjectGroupID]) REFERENCES [Projects]([ProjectID]),
	CONSTRAINT [FK_Messages_TaskTypesGroupID] FOREIGN KEY ([TaskTypesGroupID]) REFERENCES [TaskTypes]([TaskTypeID]),
	CONSTRAINT [FK_Messages_CompanyGroupID] FOREIGN KEY ([CompanyGroupID]) REFERENCES [Companys]([CompanyID]),
	CONSTRAINT [FK_Messages_OfficeGroupID] FOREIGN KEY ([OfficeGroupID]) REFERENCES [Offices]([OfficeID]),

	/*TaskTypesGroupID has no context with out ProjectsGroupID*/
	CONSTRAINT [CK_Messages_ProjectsGroupID_And_TaskTypesGroupID_MustBeSetTogether] CHECK (([ProjectGroupID] is not null and [TaskTypesGroupID] is null) or ([ProjectGroupID] is not null and [TaskTypesGroupID] is not null) or ([ProjectGroupID] is null and [TaskTypesGroupID] is null))
)
