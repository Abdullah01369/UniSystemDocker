FROM mcr.microsoft.com/dotnet/sdk:7.0 as build


WORKDIR /app

COPY ./UniSystem.Core/*.csproj ./UniSystem.Core/
COPY ./UniSystem.Data/*.csproj ./UniSystem.Data/
COPY ./UniSystem.Service/*.csproj ./UniSystem.Service/
COPY ./UniSystem.API/*.csproj ./UniSystem.API/
COPY ./WorkerService/*.csproj ./WorkerService/
COPY ./SharedLayer/*.csproj ./SharedLayer/

COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./UniSystem.API/*.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet","UniSystem.API.dll"]