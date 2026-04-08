FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EmirOtomotiv.sln .
COPY Core/EmirOtomotiv.Domain/EmirOtomotiv.Domain.csproj             Core/EmirOtomotiv.Domain/
COPY Core/EmirOtomotiv.Application/EmirOtomotiv.Application.csproj   Core/EmirOtomotiv.Application/
COPY Infrastructure/EmirOtomotiv.Infrastructure/EmirOtomotiv.Infrastructure.csproj Infrastructure/EmirOtomotiv.Infrastructure/
COPY Infrastructure/EmirOtomotiv.persistence/EmirOtomotiv.Persistence.csproj       Infrastructure/EmirOtomotiv.persistence/
COPY Presentation/Emirotomotiv.api/EmirOtomotiv.Api.csproj            Presentation/Emirotomotiv.api/

RUN dotnet restore

COPY . .

RUN dotnet publish Presentation/Emirotomotiv.api/EmirOtomotiv.Api.csproj \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmirOtomotiv.Api.dll"]
