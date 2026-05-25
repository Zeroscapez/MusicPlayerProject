<img width="1100" height="448" alt="image" src="https://github.com/user-attachments/assets/56f90105-623a-49f2-a394-819912cd91cd" />


# 🎵 MusicPlayer

A Windows desktop music player built with WinForms and NAudio, with an embedded ASP.NET Core Web API for remote control from any device on your network.

## Features

### Desktop App
- Play MP3, WAV, FLAC, AAC, and OGG files
- Load individual files or entire folders
- Playlist management with shuffle support
- Album art display via ID3 tags
- Progress bar with click-to-seek
- Volume control

### Web API
- Control playback remotely from any browser or HTTP client
- Room system — share a room code with guests so they can upload songs
- Host and guest roles with separate permissions
- Request logging middleware
- API key authentication

### Phone Remote
- Open the remote page on your phone from any browser on the same network
- Now playing info with live updates
- Play, pause, skip, and volume control (host only)
- Upload songs directly from your phone

---

## Tech Stack

- **Frontend** — WinForms (.NET 10)
- **Audio** — NAudio
- **Tag reading** — TagLib#
- **Web API** — ASP.NET Core (Kestrel, embedded)
- **API Docs** — Swagger / Swashbuckle

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- Windows (WinForms requirement)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Zeroscapez/SingWithFriends.git
   cd MusicPlayer
   ```

2. Copy the example settings file and add your API key:
   ```bash
   cp appsettings.example.json appsettings.json
   ```

3. Open `appsettings.json` and replace the placeholder:
   ```json
   {
     "ApiKey": "your-secret-key-here"
   }
   ```

4. Run the app:
   ```bash
   dotnet run
   ```

The desktop player will open and the API will start on `http://0.0.0.0:5000`.

---

## API Reference

All endpoints except `/api/room/join` require authentication via one of these headers:

| Header | Value | Role |
|--------|-------|------|
| `X-Api-Key` | Your key from `appsettings.json` | Host |
| `X-Guest-Token` | Token received from `/api/room/join` | Guest |

### Player

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/player/nowplaying` | Host / Guest | Current track info |
| GET | `/api/player/playlist` | Host / Guest | Full playlist |
| GET | `/api/player/queue` | Host / Guest | Uploaded song queue |
| POST | `/api/player/play` | Host only | Resume playback |
| POST | `/api/player/pause` | Host only | Pause playback |
| POST | `/api/player/stop` | Host only | Stop playback |
| POST | `/api/player/next` | Host only | Skip to next track |
| POST | `/api/player/previous` | Host only | Go to previous track |
| POST | `/api/player/volume` | Host only | Set volume (0–100, JSON body) |
| POST | `/api/player/upload` | Host / Guest | Upload an audio file |
| DELETE | `/api/player/playlist/{index}` | Host only | Remove track from playlist |
| DELETE | `/api/player/queue/{index}` | Host only | Remove track from queue |

### Room

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/room/code` | Host only | Get the current room code |
| POST | `/api/room/join` | None | Join with room code, returns guest token |

### Example Requests

```bash
# Now playing
curl http://localhost:5000/api/player/nowplaying -H "X-Api-Key: your-key"

# Play
curl -X POST http://localhost:5000/api/player/play -H "X-Api-Key: your-key"

# Set volume to 75
curl -X POST http://localhost:5000/api/player/volume \
  -H "X-Api-Key: your-key" \
  -H "Content-Type: application/json" \
  -d "75"

# Upload a song as a guest
curl -X POST http://localhost:5000/api/player/upload \
  -H "X-Guest-Token: your-guest-token" \
  -F "file=@song.mp3"
```

### Swagger UI

Full interactive API docs available at:
```
http://localhost:5000/swagger
```

---

## Phone Remote

1. Find your PC's local IP address:
   ```bash
   ipconfig
   ```
   Look for **IPv4 Address** under your WiFi adapter.

2. On your phone's browser, navigate to:
   ```
   http://192.168.x.x:5000
   ```
   > Make sure to use `http://` — mobile browsers may auto-upgrade to HTTPS.

3. Enter the room code (get it from `GET /api/room/code`) to join as a guest, or tap **I'm the host** and enter your API key for full control.

---

## Project Structure

```
MusicPlayer/
├── Controllers/
│   ├── PlayerController.cs   # Playback and upload endpoints
│   └── RoomController.cs     # Room join endpoint
├── wwwroot/
│   └── index.html            # Phone remote UI
├── logs/
│   └── requests.log          # Auto-generated request log
├── Form1.cs                  # Main WinForms form
├── PlayerService.cs          # Bridge between API and WinForms
├── RoomService.cs            # Room code and guest token management
├── ApiKeyMiddleware.cs       # Authentication middleware
├── LoggingMiddleware.cs      # Request logging middleware
├── Track.cs                  # Track model
├── Program.cs                # Entry point, Kestrel setup
├── appsettings.example.json  # Config template (commit this)
└── appsettings.json          # Real config with API key (do NOT commit)
```

---

## Security Notes

- `appsettings.json` is excluded from Git via `.gitignore` — never commit your API key
- The API key in `index.html` is visible to anyone who views the page source — this is acceptable for a local network tool but should not be exposed to the public internet
- The room code expires when the app closes — guests must rejoin each session
- File uploads are limited to 50MB and validated by extension (`.mp3`, `.wav`, `.flac`, `.aac`, `.ogg`)

---

## Supported Audio Formats

| Format | Extension |
|--------|-----------|
| MP3 | `.mp3` |
| WAV | `.wav` |
| FLAC | `.flac` |
| AAC | `.aac` |
| OGG | `.ogg` |