CREATE PROCEDURE [dbo].[SelectProjectByID_Light]
	@ID int = 0
AS
	SELECT P.ProjectID, P.ProjectName
	from Projects as P
	where ProjectID = @ID
RETURN 0
