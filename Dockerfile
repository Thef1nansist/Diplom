#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdYou/AdYou.csproj", "AdYou/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Infastructure/Infastructure.csproj", "Infastructure/"]
RUN dotnet restore "AdYou/AdYou.csproj"
COPY . .
WORKDIR "/src/AdYou"
RUN dotnet build "AdYou.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdYou.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdYou.dll", "--urls", "http://0.0.0.0:8080"]