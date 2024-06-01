# AWS Service Authentication Tool

[![Build and Release](https://github.com/karimz1/AWS-Service-Authentication-Tool/actions/workflows/release.yml/badge.svg?event=release)](https://github.com/karimz1/AWS-Service-Authentication-Tool/actions/workflows/release.yml)

## Overview

Welcome to the AWS Authentication Tool repository! This project contains a .NET application designed to streamline the authentication process for AWS services like CodeArtifact and ECR. This tool helps you log into each service and update necessary tokens or credentials, with detailed logging for easy auditing and troubleshooting. This version is a more robust and versatile alternative to the PowerShell scripts previously provided.

## Features

- Authenticate and log into AWS CodeArtifact repositories.
- Authenticate Docker with AWS ECR.
- Cross-platform compatibility (Windows, macOS, and Linux).
- Built with .NET 8 for improved performance and reliability.
- Detailed logging with Serilog.

## Installation and Usage

### Prerequisites

- .NET 8 SDK installed. Download it from the [.NET website](https://dotnet.microsoft.com/download).
- AWS CLI installed and configured. For more information, refer to the [AWS CLI Installation Guide](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html) and [Configuring the AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html).
- Docker installed (for ECR authentication).
- Access to the AWS CodeArtifact and ECR services.
- Proper configuration of AWS credentials and region.

### Installation

1. Download the latest release from the [Releases](https://github.com/karimz1/AWS-Service-Authentication-Tool/releases) page.

2. Extract the release package to your desired location.

### Usage

Run the tool with the desired command and options:

```sh
dotnet AwsTokenRefresher.Cli.dll --command nuget --region us-east-1 --logPath ./logs
```

#### Available Commands

- `nuget`: Refreshes authentication tokens for AWS CodeArtifact.
- `ecr`: Refreshes authentication tokens for AWS ECR.
- `--command`: The command to execute (`nuget` or `ecr`).
- `--region`: The AWS region to use (e.g., `us-east-1`).
- `--logFolderPath`: The path to the log file.

### Automate with Cron Jobs or Task Scheduler 

To ensure that your development machine is always authenticated with the necessary services, you can add this tool to your cron jobs (Linux/macOS) or Task Scheduler (Windows). This way, you can automate the authentication process to run at startup or at regular intervals.

#### Cron Job Example (Linux/macOS)

1. Open your crontab configuration:

    ```sh
    crontab -e
    ```

2. Add the following line to run the tool at startup (adjust the path as needed):

    ```sh
    @reboot dotnet /path/to/AwsTokenRefresher.Cli.dll --command nuget --region us-east-1 --logPath /path/to/logs
    @reboot dotnet /path/to/AwsTokenRefresher.Cli.dll --command ecr --region us-east-1 --logPath /path/to/logs
    ```

#### Task Scheduler Example (Windows)

1. Open Task Scheduler and create a new task.
2. Set the trigger to run the task at login.
3. Set the action to start a program and use the following settings:

    ```sh
    dotnet C:\path\to\AwsTokenRefresher.Cli.dll --command nuget --region us-east-1 --logPath C:\path\to\logs
    ```

4. Repeat for the other command if needed.

### Configuration

The tool uses configuration variables that can be modified as needed:

- **REGION**: The AWS region where the service is hosted. Default is `us-east-1`.
- **LOG PATH**: The path to the log file.

### Example Configuration

You can set these variables through the command line options as shown in the usage section.

## Development

### Prerequisites

- .NET 8 SDK installed. Download it from the [.NET website](https://dotnet.microsoft.com/download).
- AWS CLI installed and configured.
- Docker installed (for ECR authentication).
- Proper configuration of AWS credentials and region.

### Setup

1. Clone the repository:

   ```sh
   git clone https://github.com/karimz1/aws-authentication-tool.git
   cd aws-authentication-tool
   ```

2. Build the project:

   ```sh
   dotnet build --configuration Release
   ```

### Running Locally

Run the tool with the desired command and options:

```sh
dotnet run --project ./src/AwsTokenRefresher.Cli/AwsTokenRefresher.Cli.csproj -- --command nuget --region us-east-1 --logPath ./logs
```

### Testing

To run the tests, use the following command:

```sh
dotnet test
```

### Building and Releasing

This repository is set up to use GitHub Actions for continuous integration and deployment. The workflow builds and releases the project for Windows, macOS, and Linux.

#### GitHub Actions Workflow

The GitHub Actions workflow is defined in `.github/workflows/release.yml`.

##### Trigger

The workflow is triggered on push to tags matching the pattern `v*.*.*` (e.g., `v1.0.0`).

##### Jobs

1. **Build**: Builds the project for Windows, macOS, and Linux.
2. **Create Release**: Creates a GitHub release and uploads the built artifacts.

To create a new release, push a tag to the repository:

```sh
git tag v1.0.0
git push origin v1.0.0
```

This will trigger the GitHub Actions workflow, which will build the project for all specified platforms and create a release with the built artifacts.

## PowerShell Version

For users who prefer PowerShell, there is a PowerShell version of these scripts available [here](https://github.com/karimz1/AWS-Authentication-Scripts). This version requires PowerShell Core and provides similar functionality for AWS authentication.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/karimz1/AWS-Service-Authentication-Tool/blob/main/LICENCE) file for details.

## Contributing

Contributions are very welcome! If you have suggestions, improvements, or bug fixes, please open an issue or submit a pull request. Let's make this project even better together!