# Feedly Feed Provider for Microsoft Widgets

A feed provider implementation for Microsoft Widgets that displays feeds from Feedly.

## Overview

This application implements a feed provider that integrates with Microsoft Widgets on Windows 11, allowing you to display your Feedly feeds directly in the Windows Widgets board.

## Features

- Displays Feedly feeds in Microsoft Widgets
- Supports OAuth authentication with Feedly API
- Configurable feed selection
- Real-time feed updates

## Prerequisites

- Windows 11 (version 22H2 or later)
- .NET 8.0 SDK
- Visual Studio 2022 (recommended) or .NET CLI
- Developer mode enabled on Windows
- Feedly account with API access

## Setup

### 1. Enable Developer Mode

1. Open Windows Settings
2. Go to Privacy & Security > For developers
3. Enable Developer Mode

### 2. Get Feedly API Access

1. Sign up for a Feedly account at https://feedly.com
2. Register for API access at https://developers.feedly.com
3. Obtain your access token

### 3. Configure the Feed Provider

Edit the `config.json` file and add your Feedly access token:

```json
{
  "Feedly": {
    "AccessToken": "your-feedly-access-token-here",
    "StreamId": "user/YOUR_USER_ID/category/global.all",
    "FeedCount": 10
  }
}
```

### 4. Build the Application

Using .NET CLI:
```bash
dotnet build -c Release
```

Using Visual Studio:
- Open FeedlyFeedProvider.csproj
- Build > Build Solution

### 5. Register the Feed Provider

1. Build the application
2. Run the executable: `FeedlyFeedProvider.exe`
3. The feed provider will register itself with Windows
4. Open Windows Widgets board to see your Feedly feeds

## Usage

Once registered, the feed provider will:
- Automatically fetch feeds from your Feedly account
- Display them in the Windows Widgets board
- Update periodically with new content

## Configuration Options

- **AccessToken**: Your Feedly API access token
- **StreamId**: The Feedly stream to display (e.g., all items, specific category)
- **FeedCount**: Number of feed items to display (default: 10)

## Architecture

- **FeedProvider.cs**: Implements the IFeedProvider interface for Windows Widgets
- **FeedlyClient.cs**: Handles communication with Feedly API
- **Program.cs**: COM registration and application lifecycle management

## Troubleshooting

### Feed provider not appearing in Widgets
- Ensure developer mode is enabled
- Check that the application is running
- Verify the GUID matches in both Package.appxmanifest and FeedProvider.cs

### No feeds showing
- Verify your Feedly access token is correct
- Check the StreamId is valid for your account
- Review console output for errors

### Authentication errors
- Regenerate your Feedly access token
- Ensure your Feedly account has API access enabled

## Development

### Project Structure
```
FeedlyFeedProvider/
├── FeedProvider.cs          # Feed provider implementation
├── FeedlyClient.cs          # Feedly API client
├── Program.cs               # COM registration and main entry point
├── FeedlyFeedProvider.csproj # Project file
├── Package.appxmanifest     # Windows app manifest
├── config.json              # Configuration file
└── README.md                # This file
```

### Technologies Used
- .NET 8.0
- Windows App SDK
- Feedly API
- COM Interop

## License

MIT License - See LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Resources

- [Microsoft Widgets Documentation](https://learn.microsoft.com/en-us/windows/apps/develop/widgets/)
- [Feedly API Documentation](https://developers.feedly.com/)
- [Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/)