# Build the server project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY SpotifyDashboard.Server ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image for server
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

# Build the web project
FROM node:alpine AS web-env
WORKDIR /src/app

# Copy web project files
COPY SpotifyDashboard.Web ./

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Build and copy web project files
RUN npm build-production
RUN cp -a dist/* /App/wwwroot/

# Combine the server and web projects
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
COPY --from=web-env /src/app/wwwroot .

# Set the entry point
ENTRYPOINT ["dotnet", "SpotifyDashboard.Server.dll"]