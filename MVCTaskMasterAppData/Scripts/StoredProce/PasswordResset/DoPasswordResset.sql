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
CREATE PROCEDURE [dbo].[DoPasswordResset]
	@Email nvarchar(70),
	@Code char(70),
	@Password NVARCHAR(50), 
	@Salt CHAR(28)
AS
	declare @Success bit = 0,
	@CutOffDate DateTime,
	@UserID int = null

	if not exists(select UserID from Users where Email = @Email)
	begin
		SELECT @Success
			return 0
	end

	set @UserID = (select top 1 UserID from Users where Email = @Email)

	if exists(select TimeIssued from UserPasswordResset where UserID = @UserID and Code = @Code)
	begin
		--check for a time out in case -_- sever is down with attack to create secure gap -_- its all ways worst case with clever hacker 
		select top 1 @CutOffDate = DATEADD(hour, 3, TimeIssued) from UserPasswordResset where UserID = @UserID and Code = @Code
		if @CutOffDate < GETDATE()
		begin -- if its old clean it out and return
			delete from UserPasswordResset where UserID = @UserID
			SELECT @Success
			return 0
		end
		update Users
		set [Password] = @Password,
		Salt = @Salt
		where UserID = @UserID
		-- if we made it through all checks then it wass a success also update last TimeLastValidated as to allow contuined work
		set @Success = 1
	end

	SELECT @Success
RETURN 0
