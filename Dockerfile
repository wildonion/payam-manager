FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base 
USER app 
WORKDIR /app 
EXPOSE 8080 
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
ARG BUILD_CONFIGURATION=Release 
WORKDIR /src 
COPY ["payam.csproj", "payam/"] 
RUN dotnet restore "./payam/payam.csproj" 
COPY . ./payam
WORKDIR "/src/payam" 
RUN dotnet build "./payam.csproj" -c $BUILD_CONFIGURATION -o /app/build 
 
FROM build AS publish 
ARG BUILD_CONFIGURATION=Release 
RUN dotnet publish "./payam.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false 
 
FROM base AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "payam.dll"]