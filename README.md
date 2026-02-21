# 🎵 MusicPlayer

A lightweight Windows desktop music player built with **.NET WinForms**, powered by **NAudio** for audio playback and **TagLib#** for metadata and album art extraction.

This project demonstrates clean architecture, managed audio handling (no COM interop), proper resource disposal, and event-driven playback control in a desktop application.

---

## 📖 Project Overview

MusicPlayer is a local audio playback application designed to provide a simple yet polished listening experience while showcasing:

- Modern .NET desktop development
- Audio handling with NAudio
- Metadata extraction with TagLib#
- Clean event-driven architecture
- Proper memory and resource management

The application automatically advances through a playlist, supports seeking, and displays embedded album artwork.

---

## ✨ Features

- 🎧 Play / Pause / Stop controls  
- ⏭ Previous / Next track navigation  
- 🔁 Automatic playback of the next track  
- 📂 Multi-file playlist support  
- 🖼 Album artwork extraction (with fallback image)  
- ⏱ Real-time progress tracking  
- 🎚 Volume control  
- 🖱 Click-to-seek progress bar  

---

## 🏗 Technical Design

### Audio Engine — NAudio
NAudio is used instead of Windows Media Player to:

- Avoid COM interop dependencies
- Maintain a fully managed .NET codebase
- Ensure reliable builds
- Provide precise playback control

Playback progression is handled through the `PlaybackStopped` event with end-of-track detection logic to safely auto-advance the playlist.

### Metadata — TagLib#
TagLib# is used to extract:

- Embedded album artwork
- Track metadata

Album art is safely loaded and disposed to prevent memory leaks.

---

## 🛠 Technologies Used

- .NET 6+  
- Windows Forms  
- NAudio  
- TagLib#  

---

## 📦 System Requirements

- Windows 10 or later  
- .NET 6.0 SDK or newer  

Verify your installed SDK:

```bash
dotnet --version
```

## 🔧 Building the Project

### Clone the Repository
```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git
cd YOUR_REPOSITORY
```

### Restore Dependencies
```bash
dotnet restore
```

### Build the Application
```bash
dotnet build
```

---

📁 Supported Audio Formats

Supported formats depend on installed decoders, commonly including:

- MP3

- WAV

- FLAC

- AAC

---

🧠 Implementation Highlights

- Safe detection of track completion before advancing playlist

- Proper disposal of WaveOutEvent and AudioFileReader instances

- Memory-safe album artwork loading

- Separation of playback logic from UI event handling

- Owner-drawn ListBox for improved text rendering

---

🚀 Future Enhancements

- Shuffle mode

- Repeat mode

- Drag-and-drop support

- Playlist persistence

- A TON OF UI CHANGES
