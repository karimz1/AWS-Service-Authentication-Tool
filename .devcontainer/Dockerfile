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

# Install Oh My Zsh for vscode user
RUN wget https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh -O install.sh \
    && ZSH="/home/vscode/.oh-my-zsh" \
    && RUNZSH=no sh install.sh \
    && chsh -s /bin/zsh vscode \
    && rm install.sh

# Set up useful plugins and themes for Oh My Zsh
RUN git clone https://github.com/zsh-users/zsh-autosuggestions /home/vscode/.oh-my-zsh/custom/plugins/zsh-autosuggestions \
    && git clone https://github.com/zsh-users/zsh-syntax-highlighting /home/vscode/.oh-my-zsh/custom/plugins/zsh-syntax-highlighting \
    && sed -i 's/plugins=(git)/plugins=(git zsh-autosuggestions zsh-syntax-highlighting)/' /home/vscode/.zshrc \
    && sed -i 's/ZSH_THEME=".*"/ZSH_THEME="agnoster"/' /home/vscode/.zshrc \
    && echo "source /home/vscode/.oh-my-zsh/custom/plugins/zsh-syntax-highlighting/zsh-syntax-highlighting.zsh" >> /home/vscode/.zshrc \
    && echo "source /home/vscode/.oh-my-zsh/custom/plugins/zsh-autosuggestions/zsh-autosuggestions.zsh" >> /home/vscode/.zshrc

# Ensure correct ownership of the Oh My Zsh setup
RUN chown -R vscode:vscode /home/vscode

# Set the working directory
WORKDIR /workspace

# Set the default shell to Zsh for the vscode user
USER vscode
SHELL ["/bin/zsh", "-c"]
