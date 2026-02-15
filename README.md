# OPML Feed Provider for Microsoft Widgets

A feed provider implementation for Microsoft Widgets that displays feeds from an OPML file.

## Overview

This application implements a feed provider that integrates with Microsoft Widgets on Windows 11, allowing you to display feeds from an OPML file directly in the Windows Widgets board.

## Features

- Displays feeds from OPML file in Microsoft Widgets
- Supports standard OPML format
- Easy feed management through OPML file editing
- No authentication required

## Prerequisites

- Windows 11 (version 22H2 or later)
- .NET 8.0 SDK
- Visual Studio 2022 (recommended) or .NET CLI
- Developer mode enabled on Windows

## Setup

### 1. Enable Developer Mode

1. Open Windows Settings
2. Go to Privacy & Security > For developers
3. Enable Developer Mode

### 2. Configure Your Feeds

Edit the `feedls.opml` file to add or modify your feed subscriptions. The file uses the standard OPML format:

```xml
<opml version="1.0">
    <head>
        <title>My Subscriptions</title>
    </head>
    <body>
        <outline text="Category Name" title="Category Name">
            <outline type="rss" text="Feed Title" title="Feed Title" 
                     xmlUrl="https://example.com/feed.xml" 
                     htmlUrl="https://example.com"/>
        </outline>
    </body>
</opml>
```

### 3. Build the Application

Using .NET CLI:
```bash
dotnet build -c Release
```

Using Visual Studio:
- Open FeedlyFeedProvider.csproj
- Build > Build Solution

### 4. Register the Feed Provider

1. Build the application
2. Run the executable: `FeedlyFeedProvider.exe`
3. The feed provider will register itself with Windows
4. Open Windows Widgets board to see your feeds

## Usage

Once registered, the feed provider will:
- Automatically load feeds from the feedls.opml file
- Display them in the Windows Widgets board
- Reload feeds when the application restarts

To add or modify feeds:
1. Edit the feedls.opml file
2. Restart the feed provider application

## Configuration Options

The `config.json` file contains configuration for the feed provider:
- **OpmlFilePath**: Path to the OPML file (default: feedls.opml)

## Architecture

- **FeedProvider.cs**: Implements the IFeedProvider interface for Windows Widgets
- **OpmlParser.cs**: Parses OPML files and extracts feed information
- **Program.cs**: COM registration and application lifecycle management
- **feedls.opml**: OPML file containing feed subscriptions

## Troubleshooting

### Feed provider not appearing in Widgets
- Ensure developer mode is enabled
- Check that the application is running
- Verify the GUID matches in both Package.appxmanifest and FeedProvider.cs

### No feeds showing
- Verify the feedls.opml file exists in the application directory
- Check the OPML file format is valid
- Review console output for errors

### OPML file errors
- Ensure the OPML file is valid XML
- Check that feed entries have xmlUrl attributes
- Verify the file is in the same directory as the executable

## Development

### Project Structure
```
FeedlyFeedProvider/
├── FeedProvider.cs          # Feed provider implementation
├── OpmlParser.cs            # OPML file parser
├── Program.cs               # COM registration and main entry point
├── FeedlyFeedProvider.csproj # Project file
├── Package.appxmanifest     # Windows app manifest
├── config.json              # Configuration file
├── feedls.opml              # OPML feed subscriptions
└── README.md                # This file
```

### Technologies Used
- .NET 8.0
- Windows App SDK
- COM Interop
- OPML (Outline Processor Markup Language)

## License

MIT License - See LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Resources

- [Microsoft Widgets Documentation](https://learn.microsoft.com/en-us/windows/apps/develop/widgets/)
- [OPML Specification](http://opml.org/spec2.opml)
- [Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/)
