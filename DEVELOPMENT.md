# Development Guide for AWS Service Authentication Tool

This document outlines the steps for building, testing, and contributing to the AWS Service Authentication Tool.

---

## Prerequisites

Ensure you have the following installed and configured:

- **.NET 8 SDK**: Download it from the [.NET website](https://dotnet.microsoft.com/download).
- **AWS CLI**: Installed and configured. Refer to the [AWS CLI Installation Guide](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html) and [Configuring the AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html).
- **Docker**: Installed for ECR authentication. Refer to [Docker Installation Guide](https://docs.docker.com/get-docker/).
- **Git**: To clone and manage the repository.

---

## Setting Up the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/karimz1/AWS-Service-Authentication-Tool.git
   cd AWS-Service-Authentication-Tool
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build --configuration Release
   ```

4. Run the tool locally:
   ```bash
   dotnet run --project ./src/AwsServiceAuthenticator.Cli/AwsServiceAuthenticator.Cli.csproj -- --command nuget --region us-east-1 --logFolderPath ./logs
   ```

---

## Testing the Project

Run all tests using the following command:

```bash
dotnet test
```

Ensure all tests pass before submitting changes.


---

## Releasing the Tool

This repository uses GitHub Actions to automate releases.

### Steps to Publish a New Release

1. Update the version in the `.csproj` file:
   ```xml
   <PropertyGroup>
       <Version>1.0.0</Version>
   </PropertyGroup>
   ```

2. Commit your changes:
   ```bash
   git add .
   git commit -m "Release v1.0.0"
   ```

3. Tag the release:
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```

4. GitHub Actions will automatically:
   - Build the project for all platforms.
   - Create a new GitHub release.
   - Publish the package to NuGet.org.

---

## Contributing

I welcome contributions! To get started:

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Make your changes and commit them:
   ```bash
   git add .
   git commit -m "Add feature: your-feature-name"
   ```

4. Push your branch to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```

5. Open a pull request on the main repository.

---

## Licensing

This project is licensed under the MIT License. For more details, see the [LICENSE](./LICENSE) file.

