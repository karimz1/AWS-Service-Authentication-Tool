# Base image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

RUN apt-get update && apt-get install -y git curl sudo && apt-get clean

# Create a user for VS Code (to avoid running as root)
RUN useradd -m vscode
sudo usermod -aG sudo vscode

# Set up the working directory
WORKDIR /workspace

# Set the user for the container
USER vscode
