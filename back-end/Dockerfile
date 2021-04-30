FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ROS.Api/ROS.Api.csproj", "ROS.Api/"]
COPY ["ROS.Infrastructure/ROS.Infrastructure.csproj", "ROS.Infrastructure/"]
COPY ["ROS.Common/ROS.Common.csproj", "ROS.Common/"]
COPY ["ROS.Services/ROS.Services.csproj", "ROS.Services/"]
COPY ["ROS.Data/ROS.Data.csproj", "ROS.Data/"]
COPY ["ROS.EntityFramework/ROS.EntityFramework.csproj", "ROS.EntityFramework/"]
COPY ["ROS.Contracts/ROS.Contracts.csproj", "ROS.Contracts/"]
RUN dotnet restore "ROS.Api/ROS.Api.csproj"
COPY . .
WORKDIR "/src/ROS.Api"
RUN dotnet build "ROS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ROS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet "ROS.Api.dll"