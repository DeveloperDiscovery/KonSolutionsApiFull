FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["KonSolutionsApi.csproj", "./"]
RUN dotnet restore "KonSolutionsApi.csproj"
COPY . .
RUN dotnet publish "KonSolutionsApi.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
RUN apt-get update && apt-get install -y curl --no-install-recommends && rm -rf /var/lib/apt/lists/* \
 && mkdir -p /app/logs \
 && adduser --disabled-password --gecos "" appuser \
 && chown -R appuser /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1
USER appuser
ENTRYPOINT ["dotnet", "KonSolutionsApi.dll"]
