# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Disable NuGet fallback package folders to avoid fallback folder errors
ENV NUGET_FALLBACK_PACKAGES=""

WORKDIR /app

# Copy solution and project files
COPY ObiletJourneyApp.sln ./
COPY ObiletJourneyApp.Domain/ObiletJourneyApp.Domain.csproj ./ObiletJourneyApp.Domain/
COPY ObiletJourneyApp.Application/ObiletJourneyApp.Application.csproj ./ObiletJourneyApp.Application/
COPY ObiletJourneyApp.Infrastructure/ObiletJourneyApp.Infrastructure.csproj ./ObiletJourneyApp.Infrastructure/
COPY ObiletJourneyApp.Tests/ObiletJourneyApp.Tests.csproj ./ObiletJourneyApp.Tests/
COPY ObiletJourneyApp.ConsoleTest/ObiletJourneyApp.ConsoleTest.csproj ./ObiletJourneyApp.ConsoleTest/
COPY ObiletJourneyApp.CompositionRoot/ObiletJourneyApp.CompositionRoot.csproj ./ObiletJourneyApp.CompositionRoot/
COPY ObiletJourneyApp.WebUI/ObiletJourneyApp.WebUI.csproj ./ObiletJourneyApp.WebUI/

# Restore dependencies
RUN dotnet restore ObiletJourneyApp.sln

# Copy the rest of the source code
COPY . ./

# Publish the project
RUN dotnet publish ./ObiletJourneyApp.WebUI/ObiletJourneyApp.WebUI.csproj -c Release -o out

# Use the official ASP.NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from build stage
COPY --from=build /app/out ./

# Expose the port the app listens on
EXPOSE 8080

# Set environment variable for ASP.NET Core URLS
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "ObiletJourneyApp.WebUI.dll"]