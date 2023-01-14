FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UniversityOrderAPI.DAL/UniversityOrderAPI.DAL.csproj", "UniversityOrderAPI.DAL/"]
RUN dotnet restore "UniversityOrderAPI.DAL/UniversityOrderAPI.DAL.csproj"
COPY . .
WORKDIR "/src/UniversityOrderAPI.DAL"
RUN dotnet build "UniversityOrderAPI.DAL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversityOrderAPI.DAL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversityOrderAPI.DAL.dll"]
