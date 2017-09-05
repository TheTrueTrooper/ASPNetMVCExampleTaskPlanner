CREATE TABLE [dbo].[ProjectTaskTypeManagers]
(
	[ProjectTaskTypeManagersID] INT NOT NULL PRIMARY KEY, 
    [TaskTypeID] INT NOT NULL, 
    [ManagerID] INT NULL, 
    [ManagingCompanysID] INT NULL,

	CONSTRAINT [FK_ProjectTaskTypeManagers_User_ManagerID] FOREIGN KEY ([ManagerID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [FK_ProjectTaskTypeManagers_UserManagingCompanysID] FOREIGN KEY ([ManagingCompanysID]) REFERENCES [Companys]([CompanyID])
)
