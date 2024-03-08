#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BE.QuizByGPT/BE.QuizByGPT.csproj", "BE.QuizByGPT/"]
COPY ["BE.QuizByGPT.Data/BE.QuizByGPT.DAL.csproj", "BE.QuizByGPT.Data/"]
COPY ["BE.QuizByGPT.BLL/BE.QuizByGPT.BLL.csproj", "BE.QuizByGPT.BLL/"]
COPY ["BE.QuizByGPT.GPT/BE.QuizByGPT.GPT.csproj", "BE.QuizByGPT.GPT/"]
RUN dotnet restore "./BE.QuizByGPT/./BE.QuizByGPT.csproj"
COPY . .
WORKDIR "/src/BE.QuizByGPT"
RUN dotnet build "./BE.QuizByGPT.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BE.QuizByGPT.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BE.QuizByGPT.dll"]