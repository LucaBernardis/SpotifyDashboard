# Build stage for the server
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-server

# Set the working directory to /app
WORKDIR /app

# Copy the server project file
COPY SpotifyDashboard.Server/SpotifyDashboard.Server.csproj .

# Restore NuGet packages
RUN dotnet restore

# Build the server project
RUN dotnet build -c Release SpotifyDashboard.Server.csproj

# Publish the server project
RUN dotnet publish -c Release -o out SpotifyDashboard.Server.csproj

# Build stage for the frontend
FROM node:20 as build-frontend

# Set the working directory to /app
WORKDIR /app

# Copy the frontend project files
COPY SpotifyDashboard.Web/package*.json ./
COPY SpotifyDashboard.Web/tsconfig.json ./
COPY SpotifyDashboard.Web/angular.json ./

# Install npm dependencies
RUN npm install

# Copy the rest of the frontend project files
COPY SpotifyDashboard.Web/. .

# Build the frontend project
RUN npm run build --prod

# Final stage for the runtime environment
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# Set the working directory to /app
WORKDIR /app

# Copy the published server project
COPY --from=build-server /app/out .

# Copy the built frontend project
COPY --from=build-frontend /app/dist .

# Expose the port for the server
EXPOSE 80

# Run the server when the container starts
CMD ["dotnet", "SpotifyDashboard.Server.dll"]