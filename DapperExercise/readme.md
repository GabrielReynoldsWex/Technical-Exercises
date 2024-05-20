Technical Exercise for the Dapper Micro-ORM. Also utilises Fluent Migration and the Fluent Migration SQL Extension Packages.

Application provides a basic database consisting of tables Company and Employee, and various endpoints to manipulate their contents. Tables are seeded with data upon startup.

To utilise all endpoints, run the following SQL Script to store a procedure:

```
USE [DapperExercise]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowCompanyForProvidedEmployeeId] @Id int
AS
SELECT c.Id, c.Name, c.Address, c.Country
FROM Company c JOIN Employee e ON c.Id = e.CompanyId
Where e.Id = @Id
GO
```

Used the following exercises:

https://code-maze.com/dapper-migrations-fluentmigrator-aspnetcore/

https://code-maze.com/using-dapper-with-asp-net-core-web-api/
