# Use the .NET SDK image which includes dotnet watch
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set the working directory where your source will be mounted
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_FORWARDEDHEADERS_ENABLED=true 

# Expose the API port (adjust as necessary)
EXPOSE 8080


# The container command:
# - Changes into the API project folder (adjust if needed)
# - Restores dependencies with warnings not treated as errors
# - Runs dotnet watch so that file changes trigger rebuilds
CMD ["sh", "-c", "cd src/ResumePro.Api && dotnet restore /p:TreatWarningsAsErrors=false && dotnet watch run /p:TreatWarningsAsErrors=false"]
