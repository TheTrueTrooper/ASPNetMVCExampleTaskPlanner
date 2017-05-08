CREATE PROCEDURE [dbo].[DeleteTheSession]
	@SessionID int
AS
	delete from [Sessions] where @SessionID = UserID
RETURN 0
