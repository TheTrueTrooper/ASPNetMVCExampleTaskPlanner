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
CREATE PROCEDURE [dbo].[CreateThePasswordResset]
	@Email nvarchar(70),
	@Code char(28)
AS
	declare @UserID int = null,
	@Exists bit = 0

	if not exists(select E.EmailID from Users as U join UserEmails as E on U.[PrimaryPersonalEmailID] = E.EmailID and U.UserID = E.UserID where [Email] = @Email)
	begin 
		select @Exists
		return -1;
	end

	set @UserID = (select top 1 U.UserID from Users as U join UserEmails as E on U.[PrimaryPersonalEmailID] = E.EmailID and U.UserID = E.UserID where [Email] = @Email)
	set @Exists = 1

	if exists(select UserID from [UserPasswordResset] where UserID = UserID)
	begin
		update [UserPasswordResset]
		set Code = @Code,
		TimeIssued = GETDATE()
		where UserID = @UserID
	end
	else 
	begin 
	insert into [UserPasswordResset] (UserID, Code)
	values (@UserID, @Code)
	end

	select @Exists
RETURN 0
