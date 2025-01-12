# Base image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Install required packages
RUN apt-get update && apt-get install -y \
    git \
    curl \
    sudo \
    zsh \
    wget \
    fonts-powerline \
    && apt-get clean

# Create a user for VS Code (to avoid running as root)
RUN useradd -m vscode \
    && usermod -aG sudo vscode \
    && echo "vscode ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers

# Set the working directory
WORKDIR /workspace

# Set the default shell to Zsh for the vscode user
USER vscode
SHELL ["/bin/zsh", "-c"]
