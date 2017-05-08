CREATE PROCEDURE [dbo].[InsertErrorInfo]
    @ErrorMessage nvarchar(100), 
    @Type TINYINT, 
    @Table TINYINT, 
    @SQLErrorCode INT, 
    @MyErrorCode int
AS
-- so in theory we will have an unknown default id the thing cant be logged under an entry
	if not exists(select TableID from MYTableList where TableID = @Table)
		set @Table = 0;
	if not exists(select OperationTypeID from OperationTypes where OperationTypeID = @Type)
		set @Type = 0; 
	insert into ErrorTable (ErrorMessage, [Type], [Table], SQLErrorCode, MyErrorCode)
	values (@ErrorMessage, @Type, @Table, @SQLErrorCode, @MyErrorCode)
RETURN 0
go
