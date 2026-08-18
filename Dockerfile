# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY GrupoArmaReforger.csproj ./
RUN dotnet restore GrupoArmaReforger.csproj
COPY . .
RUN dotnet publish GrupoArmaReforger.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
RUN mkdir -p /app/data
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "GrupoArmaReforger.dll"]
