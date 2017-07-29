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
*insert Error code is 1
*User Table Error code is 4
*/
CREATE PROCEDURE [dbo].[InsertNewOffice]
	@CompanyID INT, 
	@Country VARCHAR(20),
	@Province NCHAR(20),
    @City NCHAR(20), 
    @Address NCHAR(30), 
    @PostalCode CHAR(6), 
    @Phone CHAR(14), 
	@Fax CHAR(14),
    @Name NCHAR(20), 
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 4,
			@ErrorOperation tinyint = 1

	if @CompanyID is Null 
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [RoleName] is NULL and Non-Nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if not exists(select CompanyID from Companys where CompanyID = @CompanyID )
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [RoleName] is NULL and Non-Nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @Country is Null or len(@Country) > 0
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Country] is NULL and Non-Nullable or It Is empty'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @Province is Null or len(@Province) > 0
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Province] is NULL and Non-Nullable or It Is empty'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @City is Null or len(@City) > 0
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [City] is NULL and Non-Nullable or It Is empty'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @Address is Null or len(@Address) > 0
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Address] is NULL and Non-Nullable or It Is empty'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @PostalCode is Null or len(@PostalCode) > 0 or @PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]' or @PostalCode like '[0-9][0-9][0-9][0-9][0-9]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [PostalCode] is NULL and Non-Nullable or It or is empty or it doesnt meet the format "[A-Z][0-9][A-Z][0-9][A-Z][0-9]","[0-9][0-9][0-9][0-9][0-9]"'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @Name is Null or len(@Name) > 0
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Name] is NULL and Non-Nullable or It Is empty'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

RETURN 0
