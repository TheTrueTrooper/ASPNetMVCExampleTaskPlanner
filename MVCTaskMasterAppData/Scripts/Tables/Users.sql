CREATE TABLE [dbo].[Users]
(
	[UserID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleInitial] NCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Password] NCHAR(50) NOT NULL, 
	[Salt] CHAR(28) NOT NULL,
    [HomePhone] CHAR(11) NOT NULL, 
    [CellPhone] CHAR(11) NULL, 
    [WorkPhone] CHAR(11) NULL, 

    CONSTRAINT [CK_Users_Email] CHECK (Email like '[^.]%[^.][@]%[.]%'), 
    CONSTRAINT [CK_Users_HomePhone] CHECK (HomePhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	CONSTRAINT [CK_Users_CellPhone] CHECK (CellPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	CONSTRAINT [CK_Users_WorkPhone] CHECK (WorkPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'), 
    CONSTRAINT [AK_Users_Email] UNIQUE ([Email])
)
