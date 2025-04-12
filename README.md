# CloneSession

CloneSession is a simple ASP.NET Core side project built to learn and experiment with session management. This project demonstrates the basic setup and usage of session state in an ASP.NET Core application. It serves as an educational resource to understand how sessions operate—storing, retrieving, and managing user-specific data during a web request.

## Table of Contents

- Overview
- Features
- Technologies
- Getting Started
- Project Structure
- Usage
- References
- License

## Overview

CloneSession focuses on exploring session state within the ASP.NET Core framework. The sample demonstrates how to configure session middleware, manage session lifetime, and interact with session data from controllers. This project is especially useful for developers new to session management in ASP.NET Core.

## Features

- **Session Initialization:** Configures the session services and middleware.
- **Data Storage & Retrieval:** Implements examples that illustrate how to set and retrieve data from a session.
- **Hands-on Learning:** Provides a minimalist environment to experiment with and understand session behavior in ASP.NET Core.

## Technologies

- **ASP.NET Core:** The primary framework used to build the application.
- **.NET SDK:** Ensure you have .NET 5.0 or later installed (depending on your project target).
- **C#:** The programming language utilized throughout the project.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or newer)
- An IDE or editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

### Steps to Run

1. **Clone the Repository:**
```bash
git clone https://github.com/ThanhTNV/CloneSession.git
cd CloneSession
```
2. **Restore Dependencies & Build the Solution:**
```bash
dotnet restore
dotnet build
```
3. **Run the Application:**
```bash
dotnet run --project CloneSession
```
4. **Test Session Functionality:**
Open your browser and navigate to ```http://localhost:<port>``` (the port is displayed in your console output). Use the provided endpoints to test session initialization, data setting, and data retrieval.

## Project Structure
A high-level overview of the repository structure:
```
CloneSession/
├── .gitignore
├── CloneSession.sln
└── CloneSession/
    ├── Controllers/        # Contains controllers demonstrating session operations
    ├── Models/             # (Optional) Data models related to session data
    ├── Views/              # Minimal UI for testing session features
    ├── Program.cs          # (or Startup.cs) Contains application configuration, including session setup
    └── appsettings.json    # Application configuration settings

```
*Note: The exact folder organization may vary depending on your ASP.NET Core template and modifications you make along the way.*
## Usage
### Configuring Session
In the application configuration (e.g., in ```Program.cs``` or ```Startup.cs```), session services are added similar to:
```Csharp
builder.Services.AddSingleton<IMySessionStorageEngine>(services =>
{
    var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
    Directory.CreateDirectory(path);
    return new FileMySessionStorageEngine(path);
});
builder.Services.AddSingleton<IMySessionStorage, MySessionStorage>();
```
### Using Session in Controllers
A simple example in a controller might look like:
```Csharp
public async Task<IActionResult> Index()
{
    var session = HttpContext.GetSession();
    session.SetString("Name", "Thanh");
    await session.CommitAsync();

    return View();
}

public async Task<IActionResult> Privacy()
{
    var session = HttpContext.GetSession();
    await session.LoadAsync();
    var name = session.GetString("Name");
    return View("Privacy", name);
}
```
These code snippets help showcase how to set values into the session and later retrieve them during subsequent requests.

## References
This project was inspired by the following resources:

YouTube Tutorial: [Session Management in ASP.NET Core](https://www.youtube.com/watch?v=gc4bmv2i2iE&list=PLRLJQuuRRcFnwlQxGeVSVv-z_5tFwAh0j&index=35&t=385s)

GitHub Example: [Let's Learn ASP.NET - MySession](https://github.com/daohainam/lets-learn-aspnet/tree/76108a5f4ac07c35afe5372e9ddf314f3e9171ba/Projects/MySession)

## License
This project is intended for educational purposes. Feel free to modify and use the code as needed. (Add your preferred license here if applicable.)
