namespace MusicPlayer
{
    public class QueuedTrack
    {
        public string FilePath { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string UploadedBy { get; set; } = "";
    }

    public class RoomService
    {
        private readonly string _roomCode;
        private readonly HashSet<string> _guestTokens = new();
        private readonly List<QueuedTrack> _queue = new();
        private readonly Random _random = new();

        public string RoomCode => _roomCode;
        public IReadOnlyList<QueuedTrack> Queue => _queue.AsReadOnly();

        public RoomService()
        {
            // Generate a random 6 character room code on startup
            _roomCode = GenerateCode();
        }

        private string GenerateCode()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, 6)
                .Select(_ => chars[_random.Next(chars.Length)])
                .ToArray());
        }

        public bool ValidateRoomCode(string code)
        {
            return string.Equals(code.Trim(), _roomCode, StringComparison.OrdinalIgnoreCase);
        }

        public string CreateGuestToken()
        {
            var token = Guid.NewGuid().ToString("N");
            _guestTokens.Add(token);
            return token;
        }

        public bool ValidateGuestToken(string token)
        {
            return _guestTokens.Contains(token);
        }

        public void AddToQueue(QueuedTrack track) => _queue.Add(track);

        public bool RemoveFromQueue(int index)
        {
            if (index < 0 || index >= _queue.Count) return false;
            _queue.RemoveAt(index);
            return true;
        }



    }



}