# EFExample

## What is?
Basic configuration and use of entity framework.

## Why was it done?
I made this project to have a clear example of how to configure and use entity framework, how to make a minimal API and how to document an API with swagger.

## How it was made?
- First of all, the references of the entity framework and swagger packages were added executing the following command inside the project folder.
```
dotnet add package [package name]
```
- To globally configure entity framework commands in case it has not been previously configured.
```
dotnet tool install --global dotnet-ef
```
- To create a new entity framework migration.
```
dotnet ef migrations add [migration name]
```
- To update migrations from entity framework to the database.
```
dotnet ef database update
```

## How to run it?
- Build the solution to download dependencies.
- Modify the connection string to the database in the appsettings.json file according to the connection parameters of the sql server to which you want to connect.
- Run the following command inside the project folder to create the database
```
dotnet ef database update
```
- Ready to run the project

## Credits
- https://platzi.com/cursos/entity-framework/
- https://github.com/platzi/curso-fundamentos-entity-framework
