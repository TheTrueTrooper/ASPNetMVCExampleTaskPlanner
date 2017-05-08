CREATE PROCEDURE [dbo].[IsEmailUsed]
	@Email NVARCHAR(320)
AS
	declare @IsUsed bit = 0
	if exists(select UserID from Users where Email = @Email)
		set @IsUsed = 1
	SELECT @IsUsed
RETURN 0
