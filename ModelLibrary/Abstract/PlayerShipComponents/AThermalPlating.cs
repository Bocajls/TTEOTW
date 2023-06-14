using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class AThermalPlating : AType
    {
        public float Thermals { get; set; }
        public float MaxThermals { get; set; } // Maximum temperature endurance
        public float ThermalDissipation { get; set; }
    }
}