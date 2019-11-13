FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY StaffTransferCore/StaffTransferCore.csproj StaffTransferCore/
RUN dotnet restore StaffTransferCore/StaffTransferCore.csproj
COPY . .
WORKDIR /src/StaffTransferCore
RUN dotnet build StaffTransferCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish StaffTransferCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StaffTransferCore.dll"]
