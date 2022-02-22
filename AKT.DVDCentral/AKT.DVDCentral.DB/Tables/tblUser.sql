CREATE TABLE [dbo].[tblUser]
(
	[ID] INT NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(255),
	[LastName] NVARCHAR(255),
	[UserName] VARCHAR(255),
	[Password] NVARCHAR(255)
)
