FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PersonalBlog.Api/PersonalBlog.Api.csproj", "PersonalBlog.Api/"]
RUN dotnet restore "PersonalBlog.Api/PersonalBlog.Api.csproj"
COPY . .
WORKDIR "/src/PersonalBlog.Api"
RUN dotnet build "PersonalBlog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PersonalBlog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalBlog.Api.dll"]
