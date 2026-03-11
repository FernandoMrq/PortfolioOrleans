****# PortfolioOrleans

A .NET 10 ASP.NET Core Web API demonstrating **Microsoft Orleans**, a virtual actor framework for building distributed applications.

## Overview

Orleans provides a straightforward approach to building distributed systems by using the virtual actor model. Actors (called grains) are automatically instantiated, placed, and managed by the Orleans runtime. This project implements a URL shortener to showcase grains, silos, persistent state, and grain factories.

Key concepts:

- **Grains**: The core building blocks. Each grain has a unique identity and can hold state. Grains communicate via async method calls.
- **Silos**: Host processes that store and manage grains. A cluster is a group of silos that coordinate work.
- **Grain Factory**: Used to obtain references to grains by their identity.

## Project Structure

```
PortfolioOrleans/
├── PortfolioOrleans.sln
├── src/
│   └── PortfolioOrleans/
│       ├── Grains/
│       │   ├── IUrlShortenerGrain.cs
│       │   └── UrlShortenerGrain.cs
│       ├── Models/
│       │   └── UrlDetails.cs
│       └── Program.cs
└── tests/
    └── PortfolioOrleans.Tests/
        ├── ClusterFixture.cs
        └── Grains/
            └── UrlShortenerGrainTests.cs
```

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Commands

```bash
# Build (solution or project)
dotnet build
# or: dotnet build PortfolioOrleans.sln

# Run
dotnet run --project src/PortfolioOrleans/PortfolioOrleans.csproj

# Test
dotnet test
```

## Sample Usage

Once the app is running (default: http://localhost:5000 or https://localhost:5001):

**Shorten a URL:**
```bash
curl "http://localhost:5000/shorten?url=https://learn.microsoft.com"
```

**Redirect using shortened URL:**
```bash
curl -L "http://localhost:5000/go/ABC123"
```

**Home:**
```bash
curl http://localhost:5000/
```

## Endpoints

| Endpoint | Description |
|----------|-------------|
| GET / | Welcome message |
| GET /shorten?url={url} | Creates a shortened URL, returns the new URL |
| GET /go/{shortenedRouteSegment} | Redirects to the original URL |
