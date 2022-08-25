# API BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS api-build-stage

WORKDIR /opt/app/API.BL
COPY sources/API.BL/*.csproj .
RUN dotnet restore

WORKDIR /opt/app/API.Data
COPY sources/API.Data/*.csproj .
RUN dotnet restore

WORKDIR /opt/app/API
COPY sources/API/*.csproj .
RUN dotnet restore

WORKDIR /opt/app
COPY sources .
WORKDIR /opt/app/API
RUN dotnet publish -c Release -o out

# RUN API
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=api-build-stage /opt/app/API/out .
ENTRYPOINT ["dotnet", "API.dll"]