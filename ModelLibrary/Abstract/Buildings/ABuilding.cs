using Microsoft.Xna.Framework;
using ModelLibrary.Abstract.Grids;

namespace ModelLibrary.Abstract.Buildings
{
    public abstract class ABuilding : IBuilding
    {
        // Abstract since every building with have special methods.

        public ABuilding(short ID, string Name, Vector2 GlobalCoordinate, AGrid StorageGrid, AGrid ActiveGrid, ASize Size)
        {
            this.ID = ID;
            this.Name = Name;
            this.GlobalCoordinate = GlobalCoordinate;
            this.StorageGrid = StorageGrid;
            this.ActiveGrid = ActiveGrid;
            this.Size = Size;
        }

        public short ID { get; set; }
        public string Name { get; set; }
        public Vector2 GlobalCoordinate { get; set; }
        public AGrid StorageGrid { get; set; } // Example in Smeltery the grid that contains ingots after having being processed.
        public AGrid ActiveGrid { get; set; } // Example in Smeltery the grid that contains ores that are currently being processed.
        public ASize Size { get; set; }
    }
}