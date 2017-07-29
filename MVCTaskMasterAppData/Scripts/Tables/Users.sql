--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
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
    [Email] NVARCHAR(320) NOT NULL, 
    [Password] NCHAR(50) NOT NULL, 
	[Salt] CHAR(28) NOT NULL,
    [HomePhone] CHAR(11) NOT NULL, 
    [CellPhone] CHAR(11) NULL, 
    [WorkPhone] CHAR(11) NULL, 

    CONSTRAINT [CK_Users_Email] CHECK (Email like '[^.]%[^.][@]%[.]%'), 
    CONSTRAINT [AK_Users_Email] UNIQUE ([Email])
)
