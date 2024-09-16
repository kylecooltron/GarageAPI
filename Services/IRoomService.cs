using GarageAPI.Models;
using System.Collections.Generic;

public interface IRoomService
{
    List<Room> GetRooms();
    Room GetRoom(int id);
    void AddRoom(Room room);
    void UpdateRoom(Room room);
    void DeleteRoom(int id);
}
