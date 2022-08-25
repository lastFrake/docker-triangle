@echo off

set CONNECTION_STRING=Server=localhost;Port=3307;Database=foobar;User=root;Password=O6SoStdNb15DcDckORoa
set /p MIGRATION_NAME="Migration name: "

dotnet ef migrations add %MIGRATION_NAME%  -n "API.Data.Migrations" -o Data/Migrations -p .. -- %CONNECTION_STRING%
pause