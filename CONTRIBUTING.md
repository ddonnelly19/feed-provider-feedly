# Contributing to OPML Feed Provider

Thank you for your interest in contributing to the OPML Feed Provider for Microsoft Widgets!

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- A clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Your Windows version and .NET SDK version

### Suggesting Features

Feature requests are welcome! Please:
- Check if the feature has already been requested
- Provide a clear use case
- Explain how it would benefit users

### Code Contributions

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes**
   - Follow the existing code style
   - Add comments for complex logic
   - Update documentation as needed
4. **Test your changes**
   - Build the project
   - Test on Windows 11
   - Verify the feed provider works with Microsoft Widgets
5. **Commit your changes**
   ```bash
   git commit -m "Description of your changes"
   ```
6. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```
7. **Create a Pull Request**

## Development Setup

### Prerequisites
- Windows 11 (22H2 or later)
- Visual Studio 2022 or .NET 8.0 SDK
- Developer mode enabled

### Building
```bash
dotnet build -c Release
```

### Running
```bash
dotnet run
```

## Code Style

- Use meaningful variable and method names
- Follow C# naming conventions
- Add XML documentation comments for public methods
- Keep methods focused and concise

## Testing

When making changes:
1. Build the project successfully
2. Run the feed provider
3. Verify it appears in Windows Widgets
4. Check that feeds load correctly from the OPML file
5. Test with different OPML file configurations

## Questions?

Feel free to open an issue for any questions or concerns!
