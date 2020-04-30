CREATE PROCEDURE [dbo].[CreateTaskLink]
	@TaskID int,
	@NextTaskID int,
	@OutID int output,
	@ErrorMessage char(100) output 
AS
	Declare @TempError int = 0,
			@MyTempError int = 0,
			@ErrorTable tinyint = 1,
			@ErrorOperation tinyint = 2

	if not exists(select TaskID from Tasks where TaskID = @TaskID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Task does not exist'
			set @MyTempError = -1
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	if not exists(select TaskID from Tasks where TaskID = @NextTaskID)
		begin
			set @TempError = @@ERROR
			set @ErrorMessage = 'Error Next Task does not exist'
			set @MyTempError = -2
			execute InsertErrorInfo  @ErrorMessage, @ErrorOperation, @ErrorTable, @TempError, @MyTempError
			return @MyTempError
		end

	insert into TaskLinkers
		(TaskID, NextTaskID)
	Values
		(@TaskID, @NextTaskID)

RETURN 0
