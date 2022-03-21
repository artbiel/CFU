#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CFU.UniversityManagement.WebAPI/CFU.UniversityManagement.WebAPI.csproj", "src/CFU.UniversityManagement.WebAPI/"]
COPY ["src/CFU.UniversityManagement.Infrastructure/CFU.UniversityManagement.Infrastructure.csproj", "src/CFU.UniversityManagement.Infrastructure/"]
COPY ["src/CFU.Application/CFU.UniversityManagement.Application.csproj", "src/CFU.Application/"]
COPY ["src/CFU.Domain/CFU.Domain.csproj", "src/CFU.Domain/"]
RUN dotnet restore "src/CFU.UniversityManagement.WebAPI/CFU.UniversityManagement.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/CFU.UniversityManagement.WebAPI"
RUN dotnet build "CFU.UniversityManagement.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CFU.UniversityManagement.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CFU.UniversityManagement.WebAPI.dll"]