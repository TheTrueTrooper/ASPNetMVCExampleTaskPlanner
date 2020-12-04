CREATE TABLE [dbo].[UserEmails]
(
	[EmailID] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[UserID] INT NOT NULL,
    [CompanyID] INT NULL,
	[Email] NVARCHAR(320) NOT NULL,
    [EmailName] VARCHAR (30) NOT NULL,
    [EmailUsageNotesAndPolicies] NVARCHAR (320) NULL,
    [PublicHide] BIT DEFAULT (0) NOT NULL,
    [AddressBookHide] BIT DEFAULT (0) NOT NULL,
    [CompanysHide] BIT DEFAULT (0) NOT NULL,
	[Validated] BIT DEFAULT (0) NOT NULL,

	CONSTRAINT [CK_UserEmails_Email] CHECK ([Email] like '[^.]%[^.][@]%[.]%'), 
	CONSTRAINT [AK_UserEmails_Email] UNIQUE ([Email]),

    CONSTRAINT [FK_UserEmails_Companys] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Companys] ([CompanyID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserEmails_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
)
