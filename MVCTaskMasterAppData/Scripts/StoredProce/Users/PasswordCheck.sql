CREATE PROCEDURE [dbo].[PasswordCheck]
	@Email NVARCHAR(320), 
    @Password NVARCHAR(50), 
	@IDOut int output,
	@ChecksOut bit output
AS
	set @ChecksOut = 0
	if exists(select UserID from Users where Email = @Email and [Password] = @Password)
	begin
		set @ChecksOut = 1
		set @IDOut = (select UserID from Users where Email = @Email and [Password] = @Password)
	end
RETURN 0
