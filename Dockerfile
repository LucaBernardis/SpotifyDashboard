FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
RUN echo "Build the server project"
WORKDIR /BuildOutput
# Copy everything
COPY SpotifyDashboard.Server ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out --no-restore

FROM node:alpine AS web-env
RUN echo "Build the web project"
WORKDIR /BuildOutput
# Copy everything
COPY SpotifyDashboard.Web ./
# Install Node.js and dependencies
RUN npm install -g @angular/cli
RUN npm install
# Build and copy web project files
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN echo "Combine the server and the web project"
WORKDIR /App
COPY --from=build-env /BuildOutput/out .
COPY --from=web-env /BuildOutput/dist/SpotifyDashboard.Web/browser /App/wwwroot

RUN echo "Set the entrypoints and ports"
EXPOSE 8080
ENTRYPOINT ["dotnet", "SpotifyDashboard.Server.dll"]