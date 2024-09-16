namespace GarageAPI.Models
{
    public class ToolBox
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<Tool> Tools { get; set; }

    }
}
