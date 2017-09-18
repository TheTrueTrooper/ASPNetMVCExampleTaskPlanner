--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: Augest 30, 2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: Allow the view of all of the top data of a project
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: ASP.net
--      Writer/Publisher: Microsoft
--      Link: https://www.asp.net/
--      }
CREATE PROCEDURE [dbo].[SelectProjectByID]
	@ID int = 0
AS
	SELECT P.ProjectID, P.ProjectName, P.Address as ProjectAddress, P.City as ProjectCity, P.Province as ProjectProvince, P.Country as ProjectCountry, 
	P.PostalCode as ProjectPostalCode, P.StartDate as ProjectStartDate, P.EndDate as ProjectEndDate, 
	P.ActualStartDate as ProjectActualStartDate, P.ActualEndDate as ProjectActualEndDate, P.Description as ProjectDescription, 
	P.CreationDate as ProjectCreationDate,
	U.UserID as ManagerID, U.Picture as ManagerPicture, U.FirstName as ManagerFirstName, U.MiddleInitial as ManagerMiddleInitial, 
	U.LastName as ManagerLastName, U.HomePhone as ManagerHomePhone, U.WorkPhone as ManagerWorkPhone, U.CellPhone as ManagerCellPhone,
	C.CompanyID, C.CompanyName, C.CompanySite
	from Projects as P left join Users as U on P.ManagerID = U.UserID left join Companys as C on P.CompanyID = C.CompanyID 
	where ProjectID = @ID
RETURN 0
