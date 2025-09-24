# 🎮 Gaming Backlog Web App

A modern web application built with ASP.NET Core 9.0 to help gamers manage their gaming backlog, track progress, and discover new games to play.

![Gaming Backlog App](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue?style=flat-square&logo=.net)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-orange?style=flat-square)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-purple?style=flat-square&logo=bootstrap)
![C#](https://img.shields.io/badge/C%23-12.0-green?style=flat-square&logo=csharp)

## ✨ Features

### 🎯 Core Functionality
- **Game Management**: Add, edit, and delete games from your backlog
- **Status Tracking**: Track games as Wishlist, Playing, Completed, On Hold, or Dropped
- **Rating System**: Rate completed games on a 1-10 scale with visual star display
- **Platform Support**: Organize games by platform (PC, PlayStation, Xbox, Nintendo Switch, etc.)
- **Progress Tracking**: Automatic date tracking for when games are added and completed

### 🔍 Search & Filter
- **Smart Search**: Search games by title or description
- **Advanced Filtering**: Filter by status, platform, and rating
- **Flexible Sorting**: Sort by title, platform, status, rating, or date added
- **Real-time Results**: Instant filtering and sorting without page reloads

### 🌐 External Integration
- **Game Database**: Mock IGDB-style API for game information
- **Auto-populate**: Automatically fill game details from database search
- **Rich Metadata**: Cover images, descriptions, genres, and release dates
- **Visual Search**: Image-rich search results for easy game identification

### 📊 Dashboard & Analytics
- **Statistics Overview**: View total games, wishlist count, currently playing, and completed
- **Recent Activity**: See recently added and completed games
- **Visual Progress**: Progress tracking with status badges and completion stats

## 🚀 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd GamingBacklogWebApp
   ```

2. **Navigate to the project directory**
   ```bash
   cd GamingBacklogWebApp
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Open your browser**
   Navigate to `http://localhost:5097` to start using the app!

## 🎮 How to Use

### Adding Games
1. Click **"Add Game"** in the navigation or on the home page
2. **Search the database** for existing games or **add manually**
3. Select a game from search results to auto-populate details
4. Fill in additional information like platform and status
5. Save to add to your backlog

### Managing Your Collection
- **View All Games**: Navigate to "My Games" to see your entire collection
- **Filter Games**: Use the filter bar to find specific games by status, platform, or search term
- **Edit Games**: Click "Edit" on any game card to modify details
- **Update Status**: Change game status as you progress through your backlog
- **Rate Games**: Add ratings to completed games for future reference

### Tracking Progress
- **Dashboard Stats**: View your gaming statistics on the home page
- **Status Management**: Move games through different stages of your backlog
- **Completion Tracking**: Automatic date tracking when games are marked as completed

## 🏗️ Project Structure

```
GamingBacklogWebApp/
├── Data/
│   └── GamingBacklogContext.cs      # Entity Framework context
├── Models/
│   └── Game.cs                      # Game entity model
├── Pages/
│   ├── Games.cshtml(.cs)            # Main games list page
│   ├── AddGame.cshtml(.cs)          # Add new game page
│   ├── EditGame.cshtml(.cs)         # Edit existing game page
│   ├── Index.cshtml(.cs)            # Home page with dashboard
│   └── Shared/
│       └── _Layout.cshtml           # Shared layout template
├── Services/
│   └── GameDataService.cs           # Mock API service for game data
├── wwwroot/                         # Static files (CSS, JS, images)
└── Program.cs                       # Application startup configuration
```

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 9.0 with Razor Pages
- **Database**: Entity Framework Core with In-Memory Database
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript
- **Icons**: Font Awesome 6.0
- **Development**: C# 12.0, .NET 9.0

## 📦 Dependencies

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.0" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
```

## 🎨 Screenshots & Demo

### Home Dashboard
The home page provides an overview of your gaming statistics and quick access to main features.

### Games Collection
Browse your entire game collection with advanced filtering and sorting options.

### Add Game Interface
Search for games in the database or add them manually with rich metadata support.

## 🔧 Configuration

### Database Configuration
The application currently uses an in-memory database for demonstration purposes. To use a persistent database:

1. Update `Program.cs` to use SQL Server:
   ```csharp
   builder.Services.AddDbContext<GamingBacklogContext>(options =>
       options.UseSqlServer(connectionString));
   ```

2. Add connection string to `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GamingBacklogDb;Trusted_Connection=true;"
     }
   }
   ```

3. Run Entity Framework migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### External API Integration
To integrate with a real game database API (like IGDB):

1. Replace `MockGameDataService` with a real API implementation
2. Add API credentials to configuration
3. Implement proper error handling and rate limiting

## 🚀 Deployment

### Local Development
```bash
dotnet run --environment Development
```

### Production Build
```bash
dotnet publish -c Release -o ./publish
```

### Docker Support
Create a `Dockerfile` for containerized deployment:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["GamingBacklogWebApp.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GamingBacklogWebApp.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📋 Future Enhancements

- [ ] **Real IGDB API Integration** - Connect to actual game database
- [ ] **User Authentication** - Multi-user support with individual backlogs
- [ ] **Achievement Tracking** - Track game achievements and trophies
- [ ] **Play Time Tracking** - Log hours spent playing games
- [ ] **Wishlist Price Tracking** - Monitor price changes for wishlist games
- [ ] **Social Features** - Share completed games and reviews
- [ ] **Mobile App** - Native mobile applications
- [ ] **Export/Import** - Backup and restore game collections
- [ ] **Advanced Analytics** - Detailed gaming statistics and trends
- [ ] **Game Recommendations** - AI-powered game suggestions

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

Built with ❤️ for gamers who love organizing their gaming adventures!

---

## 🆘 Troubleshooting

### Common Issues

**Build Errors with CreateAppHost**
If you encounter file locking issues during build, add this to your `.csproj`:
```xml
<UseAppHost>false</UseAppHost>
```

**Port Already in Use**
If port 5097 is busy, the app will automatically select another port. Check the console output for the correct URL.

**Missing Dependencies**
Ensure you have .NET 9.0 SDK installed and run `dotnet restore` to install all packages.

### Getting Help
- Check the [Issues](../../issues) page for common problems
- Create a new issue if you encounter bugs
- Refer to [ASP.NET Core documentation](https://docs.microsoft.com/aspnet/core/) for framework-specific questions

---

*Happy Gaming! 🎮*