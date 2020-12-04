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
    @PrimaryPhoneNumber CHAR(14), 
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
	if exists (select EmailID from UserEmails where @Email = EmailName)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Email Used'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the phone number is formated
	if @PrimaryPhoneNumber not like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Home Phone not correctly Formated' 
			set @MyTempError = -3
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if exists(select EmailID from UserEmails where [Email] = @Email and [Validated] = 1 )
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Email is already used and validated on another account' 
			set @MyTempError = -4
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	Begin Transaction
	Declare @NewUserID INT, @NewPhoneID INT, @NewEmailID INT;
	--Add a user
	insert into Users (FirstName, MiddleInitial, LastName, [Password], Salt)
	values (@FirstName, @MiddleInitial, @LastName, @Password, @Salt)
	SELECT @NewUserID = scope_identity();
	--Add their email
	insert into UserEmails ([UserID], [Email], [EmailName])
	values (@NewUserID, @Email, 'Primary Personal Email')
	SELECT @NewEmailID = scope_identity();
	--Add their Phone Number
	insert into UserPhoneNumbers ([UserID], [PhoneNumber], [PhoneName])
	values (@NewUserID, @PrimaryPhoneNumber, 'Primary Personal Phone Number')
	SELECT @NewPhoneID = scope_identity();
	--Connect them back to the user
	update Users 
	set [PrimaryPersonalEmailID] = @NewEmailID, [PrimaryPhoneID] = @NewPhoneID
	where UserID = @NewUserID
	Commit
	if not(@@ERROR = 0)
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -6
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
	return 0
go



