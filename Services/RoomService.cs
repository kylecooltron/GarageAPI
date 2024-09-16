using GarageAPI.Models;
using System.Collections.Generic;
using System.Linq;

public class RoomService : IRoomService
{
    private readonly List<Room> _rooms = new List<Room>
    {
        new Room { Id = 1, Name = "Workshop", ToolBoxes = new List<ToolBox>
            {
                new ToolBox { Id = 1, SlotId=6, Name = "Red Toolbox", Description = "Red Toolbox next to door", Tools = new List<Tool> {
                    new Tool { Id = 1, Name = "Hammer", Description = "A strong hammer" },
                    new Tool { Id = 2, Name = "Screwdriver", Description = "A flathead screwdriver" }
                }}
            }
        },
        new Room { Id = 2, Name = "Shed", ToolBoxes = new List<ToolBox>
            {
                new ToolBox { Id = 2, SlotId=1, Name = "Shelves 1", Description = "Shelves in shed", Tools = new List<Tool> {
                    new Tool { Id = 3, Name = "Drill", Description = "A cordless drill" },
                    new Tool { Id = 4, Name = "Sawzall", Description = "An electric Sawzall" }
                }},
                new ToolBox { Id = 3, SlotId=7, Name = "Shelves 2", Description = "Shelves in shed", Tools = new List<Tool> {
                }}
            }
        }
    };

    public List<Room> GetRooms() => _rooms;
    public Room GetRoom(int id) => _rooms.FirstOrDefault(r => r.Id == id);
    public void AddRoom(Room room)
    {
        room.Id = _rooms.Max(r => r.Id) + 1;
        _rooms.Add(room);
    }
    public void UpdateRoom(Room room)
    {
        var existingRoom = _rooms.FirstOrDefault(r => r.Id == room.Id);
        if (existingRoom != null)
        {
            existingRoom.Name = room.Name;
            existingRoom.ToolBoxes = room.ToolBoxes;
        }
    }
    public void DeleteRoom(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        if (room != null)
        {
            _rooms.Remove(room);
        }
    }
}
