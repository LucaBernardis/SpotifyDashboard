# Stage 1: Build the backend (C#)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as backend
WORKDIR /app
COPY SpotifyDashboard.Server/SpotifyDashboard.Server.csproj .

RUN echo "Restore started"
RUN dotnet restore

RUN echo "Build server started"
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# Stage 2: Build the frontend (Angular/TypeScript)
FROM node:22 as frontend
WORKDIR /app
COPY SpotifyDashboard.Web .

RUN echo "installing npm"
RUN npm install

RUN echo "Buil npm"
RUN npm run build

# Stage 3: Create a single image with both applications
FROM ubuntu:20.04
WORKDIR /app
COPY --from=backend /app/out .
COPY --from=frontend /app .

RUN echo "exposing ports"
EXPOSE 5000
EXPOSE 4200
CMD ["dotnet", "out/SpotifyDashboard.Server.dll"] && npm start