﻿--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data in about the USER
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Users]
(
	[UserID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleInitial] NCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [PrimaryPersonalEmailID] INT NULL, 
	[PrimaryPhoneID] INT NULL,
    [Password] NCHAR(50) NOT NULL, 
	[Salt] CHAR(28) NOT NULL,

	--Profile stuff
	--size is 2 tothe 16 - 1 = 65,536 - 1 bytes 
    [Picture] VARBINARY(16) NULL, 
    [Bio] NVARCHAR(250) NULL, 
	[PortfollURL] NVARCHAR(100) NULL,

	--CONSTRAINT [FK_Users_UserEmails] FOREIGN KEY ([PrimaryPersonalEmailID]) REFERENCES [dbo].[UserEmails] ([EmailID]),
 --   CONSTRAINT [FK_Users_UserPhoneNumbers] FOREIGN KEY ([PrimaryPhoneID]) REFERENCES [dbo].[UserPhoneNumbers] ([PhoneNumberID])
)
