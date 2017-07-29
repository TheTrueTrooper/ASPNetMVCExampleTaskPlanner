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
*Delete Error code is 2
*User Table Error code is 1
*/
CREATE PROCEDURE [dbo].[DeleteTheUser]
	@FirstName NVARCHAR(20), 
    @MiddleInitial NCHAR(1), 
    @LastName NVARCHAR(20), 
    @Email NVARCHAR(320), 
    @Password NVARCHAR(50), 
    @HomePhone CHAR(14), 
    @CellPhone CHAR(14), 
    @WorkPhone CHAR(14),
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 2

-- check if the email exists and we can delete it
	if exists(select Email from Users where Email = @Email and @Password = [Password])
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Password] was wrong or [Email] does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	delete from Users 
	where Email = @Email
	set @TempError = @@ERROR
	if not exists (select FirstName from Users where @Email = Email)
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
