USE master;
GO

ALTER DATABASE TodoDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE TodoDB;
