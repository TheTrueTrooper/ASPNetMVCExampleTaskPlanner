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
CREATE PROCEDURE [dbo].[IsEmailUsed]
	@Email NVARCHAR(320)
AS
	declare @IsUsed bit = 0
	if exists(select EmailID from UserEmails where [Email] = @Email and [Validated] = 1 )
		set @IsUsed = 1
	SELECT @IsUsed
RETURN 0
