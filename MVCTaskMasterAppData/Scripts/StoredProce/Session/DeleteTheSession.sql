--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Delete or close a session
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: ASP.net
--      Writer/Publisher: Microsoft
--      Link: https://www.asp.net/
--      }
CREATE PROCEDURE [dbo].[DeleteTheSession]
	@SessionID int
AS
	delete from [Sessions] where @SessionID = UserID
RETURN 0
