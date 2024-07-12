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
ENTRYPOINT ["dotnet", "SpotifyDashboard.Server.dll"]

# Build the web project
FROM node:alpine AS web-env
WORKDIR /usr/src/app

# Copy web project files
COPY SpotifyDashboard.Web ./

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Expose port for web server
EXPOSE 4200

# Run web server
CMD ["ng", "serve", "--host", "0.0.0.0"]

# Create a final image that combines both server and web projects
FROM ubuntu:latest
WORKDIR /app

# Copy server files
COPY --from=0 /App /app/server

# Copy web files
COPY --from=1 /usr/src/app /app/web

# Expose ports for both server and web
EXPOSE 80 4200

# Run both server and web projects
CMD ["dotnet", "SpotifyDashboard.Server.dll", "&", "ng", "serve", "--host", "0.0.0.0"]