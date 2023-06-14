using ModelLibrary.Abstract;
using ModelLibrary.Concrete.PlayerShipComponents;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete
{
    public class Player : APlayer
    {
        [JsonConstructor]
        public Player(Engine Engine, Hull Hull, Drill Drill, Inventory Inventory, Thruster Thruster, FuelTank FuelTank)
        {
            this.Engine = Engine;
            this.Hull = Hull;
            this.Drill = Drill;
            this.Inventory = Inventory;
            this.Thruster = Thruster;
            this.FuelTank = FuelTank;
        }
    }
}