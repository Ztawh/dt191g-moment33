# Moment 3.3 - Minimalt API med .NET
Detta moment ingår i kursen Webbutveckling med .NET. Jag har skapat ett minimalt API men .NET. Detta har gjorts med terminalen och följande kommandon:
* dotnet new webapi -minimal
* dotnet add package Microsoft.EntityFrameworkCore.SQLite
* dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
* dotnet ef migrations add InitialCreate
* dotnet ef database update

Därefter har jag lagt till stöd för PUT och DELETE också.

## Klona
* git clone https://github.com/Ztawh/dt191g-moment33.git