﻿--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about a company's post
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[CompanyRoles]
(
	[RoleID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[CompanyID] INT NOT NULL, 
    [RoleName] NVARCHAR(30) NOT NULL, 
    [SuperRole] INT NULL,
	--Can this individual edit the company page....
    [Admin] BIT NOT NULL DEFAULT 0,
	 
	CONSTRAINT [FK_Rolls_SuperRoles] FOREIGN KEY ([SuperRole]) REFERENCES [CompanyRoles]([RoleID]), 
	CONSTRAINT [FK_CompanyRoles_Companys] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID]), 
    CONSTRAINT [AK_CompanyRoles_RoleName] UNIQUE ([CompanyID], [RoleName]),

	CONSTRAINT [CK_Companys_RoleName] CHECK ([RoleName] like '[a-z, A-Z]%') 
)
