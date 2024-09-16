using Microsoft.AspNetCore.Mvc;
using GarageAPI.Models;

namespace GarageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/rooms
        [HttpGet]
        public IActionResult GetRooms() => Ok(_roomService.GetRooms());

        // GET: api/rooms/1
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _roomService.GetRoom(id);
            return room == null ? NotFound() : Ok(room);
        }

        // POST: api/rooms
        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room newRoom)
        {
            _roomService.AddRoom(newRoom);
            return CreatedAtAction(nameof(GetRoom), new { id = newRoom.Id }, newRoom);
        }

        // PUT: api/rooms/1
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            if (updatedRoom == null || id != updatedRoom.Id) return BadRequest("Invalid room data.");

            var existingRoom = _roomService.GetRoom(id);
            if (existingRoom == null) return NotFound();

            _roomService.UpdateRoom(updatedRoom);
            return NoContent();
        }

        // DELETE: api/rooms/1
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var existingRoom = _roomService.GetRoom(id);
            if (existingRoom == null) return NotFound();

            _roomService.DeleteRoom(id);
            return NoContent();
        }
    }
}
