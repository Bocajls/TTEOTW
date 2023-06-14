using ModelLibrary.Abstract.PlayerShipComponents;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete.PlayerShipComponents
{
    public class Drill : ADrill
    {
        public Drill(Drill original)
        {
            ID = original.ID;
            Hardness = original.Hardness;
            Damage = original.Damage;
            Plating = original.Plating;
            Name = original.Name;
            Worth = original.Worth;
            Weight = original.Weight;
        }

        [JsonConstructor]
        public Drill(short ID, float Hardness, float Damage, ThermalPlating Plating, string Name)
        {
            this.ID = ID;
            this.Hardness = Hardness;
            this.Damage = Damage;
            this.Plating = Plating;
            this.Name = Name;
        }
    }
}