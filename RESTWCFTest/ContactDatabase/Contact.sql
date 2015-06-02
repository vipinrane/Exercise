CREATE TABLE [dbo].[Contact]
(
		 ContactId INT NOT NULL PRIMARY KEY IDENTITY,
         Name varchar(100),
		 Address varchar(100),
         City varchar(70),
         State varchar(100),
         Zip varchar(100),
         Email varchar(100),
         Twitter varchar(100),
)
