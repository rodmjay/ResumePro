FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_FORWARDEDHEADERS_ENABLED=true 

EXPOSE 8080

CMD ["sh", "-c", "cd src/ResumePro.Api && \
 dotnet restore /p:TreatWarningsAsErrors=false && \
 dotnet watch run --no-launch-profile"]
