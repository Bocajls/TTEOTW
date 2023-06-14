using ModelLibrary.Abstract.PlayerShipComponents;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete.PlayerShipComponents
{
    public class ThermalPlating : AThermalPlating
    {
        public ThermalPlating(ThermalPlating original)
        {
            ID = original.ID;
            Thermals = original.Thermals;
            MaxThermals = original.MaxThermals;
            ThermalDissipation = original.ThermalDissipation;
            Name = original.Name;
            Worth = original.Worth;
            Weight = original.Weight;
        }

        [JsonConstructor]
        public ThermalPlating(short ID, float Thermals, float MaxThermals, float ThermalDissipation, string Name, float Worth, float Weight)
        {
            this.ID = ID;
            this.Thermals = Thermals;
            this.MaxThermals = MaxThermals;
            this.ThermalDissipation = ThermalDissipation;
            this.Name = Name;
            this.Worth = Worth;
            this.Weight = Weight;
        }
    }
}