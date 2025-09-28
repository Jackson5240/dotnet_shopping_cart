# Server Dockerfile for ASP.NET Core 9
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./server.csproj
##for production
#RUN dotnet publish ./server.csproj -c Release -o /app/publish
## for deugging
RUN dotnet publish ./server.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 🔹 Install debugger (only needed for dev/debug images)
#RUN apt-get update && apt-get install -y unzip curl \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/linux-x64/vsdbg.tar.gz \
#    | tar -xz -C /vsdbg

# Install debugger for VS Code
#RUN apt-get update \
#    && apt-get install -y unzip curl \
#    && mkdir -p /vsdbg \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/linux-x64/vsdbg.tar.gz \
#    | tar -xz -C /vsdbg

#RUN apt-get update \
#    && apt-get install -y unzip curl ca-certificates \
##    && mkdir -p /vsdbg \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/linux-x64/vsdbg.tar.gz \
#    -o /tmp/vsdbg.tar.gz \
#    && tar -xzf /tmp/vsdbg.tar.gz -C /vsdbg \
#    && rm /tmp/vsdbg.tar.gz

#RUN apt-get update && apt-get install -y unzip curl ca-certificates \
#    && mkdir -p /vsdbg \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/vsdbg-linux-x64.tar.gz \
#    -o /tmp/vsdbg.tar.gz \
#    && tar -xzf /tmp/vsdbg.tar.gz -C /vsdbg \
#    && rm /tmp/vsdbg.tar.gz

#RUN apt-get update && apt-get install -y unzip curl ca-certificates \
#    && mkdir -p /vsdbg \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/vsdbg-linux-x64.tar.gz \
#    -o /tmp/vsdbg.tar.gz \
#    && tar -xzf /tmp/vsdbg.tar.gz -C /vsdbg \
#    && rm /tmp/vsdbg.tar.gz

## Install debugger for VS Code
#RUN apt-get update \ && apt-get install -y unzip curl ca-certificates \
#    && apt-get install -y unzip curl \
#    && mkdir -p /vsdbg \
#    && curl -sSL https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/linux-x64/vsdbg.tar.gz \
#    | tar -xz -C /vsdbg

# Install debugger (vsdbg) for VS Code remote debugging
#RUN apt-get update \
#    && apt-get install -y unzip curl ca-certificates \
#    && mkdir -p /vsdbg \
#    && curl -L -v https://aka.ms/vsdbg/download/dotnet/vsdbg/latest/vsdbg-linux-x64.tar.gz \
#       -o /tmp/vsdbg.tar.gz \
#    && tar -xzf /tmp/vsdbg.tar.gz -C /vsdbg \
#    && rm /tmp/vsdbg.tar.gz

RUN apt-get update && apt-get install -y curl unzip ca-certificates \
    && mkdir -p /vsdbg \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg



EXPOSE 80
ENTRYPOINT ["dotnet", "server.dll"]
