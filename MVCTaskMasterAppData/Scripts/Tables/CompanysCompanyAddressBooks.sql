CREATE TABLE [dbo].[CompanysCompanyAddressBooks]
(
	OwersID INT NOT NULL,
	UsersID INT NOT NULL,
	Affiliated bit NOT NULL Default 0,
	Verified bit NOT NULL Default 0,

	PRIMARY KEY(OwersID, UsersID),

	CONSTRAINT [FK_CompanysCompanyAddressBooks_Companys] FOREIGN KEY (OwersID) REFERENCES [Companys]([CompanyID]),
	CONSTRAINT [FK_CompanysCompanyAddressBooks_Users] FOREIGN KEY ([UsersID]) REFERENCES [Users]([UserID])
)
