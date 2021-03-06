FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build

COPY ./GavinHomeApi.Utilities ./GavinHomeApi.Utilities
COPY ./Services/GavinHomeApi.YoutubeDownload ./Services/GavinHomeApi.YoutubeDownload

#WORKDIR ./Services/GavinHomeApi.YoutubeDownload
RUN dotnet restore ./Services/GavinHomeApi.YoutubeDownload

RUN dotnet --version

RUN dotnet publish ./Services/GavinHomeApi.YoutubeDownload -c Release -o ./Services/GavinHomeApi.YoutubeDownload/out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /Services/GavinHomeApi.YoutubeDownload/out .

RUN apt-get update
RUN apt-get --assume-yes install python3-pip
RUN pip3 install youtube-dl

EXPOSE 80  

ENTRYPOINT ["dotnet","/app/GavinHomeApi.YoutubeDownload.dll"]