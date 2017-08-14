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
CREATE PROCEDURE [dbo].[SelectUserProjects]
	@UserID int,
	@ErrorMessage char(100) output
AS
	SELECT P.ProjectID, P.ProjectName, C.CompanyName, O.CompanyID, O.OfficeName, P.[Address], P.PostalCode, P.Country, P.Province, P.City, P.[Description], P.StartDate, P.EndDate from Projects as P
	join Companys as C on P.CompanyID = C.CompanyID
	join Offices as O on O.CompanyID = C.CompanyID
	join CompanyRoles as CR on CR.CompanyID = C.CompanyID
	join CompanyWorkers as CW on CW.OfficeID = O.OfficeID and CW.CompanyID = C.CompanyID and CW.RoleID = CR.RoleID
	join Users as U on P.ManagerID = U.UserID and CW.UserID = U.UserID
	where P.ManagerID = @UserID
RETURN 0
