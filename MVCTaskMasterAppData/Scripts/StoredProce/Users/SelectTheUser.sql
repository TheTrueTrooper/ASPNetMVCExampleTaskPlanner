/*
*Basic insert user for loggin and tracking
*Will have a Salted and Hashed password by this point
*Select Error code is 4
*User Table Error code is 1
*/
CREATE PROCEDURE [dbo].[SelectTheUser]
	@ID int,
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 4,
			@ErrorOperation tinyint = 1


	select FirstName, MiddleInitial, LastName, HomePhone, CellPhone, WorkPhone, Email from Users where UserID = @ID
	set @TempError = @@ERROR
	if @TempError != 0
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
go
