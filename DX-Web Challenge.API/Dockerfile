#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["DX-Web Challenge.API/DX-Web Challenge.API.csproj", "DX-Web Challenge.API/"]
COPY ["DX-Web Challenge.Business/DX-Web Challenge.Business.csproj", "DX-Web Challenge.Business/"]
COPY ["DX-Web Challenge.Core/DX-Web Challenge.Core.csproj", "DX-Web Challenge.Core/"]
COPY ["DX-Web Challenge.Data/DX-Web Challenge.Data.csproj", "DX-Web Challenge.Data/"]
RUN dotnet restore "DX-Web Challenge.API/DX-Web Challenge.API.csproj"
COPY . .
WORKDIR "/src/DX-Web Challenge.API"
RUN dotnet build "DX-Web Challenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DX-Web Challenge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DX-Web Challenge.API.dll"]