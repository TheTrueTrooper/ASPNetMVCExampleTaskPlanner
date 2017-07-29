--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to aid in the linking of Users as workers to offices and companies
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[CompanyWorkers]
(
	[CompanyID] INT NOT NULL, 
    [UserID] INT NOT NULL, 
    [RoleID] INT NOT NULL, 
	[OfficeID] INT NOT NULL, 
    [WorkerID] INT IDENTITY(1,1) NOT NULL, 
    CONSTRAINT [FK_CompanyWorkers_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]), 
	CONSTRAINT [FK_CompanyWorkers_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID]), 
    CONSTRAINT [FK_CompanyWorkers_Rolls] FOREIGN KEY ([RoleID]) REFERENCES [CompanyRoles]([RoleID]), 
	CONSTRAINT [FK_CompanyWorkers_Offices] FOREIGN KEY ([OfficeID]) REFERENCES [Offices]([OfficeID]), 
    CONSTRAINT [PK_CompanyWorkers] PRIMARY KEY ([WorkerID]), 
    CONSTRAINT [AK_CompanyWorkers_Comp_CompanyIDUserIDRoleID] UNIQUE ([CompanyID], [UserID], [RoleID], [OfficeID])

)
