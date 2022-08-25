@echo off
set CONNECTION_STRING=Server=localhost;Port=3307;Database=foobar;User=root;Password=O6SoStdNb15DcDckORoa

dotnet ef database update -p .. -- %CONNECTION_STRING%
pause