FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UniversityOrderAPI.BLL/UniversityOrderAPI.BLL.csproj", "UniversityOrderAPI.BLL/"]
RUN dotnet restore "UniversityOrderAPI.BLL/UniversityOrderAPI.BLL.csproj"
COPY . .
WORKDIR "/src/UniversityOrderAPI.BLL"
RUN dotnet build "UniversityOrderAPI.BLL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversityOrderAPI.BLL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversityOrderAPI.BLL.dll"]
