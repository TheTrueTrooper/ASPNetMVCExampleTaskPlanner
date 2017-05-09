select * from Users

select * from Companys

Declare @error varchar(100)
Declare @Email nvarchar(50)= 'BitSan@outlook.com'

execute InsertNewUser 'Me', NULL, 'dsf', @Email, 'Bob456,.,', 'sdaf', '17802572525', NULL, NULL, @error

update Users
set Password = 'RM2RwZmgDP62NebThSKYKCTEFR2pw7rP7Sv5ebfGXA4='
select * from Users
select * from ErrorTable

delete from Users where 1=1

Declare @Email nvarchar(50)= 'BitSan@outlook.com',
		@Password nvarchar(50) = 'Azto3D5FIT3PbYv2D2dJCTV5J9nF3B63XLYhxpq49og=',
		@Cool bit = 0,
		@ID int = 0

execute PasswordCheck @Email, @Password, @ID output, @Cool output

select * from [Sessions]

declare 
@userID int = 9,
@CellPhone char(14) = 17807774070,
@Error varchar(100) = ''
select * from Users
execute UpdateTheUserInfo @userID, null, null, null, null, @CellPhone, @CellPhone, @Error output
select * from Users
select * from ErrorTable
select * from Users
go
declare 
@userID int = 9,
@CellPhone char(14) = 17807774070,
@Error varchar(100) = ''
execute UpdateTheUserInfo @userID, null, null, null, null, null, null, @Error output
select * from Users
select * from ErrorTable

set IDENTITY_INSERT Companys on
insert into Companys (CompanyID, CompanyName, CompanySite, [Description])
values (2, 'SCFConcreteFinishing2', 'www.Com', 'We Do Concrete')
set IDENTITY_INSERT Companys off

set IDENTITY_INSERT CompanyRoles on
insert into CompanyRoles(RoleID, CompanyID, RoleName, SuperRole, [Admin])
values (2, 2, 'OnlyDudeLeft', null, 0)
set IDENTITY_INSERT CompanyRoles off

set IDENTITY_INSERT Offices on
insert into Offices (OfficeID, CompanyID, [Country], [Province], [City], [Address], [PostalCode], [Phone], [Name])
values (2, 2, 'Canada', 'Alberta', 'Edmonton', '1840 28 ave SW', 'T6X1A5', '17802572525', 'Main')
set IDENTITY_INSERT Offices off

set IDENTITY_INSERT CompanyWorkers on
insert into CompanyWorkers (WorkerID, CompanyID, OfficeID, RoleID, UserID)
values (2, 2, 2, 2, 9)
set IDENTITY_INSERT CompanyWorkers off
-- check if user belongs to company <-----------------------------------------------------------------
select C.CompanyID from Companys as C
		join CompanyRoles as CR on C.CompanyID = CR.CompanyID
		join CompanyWorkers as CW on CW.CompanyID = CR.CompanyID
		join Users as U on CW.UserID = U.UserID
-- check if user belongs to company with admin privilages <-------------------------------------------
select C.CompanyID from Companys as C
		join CompanyRoles as CR on C.CompanyID = CR.CompanyID
		join CompanyWorkers as CW on CW.CompanyID = CR.CompanyID
		join Users as U on CW.UserID = U.UserID and CR.[Admin] = 1

delete from CompanyWorkers
where 1 = 1

delete from CompanyRoles
where 1 = 1

delete from Offices
where 1 = 1

delete from Companys
where 1 = 1



--My Notes for self on future production
-- Data in temp tables will presist after roll backs so log with this device after introduction of transations
--Declare @tempvar TABLE
--(
--[id] int Identity(1,1),
--[desc] varchar(100)
--) Azto3D5FIT3PbYv2D2dJCTV5J9nF3B63XLYhxpq49og= 44      
delete from [Sessions]
where 1 = 1


SELECT P.ProjectID, P.ProjectName, C.CompanyName, O.CompanyID, O.OfficeName, P.[Address], P.PostalCode, P.Country, P.Province, P.[Description], P.StartDate, P.EndDate from Projects as P
	join Companys as C on P.CompanyID = C.CompanyID
	join Offices as O on O.CompanyID = C.CompanyID
	join CompanyRoles as CR on CR.CompanyID = C.CompanyID
	join CompanyWorkers as CW on CW.OfficeID = O.OfficeID and CW.CompanyID = C.CompanyID and CW.RoleID = CR.RoleID
	join Users as U on P.ManagerID = U.UserID and CW.UserID = U.UserID
	where P.ManagerID = 1



