CREATE PROCEDURE [dbo].[SelectTasksByProjectID]
	@ID int = 0
AS
	SELECT TaskID, SubContractorID, [Description], TaskTypeID, DurationTicks,
	ActualStartDate, ActualEndDate, CreationDate
	from Tasks 
	where ProjectID = @ID
RETURN 0



