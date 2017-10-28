CREATE PROCEDURE [dbo].[SelectLinkerByTaskID]
	@ID int = 0
AS
	SELECT [linkerID], [NextTaskID]  from TaskLinkers 
	where TaskID = @ID
RETURN 0
