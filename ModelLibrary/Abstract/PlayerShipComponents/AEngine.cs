using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class AEngine : AType
    {
        public float ActiveFuelConsumption { get; set; } // When Mining
        public AThermalPlating Plating { get; set; } // Heat dissipating plating. Maybe only thermalplating on hull (?)
    }
}
