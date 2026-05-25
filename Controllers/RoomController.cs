using Microsoft.AspNetCore.Mvc;

namespace MusicPlayer.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _room;

        public RoomController(RoomService room)
        {
            _room = room;
        }

        // Host only - get the room code to share with guests
        [HttpGet("code")]
        public ActionResult<object> GetCode()
        {
            return Ok(new { code = _room.RoomCode });
        }

        // Guest joins a room with a code, receives a guest token
        [HttpPost("join")]
        public ActionResult<object> Join([FromBody] string code)
        {
            if (!_room.ValidateRoomCode(code))
                return Unauthorized(new { error = "Invalid room code" });

            var token = _room.CreateGuestToken();
            return Ok(new { token });
        }
    }
}