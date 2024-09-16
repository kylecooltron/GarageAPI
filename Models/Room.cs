namespace GarageAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<ToolBox> ToolBoxes { get; set; }
    }
}
