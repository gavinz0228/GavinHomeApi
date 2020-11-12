FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

COPY *.csproj ./
COPY ../../GavinHomeApi.Utilities/GavinHomeApi.Utilities.csproj ./
RUN dotnet restore

RUN dotnet --version

COPY . ./
RUN dotnet publish -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
#WORKDIR /app
#COPY --from=build /app/out .

RUN apt-get update
RUN apt-get --assume-yes install youtube-dl

EXPOSE 80  

ENTRYPOINT ["dotnet","out/GavinHomeApi.YoutubeDownload.dll"]