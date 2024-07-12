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
WORKDIR /src/app

# Copy web project files
COPY SpotifyDashboard.Web ./

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Build the web project
RUN npm run build --prod

# Copy built files to the correct location
COPY --from=web-env /src/app/dist /App/wwwroot

# Expose port for web server
EXPOSE 4200

# Run web server
CMD ["ng", "serve"]