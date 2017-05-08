CREATE PROCEDURE [dbo].[CreateTheSession]
	@UserID int,
	@Code char(28)
AS
	if not exists(select UserID from Users where UserID = @UserID)
		return -1;
	if @Code is null and Len(@Code) < 70
		return -2;
	if exists(select UserID from [Sessions] where UserID = UserID)
	begin
		update [Sessions]
		set Code = @Code
		where UserID = @UserID
	end
	else 
	begin 
	insert into [Sessions] (UserID, Code)
	values (@UserID, @Code)
	end
RETURN 0
