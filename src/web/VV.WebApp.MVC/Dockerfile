#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/web/VV.WebApp.MVC/VV.WebApp.MVC.csproj", "src/web/VV.WebApp.MVC/"]
RUN dotnet restore "src/web/VV.WebApp.MVC/VV.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/src/web/VV.WebApp.MVC"
RUN dotnet build "VV.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VV.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VV.WebApp.MVC.dll"]