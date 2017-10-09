CREATE PROCEDURE [dbo].[ValidateWithProjectViewPriv]
	@UserID int,
	@Code char(70),
	@ProjectID int
AS
	declare @SessionValid bit = 0,
	@CanView bit = 0,
	@CanPost bit = 0,
	@CanDeletePost bit = 0,
	@CanUploadFile bit = 0,
	@CanDeleteFile bit = 0,
	@CanEdit bit = 0,
	@CanDeleteOrClose bit = 0,
	@CutOffDate DateTime
	delete from [Sessions] where DATEADD(hour, 1, TimeLastValidated) < GETDATE()
	if exists(select TimeLastValidated from [Sessions] where UserID = @UserID and Code = @Code)
	begin
		--check for a time out in case -_- sever is down with attack to create secure gap -_- its all ways worst case with clever hacker 
		select top 1 @CutOffDate = DATEADD(hour, 1, TimeLastValidated) from [Sessions] where UserID = @UserID and Code = @Code
		if @CutOffDate < GETDATE()
		begin -- if its old clean all of it out and return
			SELECT @SessionValid as Valid, @CanView as CanViewProject, @CanPost as CanPostToProject, @CanDeletePost as CanDeleteProjectPost, @CanUploadFile as CanUpLoadToFile, @CanDeleteFile as CanDeleteProjectFile, @CanEdit as CanEditProject, @CanDeleteOrClose as FullProjectAdmin
			return 0
		end
		-- if we made it through all checks then it wass a success also update last TimeLastValidated as to allow contuined work
		set @SessionValid = 1
		update [Sessions]
		 set TimeLastValidated = GETDATE()  
		 --Can We view Table well lets build a temp table so we can see this table should be smaller for more quicker calls
		 declare @Table table 
		 (ManagerID int, ProjectTaskTypeManagersID int, WorkerID int,
		 TaskManagersCanEdit bit, 
		 TaskManagersCanPost bit, TaskManagersCanDeletePost bit,
		 TaskManagersCanUploadFile bit, TaskManagersCanDeleteFile bit,
		 CanViewComapnyProjects bit, CanEditCompanyProjects bit, 
		 CanPostToCompanyProjects bit, CanDeletePostCompanyProjects bit, 
		 CanUploadCompanyProjectFile bit, CanDeleteCompanyProjectFile bit, 
		 [Admin] bit, [Owner] bit)

		 insert into @Table
		 select P.ManagerID, P.ProjectTaskTypeManagersID, CW.WorkerID,
		 P.TaskManagersCanEdit, 
		 P.TaskManagersCanPost, P.TaskManagersCanDeletePost,
		 P.TaskManagersCanUploadFile, P.TaskManagersCanDeleteFile, 
		 CR.CanViewComapnyProjects, CR.CanEditCompanyProjects, 
		 CR.CanPostToCompanyProjects, CR.CanDeletePostCompanyProjects, 
		 CR.CanUploadCompanyProjectFile, CR.CanDeleteCompanyProjectFile, 
		 CR.Admin, CR.Owner
		 from Projects as P 
		 left join Companys as C on P.CompanyID = C.CompanyID
		 left join CompanyRoles as CR on CR.CompanyID = C.CompanyID
		 left join CompanyWorkers CW on CW.RoleID = CR.RoleID
		 where ProjectID = @ProjectID and
		 (P.ManagerID = @UserID 
		 or (P.ProjectTaskTypeManagersID = @UserID  
		 or (CW.WorkerID = @UserID 
			and (CR.CanViewComapnyProjects = 1
				or CR.CanEditCompanyPage = 1
				or CR.CanViewComapnyProjects = 1 
				or CR.CanEditCompanyProjects = 1
				or CR.CanPostToCompanyProjects = 1 
				or CR.CanDeletePostCompanyProjects = 1
				or CR.CanUploadCompanyProjectFile = 1
				or CR.CanDeleteCompanyProjectFile = 1
				or CR.Admin = 1
				or CR.Owner = 1))))
		 
		 if exists(select ManagerID from @Table where 
		 ManagerID = @UserID or ProjectTaskTypeManagersID = @UserID
		 or (WorkerID = @UserID and (CanViewComapnyProjects = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanView =  1;

		 if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (ProjectTaskTypeManagersID = @UserID and TaskManagersCanPost = 1)
		 or (WorkerID = @UserID and (CanPostToCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanPost =  1;

		if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (ProjectTaskTypeManagersID = @UserID and TaskManagersCanDeletePost = 1)
		 or (WorkerID = @UserID and (CanDeletePostCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanDeletePost =  1;

		if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (ProjectTaskTypeManagersID = @UserID and TaskManagersCanUploadFile = 1)
		 or (WorkerID = @UserID and (CanUploadCompanyProjectFile = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanUploadFile =  1;

		if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (ProjectTaskTypeManagersID = @UserID and TaskManagersCanDeleteFile = 1)
		 or (WorkerID = @UserID and (CanDeleteCompanyProjectFile = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanDeleteFile =  1;

		if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (ProjectTaskTypeManagersID = @UserID and TaskManagersCanEdit = 1)
		 or (WorkerID = @UserID and (CanEditCompanyProjects = 1 or [Admin] = 1 or [Owner]= 1)))
			set @CanEdit =  1;

		if exists(select ManagerID from @Table where 
		 ManagerID = @UserID 
		 or (WorkerID = @UserID and ([Admin] = 1 or [Owner]= 1)))
		  set @CanDeleteOrClose =  1;

	end

	SELECT @SessionValid as Valid, @CanView as CanViewProject, @CanPost as CanPostToProject, @CanDeletePost as CanDeleteProjectPost, @CanUploadFile as CanUpLoadToFile, @CanDeleteFile as CanDeleteProjectFile, @CanEdit as CanEditProject, @CanDeleteOrClose as FullProjectAdmin
RETURN 0
