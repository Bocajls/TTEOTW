using ModelLibrary.Abstract.PlayerShipComponents;
using Newtonsoft.Json;

namespace ModelLibrary.Concrete.PlayerShipComponents
{
    public class Thruster : AThruster
    {
        public Thruster(Thruster original)
        {
            ID = original.ID;
            Speed = original.Speed;
            Acceleration = original.Acceleration;
            Power = original.Power;
            Plating = original.Plating;
            Name = original.Name;
            ActiveFuelConsumption = original.ActiveFuelConsumption;
            Weight = original.Weight;
            Worth = original.Worth;
            MinimumVelocity= original.MinimumVelocity;
        }

        [JsonConstructor]
        public Thruster(short ID, float Speed, float Acceleration, float Power, ThermalPlating Plating, string Name, float ActiveFuelConsumption, float Weight, float Worth, float MinimumVelocity)
        {
            this.ID = ID;
            this.Speed = Speed;
            this.Acceleration = Acceleration;
            this.Power = Power;
            this.Plating = Plating;
            this.Name = Name;
            this.ActiveFuelConsumption = ActiveFuelConsumption;
            this.Weight = Weight;
            this.Worth = Worth;
            this.MinimumVelocity = MinimumVelocity;
        }
    }
}