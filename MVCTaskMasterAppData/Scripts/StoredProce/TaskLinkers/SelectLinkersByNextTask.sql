CREATE PROCEDURE [dbo].[SelectLinkersByNextTask]
	@TaskID int
AS
	SELECT LinkerID, TaskID, NextTaskID from TaskLinkers where TaskID = @TaskID
RETURN 0

