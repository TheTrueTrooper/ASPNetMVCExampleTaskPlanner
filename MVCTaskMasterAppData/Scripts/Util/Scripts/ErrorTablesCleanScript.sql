delete from ErrorTable
where 1=1 
delete from MYTableList
where 1=1 
delete from OperationTypes
where 1=1 

--if exists(select 	[name] from sysobjects where [name] = 'PseudoConstants_OperationTypes')
--	drop view [dbo].[PseudoConstants_OperationTypes]
--go
--if exists(select 	[name] from sysobjects where [name] = 'PseudoConstants_ErrorTables')
--	drop view [dbo].[PseudoConstants_ErrorTables]
--go

--some tools if you mess up the table
--DBCC CHECKIDENT('MYTableList', RESEED, 2) -- resets the ID counter to the number note: adds before setting it
--delete from MYTableList where TableID > 2