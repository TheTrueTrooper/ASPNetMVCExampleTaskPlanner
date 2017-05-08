
-- drop all errors at first to allow for drops--------------------
delete from ErrorTable
where 1=1 
go

--------------------------------------------------------<Table Table work begins
--create a Pseudo Enum to use afte droping the poten of it
--if exists(select 	[name] from sysobjects where [name] = 'PseudoConstants_ErrorTables')
--	drop view [dbo].[PseudoConstants_ErrorTables]
--go
--CREATE VIEW [dbo].[PseudoConstants_ErrorTables]
--as
--SELECT
--	CAST(0 AS tinyint) AS [Uknown],
--	CAST(1 AS tinyint) AS [Users],
--	CAST(2 AS tinyint) AS [Companys],
--	CAST(3 AS tinyint) AS [Roles]
--go


Declare @Table nvarchar(20)

delete from MYTableList
where 1=1 

set IDENTITY_INSERT MYTableList on
set @Table = 'Unknown'
if not exists(select TableName from MYTableList where @Table = TableName) 
insert into MYTableList (TableID, TableName) values (0, @Table) 
set IDENTITY_INSERT MYTableList off

set @Table = 'Users'
if not exists(select TableName from MYTableList where @Table = TableName) 
insert into MYTableList (TableName) values (@Table) 

set @Table = 'Companys'
if not exists(select TableName from MYTableList where @Table = TableName) 
insert into MYTableList (TableName) values (@Table) 

set @Table = 'Roles'
if not exists(select TableName from MYTableList where @Table = TableName) 
insert into MYTableList (TableName) values (@Table) 

select * from MYTableList order by TableID
go

--------------------------------------------------------<Operation Table work begins
--create a Pseudo Enum to use afte droping the poten of it
--if exists(select 	[name] from sysobjects where [name] = 'PseudoConstants_OperationTypes')
--	drop view [dbo].[PseudoConstants_OperationTypes]
--go
--CREATE VIEW [dbo].[PseudoConstants_OperationTypes]
--as
--SELECT
--	CAST(0 AS tinyint) AS [Uknown],
--	CAST(1 AS tinyint) AS [Insert],
--	CAST(2 AS tinyint) AS [Delete],
--	CAST(3 AS tinyint) AS [Update],
--	CAST(4 AS tinyint) AS [Select],
--	CAST(5 AS tinyint) AS [Other]
--go


Declare @Type char(6)

delete from OperationTypes
where 1=1 

set IDENTITY_INSERT OperationTypes on
set @Type = 'Unknown'
if not exists(select [OperationType] from OperationTypes where @Type = [OperationType]) 
insert into OperationTypes (OperationTypeID, [OperationType]) values (0, @Type) 
set IDENTITY_INSERT OperationTypes off

set @Type = 'Insert'
if not exists(select [OperationType] from OperationTypes where @Type = [OperationType]) 
insert into OperationTypes ([OperationType]) values (@Type) 

set @Type = 'Delete'
if not exists(select [OperationType] from OperationTypes where @Type = [OperationType]) 
insert into OperationTypes ([OperationType]) values (@Type) 

set @Type = 'Update'
if not exists(select [OperationType] from OperationTypes where @Type = [OperationType]) 
insert into OperationTypes ([OperationType]) values (@Type) 

set @Type = 'Select'
if not exists(select [OperationType] from OperationTypes where @Type = [OperationType]) 
insert into OperationTypes ([OperationType]) values (@Type) 


select * from OperationTypes order by OperationTypeID
go



--some tools if you mess up the table
--DBCC CHECKIDENT('MYTableList', RESEED, 2) -- resets the ID counter to the number note: adds before setting it
--delete from MYTableList where TableID > 2