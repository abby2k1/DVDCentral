/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\DefaultData\Users.sql
:r .\DefaultData\Customers.sql
:r .\DefaultData\Formats.sql
:r .\DefaultData\Directors.sql
:r .\DefaultData\Ratings.sql
:r .\DefaultData\Movies.sql
:r .\DefaultData\Orders.sql
:r .\DefaultData\OrdersItems.sql
:r .\DefaultData\Genres.sql
:r .\DefaultData\MoviesGenres.sql
