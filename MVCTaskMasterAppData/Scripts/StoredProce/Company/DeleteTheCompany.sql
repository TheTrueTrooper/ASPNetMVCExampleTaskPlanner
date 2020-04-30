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
*Delete Error code is 2
*Company Table Error code is 2
*/
CREATE PROCEDURE [dbo].[DeleteTheCompany]
	@CompanyID int,
	@ErrorMessage char(100) output
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 2,
			@ErrorOperation tinyint = 2

-- check if the company exists and we can delete it
	if not exists(select CompanyID from Companys where CompanyID = @CompanyID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error [Company] does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	delete from Companys 
	where CompanyID = @CompanyID
	set @TempError = @@ERROR
	if not exists (select CompanyID from Companys where CompanyID = @CompanyID)
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
