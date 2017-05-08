CREATE TABLE [dbo].[ProjectFiles]
(
	[FileID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProjectID] INT NOT NULL, 
	--size is 2 tothe 16 - 1 = 65,536 - 1 bytes
    [File] VARBINARY(16) NOT NULL, 
    CONSTRAINT [FK_ProjectFiles_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [Projects]([ProjectID])
)
