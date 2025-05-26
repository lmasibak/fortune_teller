# Contributing to Daily Fortune Generator

Thank you for your interest in contributing to the Daily Fortune Generator! This document provides guidelines and information for contributors.

## 🤝 How to Contribute

### Reporting Issues
1. Check existing issues to avoid duplicates
2. Use the issue template when creating new issues
3. Provide detailed information including:
   - Steps to reproduce
   - Expected vs actual behavior
   - Environment details (OS, .NET version)
   - Screenshots if applicable

### Submitting Changes
1. Fork the repository
2. Create a feature branch from `main`
3. Make your changes following our coding standards
4. Add or update tests as needed
5. Update documentation if required
6. Submit a pull request

## 🏗️ Development Setup

### Prerequisites
- Visual Studio 2022 or Visual Studio Code
- .NET 6.0 SDK or later
- Git for version control

### Getting Started
1. Clone your fork:
   \`\`\`bash
   git clone https://github.com/yourusername/daily-fortune-generator.git
   cd daily-fortune-generator
   \`\`\`

2. Restore packages:
   \`\`\`bash
   dotnet restore
   \`\`\`

3. Build the solution:
   \`\`\`bash
   dotnet build
   \`\`\`

4. Run tests:
   \`\`\`bash
   dotnet test
   \`\`\`

## 📝 Coding Standards

### C# Guidelines
- Follow Microsoft C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and concise
- Use proper exception handling

### Code Style
- Use 4 spaces for indentation
- Place opening braces on new lines
- Use `var` when type is obvious
- Prefer explicit access modifiers
- Order class members: fields, properties, constructors, methods

### Example:
\`\`\`csharp
/// <summary>
/// Generates a fortune based on user input.
/// </summary>
/// <param name="input">User preferences and information.</param>
/// <returns>A personalized Fortune object.</returns>
public Fortune GenerateFortune(UserInput input)
{
    if (input == null)
        throw new ArgumentNullException(nameof(input));
    
    // Implementation here
}
\`\`\`

## 🧪 Testing Guidelines

### Unit Tests
- Write tests for all public methods
- Use descriptive test method names
- Follow Arrange-Act-Assert pattern
- Aim for high code coverage
- Test edge cases and error conditions

### Test Structure
\`\`\`csharp
[Fact]
public void GenerateFortune_WithValidInput_ReturnsFortuneWithCorrectUserName()
{
    // Arrange
    var userInput = new UserInput { Name = "John" };
    
    // Act
    var fortune = _fortuneEngine.GenerateFortune(userInput);
    
    // Assert
    Assert.Equal("John", fortune.UserName);
}
\`\`\`

## 📁 Project Structure

\`\`\`
DailyFortuneGenerator/
├── src/
│   └── DailyFortuneGenerator/
│       ├── Core/              # Business logic
│       ├── Forms/             # UI forms
│       ├── Models/            # Data models
│       └── Assets/            # Resources
├── tests/
│   └── DailyFortuneGenerator.Tests/
├── docs/                      # Documentation
├── build/                     # Build scripts
└── artifacts/                 # Build outputs
\`\`\`

## 🎯 Areas for Contribution

### High Priority
- Database integration (SQLite)
- Dark theme implementation
- Performance optimizations
- Additional fortune categories
- Localization support

### Medium Priority
- Social media sharing
- Custom fortune templates
- Advanced statistics
- Export formats (PDF, JSON)
- Plugin system

### Low Priority
- Web version
- Mobile app
- Cloud synchronization
- AI-powered fortunes

## 🔄 Pull Request Process

1. **Before Starting**
   - Check if an issue exists for your change
   - Discuss major changes in an issue first
   - Ensure your fork is up to date

2. **Making Changes**
   - Create a descriptive branch name
   - Make focused, atomic commits
   - Write clear commit messages
   - Keep changes small and reviewable

3. **Submitting PR**
   - Fill out the PR template completely
   - Link related issues
   - Ensure all tests pass
   - Update documentation as needed

4. **Review Process**
   - Address reviewer feedback promptly
   - Keep discussions constructive
   - Be open to suggestions
   - Update your branch as needed

## 📋 Commit Message Format

Use conventional commit format:
\`\`\`
type(scope): description

[optional body]

[optional footer]
\`\`\`

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes
- `refactor`: Code refactoring
- `test`: Test additions/changes
- `chore`: Maintenance tasks

Examples:
\`\`\`
feat(fortune): add new adventure category templates
fix(ui): resolve fortune display formatting issue
docs(readme): update installation instructions
\`\`\`

## 🐛 Bug Reports

Include the following information:
- **Environment**: OS, .NET version, app version
- **Steps to Reproduce**: Detailed steps
- **Expected Behavior**: What should happen
- **Actual Behavior**: What actually happens
- **Screenshots**: If applicable
- **Additional Context**: Any other relevant information

## 💡 Feature Requests

When suggesting new features:
- Explain the use case and benefits
- Provide mockups or examples if possible
- Consider implementation complexity
- Discuss potential alternatives
- Be open to feedback and iteration

## 📞 Getting Help

- **GitHub Issues**: For bugs and feature requests
- **GitHub Discussions**: For questions and general discussion
- **Code Review**: For implementation guidance
- **Documentation**: Check existing docs first

## 🏆 Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes for significant contributions
- Special thanks in documentation

## 📜 Code of Conduct

- Be respectful and inclusive
- Welcome newcomers and help them learn
- Focus on constructive feedback
- Respect different opinions and approaches
- Follow GitHub's community guidelines

Thank you for contributing to Daily Fortune Generator! 🔮✨
