#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/BotnetJobs/BotnetJobs.API/BotnetJobs.API.csproj", "Services/BotnetJobs/BotnetJobs.API/"]
COPY ["Services/BotnetJobs/BotnetJobs.Messaging.Send/BotnetJobs.Messaging.Send.csproj", "Services/BotnetJobs/BotnetJobs.Messaging.Send/"]
COPY ["Services/BotnetJobs/BotnetJobs.Domain/BotnetJobs.Domain.csproj", "Services/BotnetJobs/BotnetJobs.Domain/"]
COPY ["Services/BotnetJobs/BotnetJobs.Service/BotnetJobs.Service.csproj", "Services/BotnetJobs/BotnetJobs.Service/"]
COPY ["Services/BotnetJobs/BotnetJobs.Data/BotnetJobs.Data.csproj", "Services/BotnetJobs/BotnetJobs.Data/"]
RUN dotnet restore "Services/BotnetJobs/BotnetJobs.API/BotnetJobs.API.csproj"
COPY . .
WORKDIR "/src/Services/BotnetJobs/BotnetJobs.API"
RUN dotnet build "BotnetJobs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BotnetJobs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotnetJobs.API.dll"]