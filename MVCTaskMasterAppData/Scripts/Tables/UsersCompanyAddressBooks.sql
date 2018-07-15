CREATE TABLE [dbo].[UsersCompanyAddressBooks]
(
	OwersID INT NOT NULL,
	CompanyID INT NOT NULL,
	Affiliated bit NOT NULL Default 0,
	Verified bit NOT NULL Default 0,

	PRIMARY KEY(OwersID, CompanyID),

	CONSTRAINT [FK_UsersCompanyAddressBooks_Companys] FOREIGN KEY (CompanyID) REFERENCES [Companys]([CompanyID]),
	CONSTRAINT [FK_UsersCompanyAddressBooks_Users] FOREIGN KEY ([OwersID]) REFERENCES [Users]([UserID])
)
