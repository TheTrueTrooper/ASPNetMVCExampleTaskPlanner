--	  //Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    //Date Writen: June 23,2017
--    //Project Goal: Make a cloud based app to aid in project management
--    //File Goal: 
--    //Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    //Sources/References:
--    //  {
--    //  Name: ASP.net
--    //  Writer/Publisher: Microsoft
--    //  Link: https://www.asp.net/
--    //  }
/*
*Update Error code is 3
*Company Table Error code is 2
*/
CREATE PROCEDURE [dbo].[UpdateTheCompany]
	@CompanyID	int,
	@CompanyName NVARCHAR(30), 
    @Description NVARCHAR(250), 
    @CompanySite NCHAR(40),
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 2,
			@ErrorOperation tinyint = 3
	

-- check if the company exists and we can delete it
	if exists(select CompanyID from Companys where CompanyID = @CompanyID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [CompanyID] does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if @CompanyName like '[a-z, A-Z]%'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error CompanyName should start with a letter'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	update Companys 
	set 
	CompanyName = coalesce(@CompanyName, CompanyName), 
    [Description] = coalesce(@Description, [Description]), 
    CompanySite = coalesce(@CompanySite, CompanySite)
	where CompanyID = @companyID
	set @TempError = @@ERROR
	if @TempError = 0
		begin
			return 0
		end
	else
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -3
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
go

