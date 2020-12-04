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
/*
*Basic insert user for loggin and tracking
*Will have a Salted and Hashed password by this point
*Delete Error code is 3
*User Table Error code is 1
*/
CREATE PROCEDURE [dbo].[UpdateTheUserChangePassword]
	@UserID int,
    @Password NVARCHAR(50), 
	@NewPassword NVARCHAR(50),
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 3

-- check if the email exists
	if exists(select Email from Users as U join UserEmails as E on U.[PrimaryPersonalEmailID] = E.EmailID and U.UserID = E.UserID where U.UserID = @UserID and [Password] = @Password)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email doesn''t exist or the wrong email Password combo'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	update Users 
	set 
	[Password] = @NewPassword
	where @UserID = UserID
	set @TempError = @@ERROR
	if @TempError = 0
		begin
			return 0
		end
	else
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
go
