# Enviroment

## Install SqlServer
`
docker run -e 'ACCEPT_EULA=Y' \
	--name mssql2017 \
	--restart unless-stopped \
	-e 'MSSQL_SA_PASSWORD=pas$word' \
	-p 1433:1433 \
	-v ~/projects/global/.sqlserver/data:/var/opt/mssql/data \
	-v ~/projects/global/.sqlserver/log:/var/opt/mssql/log \
	-v ~/projects/global/.sqlserver/secrets:/var/opt/mssql/secrets \
	-d mcr.microsoft.com/mssql/server:2017-latest
`

## Install Seq server
`
$docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:2021.4
`

# Scaffold
`
$dotnet ef dbcontext scaffold "Server=localhost;Database=bookstoredb;User Id=sa; Password=pas$word;Trusted_Connection=true;" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Data
$dotnet ef dbcontext scaffold Name=ConnectionStrings:BookStoreAppDbConnection Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Data
Scaffold-DbContext Name=ConnectionStrings:BookStoreAppDbConnection Microsoft.EntityFrameworkCore.SqlServer -Context BookStoreDbContext -ContextDir Data -OutputDir Data
`

# Nuget

## Add Seq Log
`
$cd BookStoreApp.Api
$dotnet add package Serilog.AspNetCore
$dotnet add package Serilog.Expressions
$dotnet add package Serilog.Sinks.Seq
`

## Install EfCore
`
$dotnet add package Microsoft.EntityFrameworkCore.SqlServer
$dotnet add package Microsoft.EntityFrameworkCore.Tools
$dotnet add package Microsoft.EntityFrameworkCore.Design
`

# appsetting.json

## Serilog
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "./.logs/log-.txt",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "Seq",
        "Args": { "serverUrl": "http://localhost:5341" }
      }
    ]
  }
}

## JwtSettings
{
  "JwtSettings": {
    "Issuer": "BookStoreAPI",
    "Audience": "BookStoreApiClient",
    "Duration": 3600 //seconds
  },
}

## secrets.json
{
  "JwtSettings": {
    "Key": "secrect890123456" //length must be greater than 16
  }
}
