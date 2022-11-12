CREATE TABLE [dbo].[tblCustomer]
(
	[ID] INT NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(255),
	[LastName] NVARCHAR(255),
	[Address] NVARCHAR(255),
	[City] NVARCHAR(255),
	[State] NVARCHAR(255),
	[Zip] NVARCHAR(10),
	[Phone] NVARCHAR(255),
	[UserID] INT NOT NULL
)
