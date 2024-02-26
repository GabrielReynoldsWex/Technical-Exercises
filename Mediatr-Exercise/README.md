Second attempt at a MediatR package technical exercise.

Requires the following packages:

1. MediatR
2. MediatR.Extensions.Microsoft.DependencyInjection
3. Microsoft.EntityFrameworkCore.SqlServer
4. Microsoft.EntityFrameworkCore.Tools
5. Swashbuckle.AspNetCore

Requires a local installation of SQL to run. To initialise the following, run the following commands through the package-manager console:

1. Add-migration CustomerTableAdded
2. Add-migration ItemTableAdded
3. Add-migration PurchaseTableAdded
4. update-database

The database is empty by default. Values can be added through Swagger.
