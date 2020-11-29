#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Bots/Bots.API/Bots.API.csproj", "Services/Bots/Bots.API/"]
COPY ["Services/Bots/Bots.Data/Bots.Data.csproj", "Services/Bots/Bots.Data/"]
COPY ["Services/Bots/Bots.Domain/Bots.Domain.csproj", "Services/Bots/Bots.Domain/"]
COPY ["Services/Bots/Bots.Service/Bots.Service.csproj", "Services/Bots/Bots.Service/"]
COPY ["Services/Bots/Bots.Messaging.Receive/Bots.Messaging.Receive.csproj", "Services/Bots/Bots.Messaging.Receive/"]
RUN dotnet restore "Services/Bots/Bots.API/Bots.API.csproj"
COPY . .
WORKDIR "/src/Services/Bots/Bots.API"
RUN dotnet build "Bots.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bots.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bots.API.dll"]