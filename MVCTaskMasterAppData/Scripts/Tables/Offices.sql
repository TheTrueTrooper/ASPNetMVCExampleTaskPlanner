CREATE TABLE [dbo].[Offices]
(
	[OfficeID] INT IDENTITY(1,1) NOT NULL, 
    [CompanyID] INT NOT NULL, 
	[Country] VARCHAR(20) NOT NULL,
	[Province] NCHAR(20) NOT NULL,
    [City] NCHAR(20) NOT NULL, 
    [Address] NCHAR(30) NOT NULL, 
    [PostalCode] CHAR(6) NOT NULL, 
    [Phone] CHAR(14) NOT NULL, 
	[Fax] CHAR(14) NULL,
    [OfficeName] NCHAR(20) NOT NULL, 

    PRIMARY KEY ([OfficeID]), 

    CONSTRAINT [FK_Offices_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Companys]([CompanyID]), 
    CONSTRAINT [CK_Offices_Phone] CHECK (Phone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'), 
	CONSTRAINT [CK_Offices_Fax] CHECK (Fax like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'), 
    CONSTRAINT [CK_Offices_PostalCode] CHECK (PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]' or PostalCode like '[0-9][0-9][0-9][0-9][0-9]' or PostalCode like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'), --5 dig followed by 4
)
