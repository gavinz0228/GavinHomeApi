FROM microsoft/dotnet:3.0-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:3.0-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet","GavinHomeApi.dll"]