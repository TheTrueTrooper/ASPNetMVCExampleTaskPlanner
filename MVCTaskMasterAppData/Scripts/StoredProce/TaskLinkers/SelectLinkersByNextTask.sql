CREATE PROCEDURE [dbo].[SelectLinkersByNextTask]
	@TaskID int
AS
	SELECT LinkerID, TaskID, NextTaskID from TaskLinkers where NextTaskID = @TaskID
RETURN 0

