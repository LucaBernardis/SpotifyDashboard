# Build the server project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

RUN echo "Copy everything"
# Copy everything
COPY SpotifyDashboard.Server ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

EXPOSE 8080

RUN echo "Build runtime image for server"
# Build runtime image for server
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

RUn echo "Build the web project"
# Build the web project
FROM node:alpine AS web-env
WORKDIR /src/app

RUn echo "Copy web project files"
# Copy web project files
COPY SpotifyDashboard.Web ./

Run echo "Install dependencies"
# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

RUN echo "Build and cp web project files"
# Build and copy web project files
RUN npm run build-production
RUN cp -a dist/* /App/

RUN echo "Combine the server and the eb project"
# Combine the server and web projects
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
COPY --from=web-env /src/app/ .

RUN echo "Set the entrypoint"
# Set the entry point
ENTRYPOINT ["dotnet", "SpotifyDashboard.Server.dll"]