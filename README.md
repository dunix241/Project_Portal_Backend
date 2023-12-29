# EIU_Projects
[//]: # (Project description)
>**This page contains important information on how to properly configure and run EIU_Projects**

## Table of contents
- [Used Technologies](#used-technologies)
- [Set up](#set-up)
- [Start the server](#start-the-server)

## Used Technologies
- ASP.NET Core Web API
- Entity Framework Core
- MediatR
- AutoMapper

## Getting started
### Set up
#### Install Dotnet SDK
##### Windows
Use any of the following methods
- Winget
```shell
winget install Microsoft.DotNet.SDK.<version>
```
supported versions are `6`, `7`, `8`
- Download the sdk from the [official website](https://dotnet.microsoft.com/en-us/download/dotnet)
##### Linux
Instructions to install the Dotnet SDK on several Linux distros can be found [here](https://learn.microsoft.com/en-us/dotnet/core/install/linux)

#### Install `dotnet ef` (optional)
```shell
dotnet tool install --global dotnet-ef
```

### Start the server
#### with dotnet command
```shell
cd API
dotnet run
```

#### with Docker
```shell
docker build -t api -f API/Dockerfile .
```
