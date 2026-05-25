namespace MusicPlayer
{
    public class NowPlayingInfo
    {
        public string? TrackName { get; set; }
        public double CurrentSeconds { get; set; }
        public double TotalSeconds { get; set; }
        public bool IsPlaying { get; set; }
        public int Volume { get; set; }
    }

    public class PlayerService
    {
        public Func<NowPlayingInfo>? GetNowPlaying { get; set; }
        public Func<List<String>>? GetPlaylist { get; set; }
        public Action? Play { get; set; }
        public Action? Pause { get; set; }
        public Action? Stop { get; set; }
        public Action? Next { get; set; }
        public Action? Previous { get; set; }
        public Action<int>? SetVolume { get; set; }
    }
}