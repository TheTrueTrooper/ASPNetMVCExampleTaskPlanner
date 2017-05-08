CREATE PROCEDURE [dbo].[GetTheSalt]
	@Email NVARCHAR(320)
AS
	SELECT Salt from Users where Email = @Email
return 0
