using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class ADrill : AType
    {
        public float Damage { get; set; }
        public float Hardness { get; set; } // How hard a mineral can this drill mine
        public AThermalPlating Plating { get; set; } // Heat dissipating plating. Maybe only thermalplating on hull (?)
    }
}