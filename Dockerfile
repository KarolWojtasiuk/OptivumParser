FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY . ./
RUN dotnet publish OptivumParser.Api -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /build/output ./
ENTRYPOINT ["dotnet", "OptivumParser.Api.dll"]
