CREATE TABLE [dbo].[UserPhoneNumbers] (
    [PhoneNumberID] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [UserID] INT NOT NULL,
    [CompanyID] INT NULL,
    [PhoneNumber] CHAR (11) NOT NULL,
    [PhoneName] VARCHAR (30) NOT NULL,
    [PhoneUsageNotesAndPolicies] NVARCHAR (320) NULL,
    [PublicHide] BIT DEFAULT (0) NOT NULL,
    [AddressBookHide] BIT DEFAULT (0) NOT NULL,
    [CompanysHide] BIT DEFAULT (0) NOT NULL,
	[Validated] BIT DEFAULT (0) NOT NULL,

    CONSTRAINT [FK_UserPhoneNumbers_Companys] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Companys] ([CompanyID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserPhoneNumbers_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
);