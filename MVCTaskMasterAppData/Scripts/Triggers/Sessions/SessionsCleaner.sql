--	  Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
--    Date Writen: Sep 14, 2017
--    Project Goal: Make a cloud based app to aid in project management
--    File Goal: To Clean out over due signins and keepa a cleaner table
--    Link: https://github.com/TheTrueTrooper/AngelASPExtentions
--    Sources/References:
--      {
--      Name: NA
--      Writer/Publisher: NA
--      Link: NA
--      }
CREATE TRIGGER [SessionsCleaner]
	ON [dbo].[Sessions]
	FOR insert, delete, update
	AS
	BEGIN
	  delete from [Sessions] where DATEADD(hour, 1, TimeLastValidated) < GETDATE()
	END