CREATE PROCEDURE [dbo].[CreateTask]
	@SubContractorID int,
	@TaskTypeID int,
	@ProjectID int,
	@Description NVARCHAR(250),
	@DurationTicks bigint,
	@OutID int output,
	@ErrorMessage char(100) output 
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 2

	-- check if Project exists
	if not exists(select ProjectID from Projects where ProjectID = @ProjectID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Project does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if Project exists
	if not exists(select UserID from Users where UserID = @SubContractorID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Project does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if Project exists
	if not exists(select ProjectID from Projects where ProjectID = @ProjectID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Project does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if Description is null
	if @Description is null
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Description is null and cant be'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	-- check if Durration is null
	if @DurationTicks is null
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error DurationTicks is null and cant be'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	insert into Tasks 
	(SubContractorID, TaskTypeID, ProjectID, [Description], DurationTicks) 
	values
	(@SubContractorID, @TaskTypeID, @ProjectID, @Description, @DurationTicks) 
	if not(@@ERROR = 0)
		begin
			set @ErrorMessage = 'Error UnkownSQLError'
			set @MyTempError = -5
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end
	set @OutID = Scope_Identity()
RETURN 0


--[TaskID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
--	[SubContractorID] INT NULL,
--	[TaskTypeID] INT NULL,
--	[ProjectID] INT NOT NULL,
--    [Description] NVARCHAR(250) NOT NULL, 

--	[DurationTicks] bigint not null default 0,

--	[ActualStartDate] DateTime NULL, 
--    [ActualEndDate] DateTime NULL,

--	--time that the project was opened utc
--    [CreationDate] DateTime NOT NULL DEFAULT GETDATE(), 