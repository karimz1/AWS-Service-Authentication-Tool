# Base image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Install required packages
RUN apt-get update && apt-get install -y \
    git \
    curl \
    sudo \
    zsh \
    wget \
    unzip \
    fonts-powerline \
    && apt-get clean


# Install the AWS CLI v2
RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "/tmp/awscliv2.zip" \
    && unzip /tmp/awscliv2.zip -d /tmp \
    && /tmp/aws/install \
    && rm -rf /tmp/aws /tmp/awscliv2.zip

# Optionally, verify the installation by printing the version
RUN aws --version


# Create a user for VS Code (to avoid running as root)
RUN useradd -m vscode \
    && usermod -aG sudo vscode \
    && echo "vscode ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers

# Set the working directory
WORKDIR /workspace

# Set the default shell to Zsh for the vscode user
USER vscode
SHELL ["/bin/zsh", "-c"]
