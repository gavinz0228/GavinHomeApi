FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

COPY ./GavinHomeApi.Utilities ./GavinHomeApi.Utilities
COPY ./Services/GavinHomeApi.YoutubeDownload ./Services/GavinHomeApi.YoutubeDownload

WORKDIR ./Services/GavinHomeApi.YoutubeDownload
RUN dotnet restore

RUN dotnet --version

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /Services/GavinHomeApi.YoutubeDownload/out .

RUN apt-get update
RUN apt-get --assume-yes install youtube-dl

EXPOSE 80  

ENTRYPOINT ["dotnet","app/GavinHomeApi.YoutubeDownload.dll"]