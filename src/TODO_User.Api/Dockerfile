#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TODO_User.Api/TODO_User.Api.csproj", "src/TODO_User.Api/"]
COPY ["src/TODO_User.Application/TODO_User.Application.csproj", "src/TODO_User.Application/"]
COPY ["src/TODO_User.Domain/TODO_User.Domain.csproj", "src/TODO_User.Domain/"]
COPY ["src/TODO_User.Infrastructure/TODO_User.Infrastructure.csproj", "src/TODO_User.Infrastructure/"]
RUN dotnet restore "./src/TODO_User.Api/./TODO_User.Api.csproj"
COPY . .
WORKDIR "/src/src/TODO_User.Api"
RUN dotnet build "./TODO_User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TODO_User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TODO_User.Api.dll"]