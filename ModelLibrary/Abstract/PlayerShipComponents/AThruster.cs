using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class AThruster : AType
    {
        public float ActiveFuelConsumption { get; set; } // When moving around
        public float Speed { get; set; } // Top speed of air-movement for ship
        public float MinimumVelocity { get; set; } // Top minimum speed of air-movement for ship
        public float Acceleration { get; set; } // How fast ship increases speed
        public float Power { get; set; } // How much can the thrusters air-lift
        public AThermalPlating Plating { get; set; } // Heat dissipating plating. Maybe only thermalplating on hull (?)
    }
}