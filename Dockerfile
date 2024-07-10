# Stage 1: Build the backend (C#)
FROM mcr.microsoft.com/dotnet/core/sdk:6.0 as backend
WORKDIR /app
COPY SpotifyDashboard.Server/SpotifyDashboard.Server.csproj .
RUN dotnet restore
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# Stage 2: Build the frontend (Angular/TypeScript)
FROM node:20 as frontend
WORKDIR /app
COPY SpotifyDashboard.Web .
RUN npm install
RUN npm run build

# Stage 3: Create a single image with both applications
FROM ubuntu:20.04
WORKDIR /app
COPY --from=backend /app/out .
COPY --from=frontend /app .
EXPOSE 5000
EXPOSE 4200
CMD ["dotnet", "out/SpotifyDashboard.Server.dll"] && npm start