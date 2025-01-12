# Base image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Create a user for VS Code (to avoid running as root)
RUN useradd -m vscode

# Set up the working directory
WORKDIR /workspace

# Set the user for the container
USER vscode

# Install additional tools (optional)
RUN apt-get update && apt-get install -y git curl && apt-get clean
