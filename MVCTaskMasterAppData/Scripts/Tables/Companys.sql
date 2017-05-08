CREATE TABLE [dbo].[Companys]
(
	[CompanyID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CompanyName] NVARCHAR(30) NOT NULL, 
    [Description] NVARCHAR(250) NULL, 
    [CompanySite] NCHAR(40) NULL, 
	-- names should start with a letter
    CONSTRAINT [CK_Companys_CompanyName] CHECK ([CompanyName] like '[a-z, A-Z]%') 

)
