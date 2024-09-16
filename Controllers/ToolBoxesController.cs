using Microsoft.AspNetCore.Mvc;
using GarageAPI.Models;

namespace GarageAPI.Controllers
{
    [Route("api/rooms/{roomId}/[controller]")]
    [ApiController]
    public class ToolBoxesController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public ToolBoxesController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetToolBoxes(int roomId)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            return Ok(room.ToolBoxes);
        }

        [HttpGet("{id}")]
        public IActionResult GetToolBox(int roomId, int id)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == id);
            return toolBox == null ? NotFound() : Ok(toolBox);
        }

        [HttpPost]
        public IActionResult CreateToolBox(int roomId, [FromBody] ToolBox newToolBox)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            newToolBox.Id = room.ToolBoxes.Any() ? room.ToolBoxes.Max(tb => tb.Id) + 1 : 1;
            room.ToolBoxes.Add(newToolBox);
            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return CreatedAtAction(nameof(GetToolBox), new { roomId, id = newToolBox.Id }, newToolBox);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToolBox(int roomId, int id, [FromBody] ToolBox updatedToolBox)
        {
            if (updatedToolBox == null || id != updatedToolBox.Id) return BadRequest();

            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == id);
            if (toolBox == null) return NotFound();

            toolBox.Name = updatedToolBox.Name;
            toolBox.Description = updatedToolBox.Description;
            toolBox.Tools = updatedToolBox.Tools;

            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToolBox(int roomId, int id)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == id);
            if (toolBox == null) return NotFound();

            room.ToolBoxes.Remove(toolBox);
            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return NoContent();
        }
    }
}
