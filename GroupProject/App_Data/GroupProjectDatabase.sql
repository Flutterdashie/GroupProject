use [master]
go

if exists (select * from sys.databases where name = N'GroupProject')
begin
	drop database GroupProject;
end

IF EXISTS(
   SELECT *
   FROM sys.server_principals
   WHERE [Name] = 'BlogApp')
BEGIN
   DROP LOGIN BlogApp
END
GO

CREATE LOGIN BlogApp WITH PASSWORD='testing123'


create database GroupProject
go
use GroupProject
go

DROP USER IF EXISTS [Admin]
GO
CREATE USER [Admin] FOR LOGIN BlogApp
GO
ALTER ROLE db_backupoperator ADD MEMBER [Admin]
ALTER ROLE db_ddladmin ADD MEMBER [Admin]
ALTER ROLE db_datareader ADD MEMBER [Admin]
ALTER ROLE db_datawriter ADD MEMBER [Admin]
GO
