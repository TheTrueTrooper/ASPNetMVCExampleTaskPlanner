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
*Update Error code is 3
*User Table Error code is 1
*/
CREATE PROCEDURE [dbo].[UpdateTheUserInfo]
	@UserID int,
	@FirstName NVARCHAR(20), 
    @MiddleInitial NCHAR(1), 
    @LastName NVARCHAR(20),
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 3

--Check Required inputs
	if @LastName is Null
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email, LastName, or Password is NULL and is non-nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the email exists
	update Users 
	set 
	FirstName = @FirstName,
    MiddleInitial = @MiddleInitial, 
    LastName = @LastName
	where UserID = @UserID
	set @TempError = @@ERROR
	if @TempError = 0
		begin
			return 0
		end
	else
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -6
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
go

