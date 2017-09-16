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
--CREATE TRIGGER [UsersCascadingDelete]
--	ON [dbo].[Users]
--	FOR DELETE
--	AS
--	BEGIN
--		Declare @UserID int = 0

--		DECLARE MY_CURSOR CURSOR 
--		LOCAL STATIC READ_ONLY FORWARD_ONLY
--		FOR 
--		SELECT DISTINCT UserID
--		FROM deleted

--		OPEN MY_CURSOR
--		FETCH NEXT FROM MY_CURSOR INTO @UserID

--		WHILE @@FETCH_STATUS = 0
--		BEGIN 
--			delete from CompanyWorkers where UserID = @UserID
--			FETCH NEXT FROM MY_CURSOR INTO @UserID
--		END

--		CLOSE MY_CURSOR
--		DEALLOCATE MY_CURSOR
--	END
