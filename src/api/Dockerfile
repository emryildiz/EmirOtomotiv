FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EmirOtomotiv.sln .
COPY Core/EmirOtomotiv.Domain/EmirOtomotiv.Domain.csproj             Core/EmirOtomotiv.Domain/
COPY Core/EmirOtomotiv.Application/EmirOtomotiv.Application.csproj   Core/EmirOtomotiv.Application/
COPY Infrastructure/EmirOtomotiv.Infrastructure/EmirOtomotiv.Infrastructure.csproj Infrastructure/EmirOtomotiv.Infrastructure/
COPY Infrastructure/EmirOtomotiv.Persistence/EmirOtomotiv.Persistence.csproj       Infrastructure/EmirOtomotiv.Persistence/
COPY Presentation/EmirOtomotiv.Api/EmirOtomotiv.Api.csproj            Presentation/EmirOtomotiv.Api/

RUN dotnet restore

COPY . .

RUN dotnet publish Presentation/EmirOtomotiv.Api/EmirOtomotiv.Api.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmirOtomotiv.Api.dll"]
