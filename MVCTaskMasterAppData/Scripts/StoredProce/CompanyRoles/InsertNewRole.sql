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
*Insert Error code is 1
*Role Table Error code is 3
*/
CREATE PROCEDURE [dbo].[InsertNewRole]
	@CompanyID INT, 
    @RoleName NVARCHAR(30), 
    @SuperRole INT,
	--Can this individual edit the company page....
    @Admin BIT = 0,
	@ErrorMessage nvarchar(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 3,
			@ErrorOperation tinyint = 1

-- check if name meets checks
	if @RoleName is Null 
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [RoleName] is NULL and Non-Nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @RoleName not like '[a-z, A-Z]%'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [RoleName] should start with a letter'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if the company exists so we can add to it
	if not exists(select CompanyID from Companys where CompanyID = @CompanyID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [CompanyID] does not exist'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if Role with the same name exists under the same company
	if exists(select RoleID from CompanyRoles where RoleName = RoleName and CompanyID = @CompanyID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [RoleName] exist under the same company and should not be duplicated under the same company'
			set @MyTempError = -3
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

-- check if super roll exists so we can vassallize to it if it is not null
	if @SuperRole is not NULL and not exists(select RoleID from CompanyRoles where RoleID = @SuperRole)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [SuperRole] does not exist'
			set @MyTempError = -4
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	insert into CompanyRoles (CompanyID, RoleName, SuperRole, [Admin])
	values (@CompanyID, @RoleName, @SuperRole, @Admin)
	set @TempError = @@ERROR

	if @TempError = 0
		begin
			return 0
		end
	else
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -5
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end


RETURN 0
