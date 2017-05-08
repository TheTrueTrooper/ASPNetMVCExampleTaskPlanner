﻿CREATE PROCEDURE [dbo].[ValidateSession]
	@UserID int,
	@Code char(70)
AS
	declare @Success bit = 0,
	@CutOffDate DateTime
	if exists(select TimeLastValidated from [Sessions] where UserID = @UserID and Code = @Code)
	begin
		--check for a time out in case -_- sever is down with attack to create secure gap -_- its all ways worst case with clever hacker 
		select top 1 @CutOffDate = DATEADD(hour, 1, TimeLastValidated) from [Sessions] where UserID = @UserID and Code = @Code
		if @CutOffDate < GETDATE()
		begin -- if its old clean it out and return
			delete from [Sessions] where UserID = @UserID
			SELECT @Success
			return 0
		end
		-- if we made it through all checks then it wass a success also update last TimeLastValidated as to allow contuined work
		set @Success = 1
		update [Sessions]
		 set TimeLastValidated = GETDATE()  
	end

	SELECT @Success
RETURN 0