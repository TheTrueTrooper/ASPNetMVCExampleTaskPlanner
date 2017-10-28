--CREATE PROCEDURE [dbo].[ValidateWithCompanyViewPriv]
--	@UserID int,
--	@Code char(70),
--	@CompanyID int
--AS
--	declare @SessionValid bit = 0,
--	@CanViewAsWorker bit = 0,
--	@CanPost bit = 0,
--	@CanDeletePost bit = 0,
--	@CanEdit bit = 0,
--	@CanDeleteOrClose bit = 0,
--	@CutOffDate DateTime
--	delete from [Sessions] where DATEADD(hour, 1, TimeLastValidated) < GETDATE()
--	if exists(select TimeLastValidated from [Sessions] where UserID = @UserID and Code = @Code)
--	begin
--		--check for a time out in case -_- sever is down with attack to create secure gap -_- its all ways worst case with clever hacker 
--		select top 1 @CutOffDate = DATEADD(hour, 1, TimeLastValidated) from [Sessions] where UserID = @UserID and Code = @Code
--		if @CutOffDate < GETDATE()
--		begin -- if its old clean all of it out and return
--			SELECT @SessionValid as Valid, @CanViewAsWorker as CanViewProject, @CanPost as CanPostToProject, @CanDeletePost as CanDeleteProjectPost, @CanUploadFile as CanUpLoadToFile, @CanDeleteFile as CanDeleteProjectFile, @CanEdit as CanEditProject, @CanDeleteOrClose as FullProjectAdmin
--			return 0
--		end
--		-- if we made it through all checks then it wass a success also update last TimeLastValidated as to allow contuined work
--		set @SessionValid = 1
--		update [Sessions]
--		 set TimeLastValidated = GETDATE()  
--		 --Can We view Table well lets build a temp table so we can see this table should be smaller for more quicker calls
--		 declare @Table table 
--		 (ManagerID int, ProjectTaskTypeManagersID int, WorkerID int,
--		 CanViewComapnyProjects bit, CanEditCompanyProjects bit, 
--		 CanPostToCompanyProjects bit, CanDeletePostCompanyProjects bit, 
--		 CanUploadCompanyProjectFile bit, CanDeleteCompanyProjectFile bit, 
--		 [Admin] bit, [Owner] bit)

--		 insert into @Table
--		 select CW.WorkerID,
--		 CR.CanEditCompanyEmpl, CR.CanEditCompanyOffices,
--		 CR.CanViewComapnyProjects, CR.CanEditCompanyProjects, 
--		 CR.CanPostToCompanyProjects, CR.CanDeletePostCompanyProjects, 
--		 CR.Admin, CR.Owner
--		 from Companys as C
--		 left join CompanyRoles as CR on CR.CompanyID = C.CompanyID
--		 left join CompanyWorkers CW on CW.RoleID = CR.RoleID
--		 where C.CompanyID = @CompanyID
--			and CW.UserID = @UserID
		 
--		 if exists(select WorkerID from @Table where 
--			WorkerID = @UserID)
--			set @CanViewAsWorker = 1;

--		 if exists(select WorkerID from @Table where 
--			WorkerID = @UserID and (CanPostToCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1))
--			set @CanPost = 1;

--		if exists(select WorkerID from @Table where 
--			WorkerID = @UserID and (CanDeletePostCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1))
--			set @CanDeletePost = 1;

--		if exists(select WorkerID from @Table where 
--		 WorkerID = @UserID and (CanEditCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1))
--			set @CanEdit = 1;

--		if exists(select WorkerID from @Table where 
--		 WorkerID = @UserID and ([Admin] = 1 or [Owner]= 1))
--			set @CanDeleteOrClose = 1;

--	end

--	SELECT @SessionValid as Valid, @CanViewAsWorker as CanViewProject, @CanPost as CanPostToProject, @CanDeletePost as CanDeleteProjectPost, @CanEdit as CanEditProject, @CanDeleteOrClose as FullProjectAdmin
--RETURN 0