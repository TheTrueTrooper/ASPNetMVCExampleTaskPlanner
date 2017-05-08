CREATE TABLE [dbo].[Projects]
(
	[ProjectID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ProjectName] NVARCHAR(70) NOT NULL, 
    [CompanyID] INT NOT NULL, 
	-- WorkerID that is rep to Pro
    [ManagerID] INT NOT NULL, 
    [Address] NVARCHAR(30) NOT NULL, 
    [PostalCode] NVARCHAR(10) NOT NULL, 
    [Country] NVARCHAR(10) NOT NULL, 
    [Province] NVARCHAR(10) NOT NULL, 
    [City] NVARCHAR(10) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 

    [StartDate] DateTime NOT NULL, 
    [EndDate] DateTime NULL, 

	--time that the project was opened utc
    [CreationDate] DateTime NOT NULL DEFAULT GETDATE(), 


    CONSTRAINT [FK_Projects_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID]), 
    CONSTRAINT [FK_Projects_User_PManager] FOREIGN KEY ([ManagerID]) REFERENCES [Users]([UserID]),

	CONSTRAINT [CK_Projects_PostalCode] CHECK (PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]'), 
    CONSTRAINT [CK_Projects_EndDate] CHECK (EndDate < StartDate or EndDate is NULL)
)
