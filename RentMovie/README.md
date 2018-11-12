## Overview of Stack
 - ASP.NET Core 2.1
 - Sql Server
 - Entity Framework Core w/ EF Migrations
 - Identity Server 4
 - Dapper
 - User Secret

## Setup
First off, open your CLI tool, and set the working directory to where you cloned the project using 
`cd` command

### `dotnet build`
Command to restore .nuget packages and build application

### `dotnet ef database update`
Command to apply migrations on empty database

### `Set User Secret`

To [configure](https://docs.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows) connection string to your database, you have to right click on Web project, click on Manage User Secrets. Your secrets.json should look like this

`{  
  "Movies:ServiceApiKey": "12345",
  "Movies:ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=RentMovieDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}`

- Then open cmd and go to your application RentMovie (web project) folder, and apply this command
`dotnet user-secrets set "Movies:ServiceApiKey" "12345"`
