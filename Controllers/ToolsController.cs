using Microsoft.AspNetCore.Mvc;
using GarageAPI.Models;

namespace GarageAPI.Controllers
{
    [Route("api/rooms/{roomId}/toolboxes/{toolBoxId}/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public ToolsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetTools(int roomId, int toolBoxId)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == toolBoxId);
            if (toolBox == null) return NotFound();

            return Ok(toolBox.Tools);
        }

        [HttpGet("{id}")]
        public IActionResult GetTool(int roomId, int toolBoxId, int id)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == toolBoxId);
            if (toolBox == null) return NotFound();

            var tool = toolBox.Tools.FirstOrDefault(t => t.Id == id);
            return tool == null ? NotFound() : Ok(tool);
        }

        [HttpPost]
        public IActionResult CreateTool(int roomId, int toolBoxId, [FromBody] Tool newTool)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == toolBoxId);
            if (toolBox == null) return NotFound();

            newTool.Id = toolBox.Tools.Any() ? toolBox.Tools.Max(t => t.Id) + 1 : 1;
            toolBox.Tools.Add(newTool);
            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return CreatedAtAction(nameof(GetTool), new { roomId, toolBoxId, id = newTool.Id }, newTool);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTool(int roomId, int toolBoxId, int id, [FromBody] Tool updatedTool)
        {
            if (updatedTool == null || id != updatedTool.Id) return BadRequest();

            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == toolBoxId);
            if (toolBox == null) return NotFound();

            var tool = toolBox.Tools.FirstOrDefault(t => t.Id == id);
            if (tool == null) return NotFound();

            tool.Name = updatedTool.Name;
            tool.Description = updatedTool.Description;

            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTool(int roomId, int toolBoxId, int id)
        {
            var room = _roomService.GetRoom(roomId);
            if (room == null) return NotFound();

            var toolBox = room.ToolBoxes.FirstOrDefault(tb => tb.Id == toolBoxId);
            if (toolBox == null) return NotFound();

            var tool = toolBox.Tools.FirstOrDefault(t => t.Id == id);
            if (tool == null) return NotFound();

            toolBox.Tools.Remove(tool);
            _roomService.UpdateRoom(room); // Make sure the room is updated in the service
            return NoContent();
        }
    }
}
