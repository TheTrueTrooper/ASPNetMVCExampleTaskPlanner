CREATE TABLE [dbo].[UsersUserAddressBooks]
(
	OwersID INT NOT NULL PRIMARY KEY,
	OthersID INT NOT NULL,
	Friend bit NOT NULL Default 0,
	Verified bit NOT NULL Default 0

	PRIMARY KEY(OwersID, OthersID),

	CONSTRAINT [FK_UsersUserAddressBooks_Users_Others] FOREIGN KEY (OthersID) REFERENCES [Users]([UserID]),
	CONSTRAINT [FK_UsersUserAddressBooks_Users_Owners] FOREIGN KEY ([OwersID]) REFERENCES [Users]([UserID])

	--CONSTRAINT [FK_UsersUserAddressBooks_OwersID_Not_OthersID] CHECK (OwersID  OthersID) use update and insert on
)
