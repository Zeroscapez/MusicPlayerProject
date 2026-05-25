using Microsoft.AspNetCore.Mvc;

namespace MusicPlayer.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _player;
        private readonly RoomService _room;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext context => _contextAccessor.HttpContext!;

        // ASP.NET Core injects PlayerService automatically (we registered it in Program.cs)
        public PlayerController(PlayerService player, RoomService room, IHttpContextAccessor contextAccessor)
        {
            _player = player;
            _room = room;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("nowplaying")]
        public ActionResult<NowPlayingInfo> NowPlaying()
        {
            if (_player.GetNowPlaying == null)
                return StatusCode(503, "Player not ready");

            return Ok(_player.GetNowPlaying());
        }

        [HttpPost("play")]
        public IActionResult Play()
        {
            _player.Play?.Invoke();
            return Ok();
        }

        [HttpPost("pause")]
        public IActionResult Pause()
        {
            _player.Pause?.Invoke();
            return Ok();
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            _player.Stop?.Invoke();
            return Ok();
        }

        [HttpPost("next")]
        public IActionResult Next()
        {
            _player.Next?.Invoke();
            return Ok();
        }

        [HttpPost("previous")]
        public IActionResult Previous()
        {
            _player.Previous?.Invoke();
            return Ok();
        }

        [HttpPost("volume")]
        public IActionResult SetVolume([FromBody] int volume)
        {
            if (volume < 0 || volume > 100)
                return BadRequest("Volume must be between 0 and 100");

            _player.SetVolume?.Invoke(volume);
            return Ok();
        }


        [HttpGet("playlist")]
        public ActionResult<List<string>> Playlist()
        {
            if (_player.GetPlaylist == null)
                return StatusCode(503, "Player not ready");

            return Ok(_player.GetPlaylist());
        }

        //Endpoint to get the current queue (playlist + room queue)
        [HttpGet("queue")]
        public ActionResult<IReadOnlyList<QueuedTrack>> GetQueue()
        {
            return Ok(_room.Queue);
        }

        // Host and Guest can add to the queue
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "No file uploaded" });
            }

            //Check if an audio file
            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowed = new[] { ".mp3", ".wav", ".flac", ".aac", ".ogg" };
            if (!allowed.Contains(ext))
            {
                return BadRequest(new { error = "Invalid file type" });
            }

            //Check file size limit is 50mb
            if (file.Length > 50 * 1024 * 1024)
            {
                return BadRequest(new { error = "File too large (max 50mb)" });
            }

            //Save to upload folder
            var uploadDir = Path.Combine(AppContext.BaseDirectory, "uploads");
            Directory.CreateDirectory(uploadDir);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var isHost = context.Items["IsHost"] is true;
            var uploadedBy = isHost ? "Host" : "Guest";

            var queued = new QueuedTrack
            {
                FilePath = filePath,
                DisplayName = Path.GetFileNameWithoutExtension(file.FileName),
                UploadedBy = uploadedBy
            };

            _room.AddToQueue(queued);
            _player.AddToPlaylist?.Invoke(filePath);

            return Ok(new { message = "Track added", track = queued.DisplayName });
        }

        // Host only endpoint to remove from queue by index
        [HttpDelete("queue/{index}")]
        public IActionResult RemoveFromQueue(int index)
        {
            if (context.Items["IsHost"] is not true)
            {
                return StatusCode(403, new { error = "Host only" });
            }

            if (!_room.RemoveFromQueue(index))
            {
                return BadRequest(new { error = "Invalid index" });
            }

            _player.RemoveFromPlaylist?.Invoke(index);
            return Ok(new { message = "Track removed from queue" });
        }

        // Host only endpoint to remove from playlist by index (for tracks that are already in the player's playlist, not just the room queue)
        [HttpDelete("playlist/{index}")]
        public IActionResult RemoveFromPlaylist(int index)
        {
            if (context.Items["IsHost"] is not true)
            {
                return StatusCode(403, new { error = "Host only" });
            }


            _player.RemoveFromPlaylist?.Invoke(index);

            return Ok(new { message = "Track removed from playlist" });
        }

    }

}