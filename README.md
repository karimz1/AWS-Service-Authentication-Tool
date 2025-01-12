# awsat (AWS Service Authentication Tool)

## Overview

Welcome to the **AWS Service Authentication Tool**, a cross-platform CLI tool for managing AWS CodeArtifact and ECR authentication tokens. This tool simplifies the process of logging into AWS services and managing authentication credentials.

### Features

- Authenticate AWS CodeArtifact repositories.
- Authenticate Docker with AWS ECR.
- Cross-platform compatibility (Windows, macOS, and Linux).
- Detailed logging for auditing and debugging.

------

## Installation

To install the AWS Service Authentication Tool as a global .NET tool, run:

```bash
dotnet tool install --global awsat
```
------

## Usage

Run the tool with the desired command:

```bash
awsat --command nuget --region us-east-1 --logFolderPath ./logs
```

### Available Commands

- **`nuget`**: Refreshes authentication tokens for AWS CodeArtifact.
- **`ecr`**: Refreshes authentication tokens for AWS ECR.

### Command-Line Options

| Option                | Description                             | Required |
| --------------------- | --------------------------------------- | -------- |
| `-c, --command`       | Command to execute (`nuget` or `ecr`).  | Yes      |
| `-r, --region`        | AWS region to use (e.g., `us-east-1`).  | Yes      |
| `-l, --logFolderPath` | Path to the log file.                   | Yes      |
| `-d, --debugMode`     | Enable debug mode for detailed logging. | No       |

### Example Usage

1. **Authenticate AWS CodeArtifact**:

   ```bash
   awsat --command nuget --region us-east-1 --logFolderPath ./logs
   ```

2. **Authenticate AWS ECR**:

   

   ```bash
   awsat --command ecr --region us-east-1 --logFolderPath ./logs
   ```

------

## Automating Authentication

To automate token refreshing, integrate this tool with your system's task scheduler.

### Cron Job Example (Linux/macOS)

1. Open your crontab configuration:

   ```bash
   crontab -e
   ```

2. Add the following line to refresh tokens at startup:

   ```bash
   @reboot awsat --command nuget --region us-east-1 --logFolderPath /path/to/logs
   ```

### Task Scheduler Example (Windows)

1. Open **Task Scheduler** and create a new task.

2. Set the trigger to run at login.

3. Set the action to execute:

   ```powershell
   awsat --command nuget --region us-east-1 --logFolderPath C:\path\to\logs
   ```

------

## Configuration

### Environment Prerequisites

- **AWS CLI**: Installed and configured with valid credentials. Refer to the [AWS CLI Configuration Guide](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html).
- **Docker**: Installed for ECR authentication. Refer to Docker Installation Guide.

------

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/karimz1/AWS-Service-Authentication-Tool/blob/main/LICENCE) file for details.

------

## Contributing

We welcome contributions! Feel free to open an issue or submit a pull request to improve the tool.

------

### Additional Resources

- [Releases based on Source Code](https://github.com/karimz1/AWS-Service-Authentication-Tool/releases)
- NuGet Package - 💕Link is Comming Soon 💕

------

## Development

Detailed build and development instructions are now in the [DEVELOPMENT.md](./DEVELOPMENT.md) file.