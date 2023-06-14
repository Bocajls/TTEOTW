using ModelLibrary.Abstract.PlayerShipComponents;
using ModelLibrary.Concrete.Grids;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete.PlayerShipComponents
{
    public class Inventory : AInventory
    {
        [JsonConstructor]
        public Inventory(short ID, Grid Items, float SizeLimit, string Name, float Worth, float Weight)
        {
            this.ID = ID;
            this.Items = Items;
            this.SizeLimit = SizeLimit;
            this.Name = Name;
            this.Worth = Worth;
            this.Weight = Weight;
        }
    }
}