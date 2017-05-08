CREATE ROLE [BasicViewServerRole]
Go

Deny Select, Delete, insert, update To [BasicViewServerRole]
GO 

Grant execute To [BasicViewServerRole]
go

ALTER ROLE [BasicViewServerRole]  Add MEMBER [BasicViewUser]
Go