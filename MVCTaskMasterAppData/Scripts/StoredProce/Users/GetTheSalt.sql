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
CREATE PROCEDURE [dbo].[GetTheSalt]
	@Email NVARCHAR(320)
AS
	SELECT Salt from Users as U join UserEmails as E on U.[PrimaryPersonalEmailID] = E.EmailID and U.UserID = E.UserID where [Email] = @Email
return 0
