FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Annexio_Assessment/Annexio_Assessment.csproj", "Annexio_Assessment/"]
RUN dotnet restore "Annexio_Assessment/Annexio_Assessment.csproj"
COPY . .
WORKDIR "/src/Annexio_Assessment"
RUN dotnet build "Annexio_Assessment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Annexio_Assessment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Annexio_Assessment.dll"]
