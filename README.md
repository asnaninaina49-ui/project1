# Community Notice Board — ASP.NET Core MVC

A student-friendly ASP.NET Core MVC project using C#, Entity Framework Core, SQL Server LocalDB, and ASP.NET Core Identity.

## Features
- User registration and login
- Role-based access: Admin and User
- Admin can create, edit, and delete notices
- Signed-in users can browse, search, filter, and read notices
- Categories, publish date, important-notice flag, and optional attachment URL
- Sample notices are inserted on first run
- Responsive Bootstrap layout

## Requirements
- Windows 10/11
- .NET 8 SDK
- Visual Studio 2022 (ASP.NET and web development workload) or VS Code
- SQL Server LocalDB (usually included with Visual Studio)

## Run in Visual Studio
1. Extract the ZIP.
2. Open `CommunityNoticeBoard.csproj` in Visual Studio.
3. Allow NuGet packages to restore.
4. Ensure SQL Server LocalDB is installed.
5. Press F5 or click Run.

## Run from terminal
Open PowerShell in the extracted `CommunityNoticeBoard` folder:

```powershell
dotnet --list-sdks
dotnet restore
dotnet run
```

Open the localhost URL printed in the terminal.

## Initial Admin login
- Email: `admin@communityboard.com`
- Password: `Admin@12345`

The application creates this account and the Admin/User roles on first startup if they do not exist. Change the sample password before using this project beyond a local classroom demonstration. Never publish this demo password.

## Database
Connection string is in `appsettings.json` and uses SQL Server LocalDB:
`(localdb)\\MSSQLLocalDB`, database `CommunityNoticeBoardDB`.

This starter uses `EnsureCreated()` to create the database automatically on first run. If you later add EF Core migrations, remove `EnsureCreated()` and use migrations consistently.

## If `dotnet` says no SDKs are found
Install the .NET 8 SDK (not just the runtime), restart Visual Studio/PowerShell, then verify with:

```powershell
dotnet --list-sdks
```

## Suggested viva/demo flow
1. Register a normal user and log in.
2. Open All Notices and search/filter.
3. Log out.
4. Log in with the admin account above.
5. Publish a notice, edit it, then delete a test notice.
6. Show that normal users do not see the admin management links and cannot access admin-only actions.

## Important note
This is a learning/demo project. For production use, add stronger deployment configuration, HTTPS setup, secure secret management, audit logging, and review file attachment handling.
