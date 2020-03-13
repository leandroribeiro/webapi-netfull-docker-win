IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'TEST' )
  EXEC('CREATE SCHEMA [TEST] AUTHORIZATION [dbo]');
GO