using ModelLibrary.Abstract.PlayerShipComponents;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete.PlayerShipComponents
{
    public class FuelTank : AFuelTank
    {
        public FuelTank(FuelTank original)
        {
            ID = original.ID;
            Capacity = original.Capacity;
            Fuel = original.Fuel;
            Name = original.Name;
            Worth = original.Worth;
            Weight = original.Weight;
        }

        [JsonConstructor]
        public FuelTank(short ID, float Capacity, float Fuel, string Name, float Worth, float Weight)
        {
            this.ID = ID;
            this.Capacity = Capacity;
            this.Fuel = Fuel;
            this.Name = Name;
            this.Worth = Worth;
            this.Weight = Weight;
        }
    }
}