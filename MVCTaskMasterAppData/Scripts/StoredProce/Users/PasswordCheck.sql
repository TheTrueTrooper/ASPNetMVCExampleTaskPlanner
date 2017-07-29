--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: 
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: ASP.net
--      Writer/Publisher: Microsoft
--      Link: https://www.asp.net/
--      }
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
