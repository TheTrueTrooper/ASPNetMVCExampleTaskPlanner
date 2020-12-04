--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: June 23,2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Test/Verify SQL Data base changes
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
--select * from ErrorTable

--select * from Users

--select * from Companys

Declare @error varchar(100)
Declare @Email nvarchar(50)= 'BitSan@outlook.com'

execute InsertNewUser 'Me', NULL, 'dsf', @Email, 'Bob456,.,', 'sdaf', '17802572525', @error

--update Users
--set Password = 'RM2RwZmgDP62NebThSKYKCTEFR2pw7rP7Sv5ebfGXA4='
select * from Users
select * from UserEmails
select * from UserPhoneNumbers
select * from ErrorTable

delete from Users where 1=1
delete from UserEmails where 1=1
delete from UserPhoneNumbers where 1=1

--Declare @Email nvarchar(50)= 'BitSan@outlook.com',
--		@Password nvarchar(50) = 'Azto3D5FIT3PbYv2D2dJCTV5J9nF3B63XLYhxpq49og=',
--		@Cool bit = 0,
--		@ID int = 0

--execute PasswordCheck @Email, @Password, @ID output, @Cool output

--select * from [Sessions]

--declare 
--@userID int = 9,
--@CellPhone char(14) = 17807774070,
--@Error varchar(100) = ''
--select * from Users
--execute UpdateTheUserInfo @userID, null, null, null, null, @CellPhone, @CellPhone, @Error output
--select * from Users
--select * from ErrorTable
--select * from Users
--go
--declare 
--@userID int = 9,
--@CellPhone char(14) = 17807774070,
--@Error varchar(100) = ''
--execute UpdateTheUserInfo @userID, null, null, null, null, null, null, @Error output
--select * from Users
--select * from ErrorTable

--set IDENTITY_INSERT Companys on
--insert into Companys (CompanyID, CompanyName, CompanySite, [Description])
--values (2, 'SCFConcreteFinishing2', 'www.Com', 'We Do Concrete')
--set IDENTITY_INSERT Companys off

--set IDENTITY_INSERT CompanyRoles on
--insert into CompanyRoles(RoleID, CompanyID, RoleName, SuperRole, [Admin])
--values (2, 2, 'OnlyDudeLeft', null, 0)
--set IDENTITY_INSERT CompanyRoles off

--set IDENTITY_INSERT Offices on
--insert into Offices (OfficeID, CompanyID, [Country], [Province], [City], [Address], [PostalCode], [Phone], [Name])
--values (2, 2, 'Canada', 'Alberta', 'Edmonton', '1840 28 ave SW', 'T6X1A5', '17802572525', 'Main')
--set IDENTITY_INSERT Offices off

--set IDENTITY_INSERT CompanyWorkers on
--insert into CompanyWorkers (WorkerID, CompanyID, OfficeID, RoleID, UserID)
--values (2, 2, 2, 2, 9)
--set IDENTITY_INSERT CompanyWorkers off
---- check if user belongs to company <-----------------------------------------------------------------
--select C.CompanyID from Companys as C
--		join CompanyRoles as CR on C.CompanyID = CR.CompanyID
--		join CompanyWorkers as CW on CW.CompanyID = CR.CompanyID
--		join Users as U on CW.UserID = U.UserID
---- check if user belongs to company with admin privilages <-------------------------------------------
--select C.CompanyID from Companys as C
--		join CompanyRoles as CR on C.CompanyID = CR.CompanyID
--		join CompanyWorkers as CW on CW.CompanyID = CR.CompanyID
--		join Users as U on CW.UserID = U.UserID and CR.[Admin] = 1

--delete from CompanyWorkers
--where 1 = 1

--delete from CompanyRoles
--where 1 = 1

--delete from Offices
--where 1 = 1

--delete from Companys
--where 1 = 1



----My Notes for self on future production
---- Data in temp tables will presist after roll backs so log with this device after introduction of transations
----Declare @tempvar TABLE
----(
----[id] int Identity(1,1),
----[desc] varchar(100)
----) Azto3D5FIT3PbYv2D2dJCTV5J9nF3B63XLYhxpq49og= 44      
--delete from [Sessions]
--where 1 = 1


--SELECT P.ProjectID, P.ProjectName, C.CompanyName, O.CompanyID, O.OfficeName, P.[Address], P.PostalCode, P.Country, P.Province, P.[Description], P.StartDate, P.EndDate from Projects as P
--	left join Companys as C on P.CompanyID = C.CompanyID
--	left join Offices as O on O.CompanyID = C.CompanyID
--	left join CompanyRoles as CR on CR.CompanyID = C.CompanyID
--	left join CompanyWorkers as CW on CW.OfficeID = O.OfficeID and CW.CompanyID = C.CompanyID and CW.RoleID = CR.RoleID
--	left join Users as U on P.ManagerID = U.UserID and CW.UserID = U.UserID
--	where P.ManagerID = 2013 

--delete from [Sessions] where 1=1
--delete from UserPasswordResset where 1=1
--delete from users where 1=1

	
--select * from Users
--select * from UserPasswordResset

--UPDATE Users
--set MiddleInitial = 'M',
-- CellPhone = '7802572525',
-- WorkPhone = '7802572525',
-- Bio = 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',
-- PortfollURL = 'https://www.linkedin.com/in/angelo-sanches-a4b94a129'
--where UserID = 2013

--delete from Projects
--where 1 = 1

--select FirstName, MiddleInitial, LastName, Bio, Picture, HomePhone, CellPhone, WorkPhone, Email, PortfollURL, UserID from Users where 2013 = UserID

--insert into Projects(
--	[ProjectName], 
--	-- WorkerID that is rep to Pro
--    [ManagerID], 
--    [Address], 
--    [PostalCode], 
--    [Country], 
--    [Province], 
--    [City], 
--    [Description], 
--    [StartDate],
--	[EndDate])
--	values ('Test 5', 2013, '
--	8140', 'T6X1A5', 'Canada', 'Alberta', 'Edmonton', 
--	'This is a test cvzxv xzvcxzvc  xzvxzcv xcv cxv cxz vxcv xczv xvxc vcx vxzc 312432 3243214 321421  xz dsfdv', 
--	GETDATE(), DATEADD(hour, 1, GETDATE()))

--	select * from Projects

--	execute SelectTasksByProjectID 1

--	drop proc SelectTasksByProjectID

--	go
--	CREATE PROCEDURE [dbo].[SelectTasksByProjectID]
--	@ID int = 0
--AS
--	SELECT TaskID, SubContractorID, [Description], TaskTypeID, DurationTicks,
--	ActualStartDate, ActualEndDate, CreationDate
--	from Tasks 
--	where ProjectID = @ID
--RETURN 0
--go
-- select * from Tasks
--	execute dbo.SelectTasksByProjectID 0

--	 select * from ErrorTable

--	 select UserID from Users where UserID = 1



--DELETE Users 
--FROM Users as U
--join UserPhoneNumbers as UPN ON U.UserID = UPN.UserID And U.PrimaryPhoneID = UPN.PhoneNumberID 
--join UserEmails as UE ON U.UserID = UE.UserID And U.PrimaryPersonalEmailID = UE.EmailID 
--join UserPasswordResset as UPR ON U.UserID = UPR.UserID