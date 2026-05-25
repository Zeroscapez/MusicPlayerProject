using Microsoft.AspNetCore.Mvc;

namespace MusicPlayer.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _player;

        // ASP.NET Core injects PlayerService automatically (we registered it in Program.cs)
        public PlayerController(PlayerService player)
        {
            _player = player;
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

    }

}