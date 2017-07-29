--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data about a company
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[Companys]
(
	[CompanyID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CompanyName] NVARCHAR(30) NOT NULL, 
    [Description] NVARCHAR(250) NULL, 
    [CompanySite] NCHAR(40) NULL, 
	-- names should start with a letter
    CONSTRAINT [CK_Companys_CompanyName] CHECK ([CompanyName] like '[a-z, A-Z]%') 

)
