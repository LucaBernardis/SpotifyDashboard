# Build stage for the server
FROM mcr.microsoft.com/dotnet/sdk:6.0.100 as build-server

# Set the working directory to /app
WORKDIR /app

# Copy the entire SpotifyDashboard directory
COPY . .

# Navigate into the Server directory
WORKDIR /app/SpotifyDashboard.Server

# Restore NuGet packages
RUN dotnet restore

# Add MongoDB dependency
RUN dotnet add package MongoDB.Bson
RUN dotnet add package MongoDB.Driver

# Build the server project
RUN dotnet build -c Release SpotifyDashboard.Server.csproj

# Publish the server project
RUN dotnet publish -c Release -o out SpotifyDashboard.Server.csproj

# Build stage for the frontend
FROM node:20 as build-frontend

# Set the working directory to /app
WORKDIR /app

# Copy the entire SpotifyDashboard directory
COPY . .

# Navigate into the Web directory
WORKDIR /app/SpotifyDashboard.Web

# Install npm dependencies
RUN npm install

# Build the frontend project
RUN npm run build --prod

# Final stage for the runtime environment
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0

# Set the working directory to /app
WORKDIR /app

# Copy the published server project
COPY --from=build-server /app/SpotifyDashboard.Server/out .

# Copy the built frontend project
COPY --from=build-frontend /app/SpotifyDashboard.Web/dist .

# Expose the port for the server
EXPOSE 80

# Set environment variables for MongoDB connection
ENV MONGODB_URI=mongodb://localhost:27017/
ENV MONGODB_DB=Spotify
ENV MONGODB_COLLECTION=Tiles

# Run the server when the container starts
CMD ["dotnet", "SpotifyDashboard.Server.dll"]