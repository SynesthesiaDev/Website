FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV DOTNET_GCLatencyMode=0
ENV DOTNET_GCHeapHardLimitPercent=70
ENV DOTNET_GCConserveMemory=9

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Website/Website.csproj", "Website/"]
RUN dotnet restore "Website/Website.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/Website"
RUN dotnet build "./Website.csproj" -c $BUILD_CONFIGURATION -o /app/build -r linux-arm64

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Website.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
RUN mkdir -p /app/data && chown -R 1654:1654 /app/data
ENV DOTNET_RUNNING_IN_CONTAINER=true
USER root
ENTRYPOINT ["dotnet", "Website.dll", "--urls", "http://0.0.0.0:8080"]
