FROM ubuntu:24.04 AS base

RUN apt-get update && apt-get install -y --no-install-recommends \
    libsm6 libxext6 libxrender1 ffmpeg \
    libgeotiff-dev libtiff-dev libjpeg-dev libpng-dev \
    libgtk-3-dev libavcodec-dev libavformat-dev libavutil-dev libswscale-dev \
    liblapack-dev libblas-dev libstdc++6 \
    libgstreamer1.0-0 libgstreamer-plugins-base1.0-0 libgstreamer-plugins-good1.0-0 \
    libgstreamer-plugins-bad1.0-0 libgstreamer-plugins-base1.0-dev \
    libhdf5-dev libvtk9.1 \
    && rm -rf /var/lib/apt/lists/*

RUN apt-get update && apt-get install -y wget \
    && wget https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x dotnet-install.sh \
    && ./dotnet-install.sh --version 8.0.0 --runtime dotnet \
    && ln -s /root/.dotnet/dotnet /usr/bin/dotnet

WORKDIR /app

RUN ldd --version

ENV LD_LIBRARY_PATH=/app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "VideoQrCodeDetector.sln"
RUN dotnet build "AnaliseVideoWorker/AnaliseVideoWorker.csproj" -c Release -o /app/build
RUN dotnet publish "AnaliseVideoWorker/AnaliseVideoWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "AnaliseVideoWorker.dll"]
