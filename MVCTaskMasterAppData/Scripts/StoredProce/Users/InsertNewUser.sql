﻿/*
*Basic insert user for loggin and tracking
*Will have a Salted and Hashed password by this point
*Insert Error code is 1
*User Table Error code is 1
*/
CREATE PROCEDURE [dbo].[InsertNewUser]
	@FirstName NVARCHAR(20), 
    @MiddleInitial NCHAR(1), 
    @LastName NVARCHAR(20), 
    @Email NVARCHAR(320), 
    @Password NVARCHAR(50), 
	@Salt CHAR(28),
    @HomePhone CHAR(14), 
    @CellPhone CHAR(14), 
    @WorkPhone CHAR(14),
	@ErrorMessage nvarchar(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 1

--Check Required inputs
	if @LastName is Null or @Email is Null or @Password is Null
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email, LastName, or Password is NULL and is non-nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end


-- check if the email is formated
	if @Email not like '[^.]%[^.][@]%[.]%'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email not correctly formated'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the email has been used
	if exists (select FirstName from Users where @Email = Email)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email Used'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the email is formated
	if @HomePhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Home Phone not correctly Formated' 
			set @MyTempError = -3
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the email is formated
	if @CellPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Cell Phone not correctly Formated' 
			set @MyTempError = -4
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the phone is formated
	if @WorkPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Work Phone not correctly Formated' 
			set @MyTempError = -5
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	insert into Users (FirstName, MiddleInitial, LastName, Email, [Password], Salt, HomePhone, CellPhone, WorkPhone)
	values (@FirstName, @MiddleInitial, @LastName, @Email, @Password, @Salt, @HomePhone, @CellPhone, @WorkPhone)
	set @TempError = @@ERROR
	if exists (select FirstName from Users where @Email = Email)
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


