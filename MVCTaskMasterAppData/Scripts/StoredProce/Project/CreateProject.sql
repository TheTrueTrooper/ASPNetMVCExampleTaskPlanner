--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: Augest 30, 2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: Allow additional projects to be made in the database
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: ASP.net
--      Writer/Publisher: Microsoft
--      Link: https://www.asp.net/
--      }
CREATE PROCEDURE [dbo].[CreateProject]
	@ProjectName NVARCHAR(70), 
    @CompanyID INT, 
	-- WorkerID that is rep to Pro
    @ManagerID INT, 
    @Address NVARCHAR(30), 
    @PostalCode NVARCHAR(10), 
    @Country NVARCHAR(10), 
    @Province NVARCHAR(10), 
    @City NVARCHAR(10), 
    @Description NVARCHAR(250), 

    @StartDate DateTime, 
    @EndDate DateTime, 

	@OutID Int output,
	@ErrorMessage char(100) output 
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 2

	-- check if company exists
	if not exists(select [CompanyID] from Companys where [CompanyID] = @CompanyID) and @CompanyID is not null
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Company does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if manager exists
	if not exists(select UserID from Users where UserID = @ManagerID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error User corralated to Manager does not exist'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if postal code is legit
	if not(@PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]' or @PostalCode like '[0-9][0-9][0-9][0-9][0-9]' or @PostalCode like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Entered postal code is not a valid postal code'
			set @MyTempError = -3
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check end date must be null or be after the date
	if not(@StartDate is null or @EndDate is null or @EndDate >= @StartDate)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Entered EndDate must be null or be after the StartDate is left'
			set @MyTempError = -4
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	insert into Projects
	([ProjectName], [CompanyID], [ManagerID], [Address], [PostalCode], [Country], [Province], [City], [Description], [StartDate], [EndDate])
	values
	(@ProjectName, @CompanyID, @ManagerID, @Address, @PostalCode, @Country, @Province, @City, @Description, @StartDate, @EndDate)
	if not(@@ERROR = 0)
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -5
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
	set @OutID = Scope_Identity()
RETURN 0
