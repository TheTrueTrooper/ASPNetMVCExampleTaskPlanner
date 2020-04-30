CREATE PROCEDURE [dbo].[SelectLinkerByTaskID]
	@ID int = 0
AS
	SELECT LinkerID, TaskID, NextTaskID from TaskLinkers 
	where TaskID = @ID
RETURN 0
