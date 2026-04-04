# Workleap Pingboard Who's Who AutoPlay

A Windows desktop app that automatically plays the Pingboard "Who's Who" game. It scrapes the employee directory to build a lookup of names and profile photos, then matches faces in the game to click the correct answers — hands-free.

## How It Works

1. Opens Pingboard in Chrome and waits for you to log in via Workleap
2. Navigates to the employee directory and scrapes every employee's name and profile image
3. Starts the "Who's Who" game in Everyone mode
4. For each round, matches the displayed face against the scraped data and clicks the correct name
5. Automatically advances through rounds until you stop it

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Windows)
- [Google Chrome](https://www.google.com/chrome/)
- A Workleap account with Pingboard access

## Installation

### From Release (recommended)

1. Download the latest release from the [Releases](https://github.com/NiftiestPixel/Workleap-Pingboard-Whos-Who-AutoPlay/releases) page
2. Extract the zip
3. Run `PingboardWhosWhoAutoPlay.exe`

### From Source

```bash
git clone https://github.com/NiftiestPixel/Workleap-Pingboard-Whos-Who-AutoPlay.git
cd Workleap-Pingboard-Whos-Who-AutoPlay
dotnet build
dotnet run
```

## How to Play

1. Launch the app — you'll see a small dark window with a **START PLAYING** button
2. Click **START PLAYING** — Chrome opens to the Workleap login page
3. Log in with your Workleap credentials — the app waits for you to finish
4. Sit back — the app automatically:
   - Navigates to the Pingboard directory
   - Scrolls through and memorizes every employee's face
   - Opens the Who's Who game
   - Answers each question by matching faces to names
   - Advances through rounds continuously
5. Click **STOP PLAYING** at any time to stop and close the browser

The log area in the app shows real-time progress — which employees are being identified and how many have been answered.

## Project Structure

| File | Purpose |
|---|---|
| `Program.cs` | Application entry point |
| `MainForm.cs` | WinForms UI — start/stop toggle and log display |
| `MainForm.Designer.cs` | Designer-generated layout |
| `Selenium.cs` | Chrome WebDriver initialization and cleanup |
| `WebActions.cs` | Pingboard automation — login, scraping, game play |
| `EmployeeModel.cs` | `Employee` record (name + image URL) |

## Tech Stack

- .NET 8 / Windows Forms
- Selenium WebDriver 4.30
- ChromeDriver
- C# 12

## Author

Created by [Niftiest](https://github.com/niftiest)
