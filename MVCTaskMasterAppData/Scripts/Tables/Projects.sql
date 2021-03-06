﻿--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about the Projects
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Projects]
(
	[ProjectID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ProjectName] NVARCHAR(70) NOT NULL, 
    [CompanyID] INT NULL, 
	-- WorkerID that is rep to Pro
    [ManagerID] INT NOT NULL, 
	[ProjectTaskTypeManagersID] INT NULL, 
    [Address] NVARCHAR(30) NOT NULL, 
    [PostalCode] NVARCHAR(10) NOT NULL, 
    [Country] NVARCHAR(10) NOT NULL, 
    [Province] NVARCHAR(10) NOT NULL, 
    [City] NVARCHAR(10) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 

    [StartDate] DateTime NOT NULL, 
    [EndDate] DateTime NULL, 

	[ActualStartDate] DateTime NULL, 
    [ActualEndDate] DateTime NULL,

	[TaskManagersCanEdit] BIT DEFAULT 0,
	[TaskManagersCanPost] BIT DEFAULT 1,
	[TaskManagersCanDeletePost] BIT DEFAULT 0,
	[TaskManagersCanDeleteFile] BIT DEFAULT 0,
	[TaskManagersCanUploadFile] BIT DEFAULT 1,

	--time that the project was opened utc
    [CreationDate] DateTime NOT NULL DEFAULT GETDATE(), 

    CONSTRAINT [FK_Projects_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID]), 
    CONSTRAINT [FK_Projects_User_PManager] FOREIGN KEY ([ManagerID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [FK_Projects_ProjectTaskTypeManagersID] FOREIGN KEY ([ProjectTaskTypeManagersID]) REFERENCES [ProjectTaskTypeManagers]([ProjectTaskTypeManagersID]),

	CONSTRAINT [CK_Projects_PostalCode] CHECK (PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]'), 
    CONSTRAINT [CK_Projects_EndDate] CHECK (EndDate > StartDate or EndDate is NULL)
)
