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
*Insert Error code is 1
*Company Table Error code is 2
*/
CREATE PROCEDURE [dbo].[InsertNewCompany]
	@CompanyName NVARCHAR(30), 
    @Description NVARCHAR(250), 
    @CompanySite NCHAR(40),
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 2,
			@ErrorOperation tinyint = 1

--Check Required inputs
	if @CompanyName is Null 
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error CompanyName is NULL and Non-Nullable'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if not @CompanyName like '[a-z, A-Z]'
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error CompanyName should start with a letter'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end


	insert into Companys(CompanyName, [Description],CompanySite)
	values (@CompanyName, @Description, @CompanySite)
	set @TempError = @@ERROR
	if @TempError != 0
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



