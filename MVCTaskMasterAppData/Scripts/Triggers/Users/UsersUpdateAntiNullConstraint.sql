--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: Sep 14, 2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Create a table to aid in the verif of accounts that is a normalized drop away 
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TRIGGER [UsersUpdateAntiNull]
	ON [dbo].[Users]
	for Update
	AS
	BEGIN
		if exists(select UserID from inserted where [PrimaryPersonalEmailID] = null or [PrimaryPhoneID] = null)
		begin
			raiserror('Update has faild due to Trigger Constraint Violation at UsersUpdateAntiNull due to setting either [PrimaryPersonalEmailID] or [PrimaryPhoneID] null Rolling back now', 15, 0)
			rollback
		end
	END
