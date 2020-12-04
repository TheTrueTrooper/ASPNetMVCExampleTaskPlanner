CREATE PROCEDURE [dbo].[SelectLinker]
	@LinkerID int
AS
	SELECT LinkerID, TaskID, NextTaskID from TaskLinkers where LinkerID = @LinkerID
RETURN 0
