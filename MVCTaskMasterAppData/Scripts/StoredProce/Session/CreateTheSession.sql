--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: 
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: ASP.net
--      Writer/Publisher: Microsoft
--      Link: https://www.asp.net/
--      }
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
