CREATE TRIGGER [UsersUserAddressBooks_TriggerCheck_OwersID_Not_OthersID]
	ON [dbo].[UsersUserAddressBooks]
	FOR INSERT, UPDATE
	AS
	BEGIN
		declare @OwnerID int, @OthersID int
		select @OthersID = OthersID, @OwnerID = OwersID from inserted

		if(@OwnerID = @OthersID)
		begin
			raiserror('OwnerID cannot be equal to OthersID', 547, 0)
			rollback
		end
	END
