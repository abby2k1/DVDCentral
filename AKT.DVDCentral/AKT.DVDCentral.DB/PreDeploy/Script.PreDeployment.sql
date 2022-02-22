/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS dbo.tblMovieGenre
DROP TABLE IF EXISTS dbo.tblGenre
DROP TABLE IF EXISTS dbo.tblOrderItem
DROP TABLE IF EXISTS dbo.tblOrder
DROP TABLE IF EXISTS dbo.tblMovie
DROP TABLE IF EXISTS dbo.tblRating
DROP TABLE IF EXISTS dbo.tblDirector
DROP TABLE IF EXISTS dbo.tblFormat
DROP TABLE IF EXISTS dbo.tblCustomer
DROP TABLE IF EXISTS dbo.tblUser
