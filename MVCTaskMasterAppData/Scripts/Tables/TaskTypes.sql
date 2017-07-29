--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to store data in about the Type of a Task
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TABLE [dbo].[TaskTypes]
(
	[TaskTypeID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[CompanyID] INT NOT NULL,
    [Tag] NVARCHAR(20) NOT NULL, 

    CONSTRAINT [FK_TaskTypes_Companys] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID])
)
