# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the entire application and build it
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set the environment variable to Docker
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=http://+:5084

# Copy the published output from the build stage
COPY --from=build-env /app/out .

# Expose the port your application is configured to listen on
EXPOSE 5084

# Define the entry point for the container
ENTRYPOINT ["dotnet", "DotNetWebAPI_InMemoryDatabase.dll"]